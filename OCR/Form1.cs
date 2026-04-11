using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using Tesseract;

namespace OCR
{
    public partial class Form1 : Form
    {
        private System.Drawing.Rectangle captureRect;
        private TesseractEngine tesseractEngine;
        private MD5 md5;
        private string currentHash, prevHash;
        private int readAttempt, maxReadAttempt, pagesToRead, index;
        private Image activeImage;
        private bool readCompleted;

        public Form1()
        {
            InitializeComponent();
            InitializeStuff();
        }

        private void InitializeStuff()
        {
            // Validate configuration before initializing
            if (!AppConfig.ValidateConfiguration(out string errorMessage))
            {
                MessageBox.Show(
                    errorMessage,
                    "Configuration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Environment.Exit(1);
                return;
            }

            try
            {
                md5 = MD5.Create();
                prevHash = string.Empty;
                index = 0;
                
                // Initialize Tesseract with configuration settings
                tesseractEngine = new TesseractEngine(
                    AppConfig.TessdataPath,
                    AppConfig.OcrLanguage,
                    AppConfig.OcrEngineMode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to initialize OCR engine:\n{ex.Message}\n\n" +
                    "The application will now close.",
                    "Initialization Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private string GetHashMD5(byte[] data)
        {
            return string.Concat(md5.ComputeHash(data).Select(x => x.ToString("X2")));
        }

        private void ProcessImage()
        {
            activeImage = Clipboard.GetImage();
            if (activeImage == null)
            {
                readAttempt++;
                if (readAttempt >= maxReadAttempt)
                    readCompleted = true;
                return;
            }

            byte[] imageBytes = ImageToByteArray(activeImage);
            currentHash = GetHashMD5(imageBytes);

            if (!string.IsNullOrEmpty(currentHash) && prevHash != currentHash)
            {
                // Create new tessImage and process it
                using (var tempTessImage = Pix.LoadFromMemory(imageBytes))
                {
                    using (var tempRes = tesseractEngine.Process(tempTessImage))
                    {
                        richTextBox1.AppendText(tempRes.GetText());
                    }
                }
                
                prevHash = currentHash;
                readAttempt = 0; // Reset counter on successful read
            }
            else
            {
                readAttempt++;
                if (readAttempt >= maxReadAttempt)
                    readCompleted = true;
            }
            
            // Dispose the clipboard image
            activeImage?.Dispose();
            activeImage = null;
        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private async System.Threading.Tasks.Task GetCaptureRectangleAsync()
        {
            Hide();
            WindowState = FormWindowState.Minimized;

            using (var screenShot2 = new ScreenShot2 { InstanceRef = this })
            {
                screenShot2.ShowDialog();
                var result = await screenShot2.GetSelectionResultAsync();
                captureRect = result.Bounds;
                readCompleted = result.WasCancelled;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Export to Word
            try
            {
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                app.Visible = true;

                var oDoc = app.Documents.Add();
                var paragraph1 = oDoc.Content.Paragraphs.Add();
                paragraph1.Range.Text = richTextBox1.Text;
                paragraph1.Format.SpaceAfter = 24;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export to Word: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // Debug: Test region selection
            Hide();
            WindowState = FormWindowState.Minimized;

            using (var screenShot2 = new ScreenShot2 { InstanceRef = this })
            {
                screenShot2.ShowDialog();
                var result = await screenShot2.GetSelectionResultAsync();

                Show();
                WindowState = FormWindowState.Normal;

                if (!result.WasCancelled)
                {
                    captureRect = result.Bounds;
                    MessageBox.Show(
                        $"Selection:\n" +
                        $"X: {captureRect.X}\n" +
                        $"Y: {captureRect.Y}\n" +
                        $"Width: {captureRect.Width}\n" +
                        $"Height: {captureRect.Height}",
                        "Region Selected",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Selection was cancelled.", "Cancelled",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void TakeScreenShot()
        {
            var startPoint = new System.Drawing.Point(captureRect.X, captureRect.Y);
            ScreenShot.AutoCaptureImage(startPoint, System.Drawing.Point.Empty, captureRect);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            // Batch OCR reading
            readCompleted = false;
            await GetCaptureRectangleAsync();

            if (readCompleted) // User cancelled selection
            {
                Show();
                WindowState = FormWindowState.Normal;
                return;
            }

            // Parse and validate input values
            if (!int.TryParse(readAttemptTextBox.Text, out maxReadAttempt) || maxReadAttempt <= 0)
                maxReadAttempt = AppConfig.DefaultMaxReadAttempts;

            if (!int.TryParse(pagesToReadTextBox.Text, out pagesToRead) || pagesToRead <= 0)
                pagesToRead = AppConfig.DefaultPagesToRead;

            index = 0;
            readAttempt = 0;

            // Disable UI during processing
            button5.Enabled = false;
            
            if (AppConfig.ShowTimestamps)
                richTextBox1.AppendText($"\n--- Starting OCR at {DateTime.Now:yyyy-MM-dd HH:mm:ss} ---\n");

            try
            {
                // Process pages
                while (!readCompleted && index < pagesToRead)
                {
                    await System.Threading.Tasks.Task.Delay(AppConfig.PageTurnDelayMs);
                    TakeScreenShot();
                    ProcessImage();
                    SendKeys.Send("{PGDN}");
                    index++;

                    // Update UI based on configuration interval
                    if (index % AppConfig.ProgressUpdateInterval == 0)
                    {
                        richTextBox1.AppendText($"\n[Progress: {index} pages processed]\n");
                        System.Windows.Forms.Application.DoEvents();
                    }
                }

                if (AppConfig.ShowTimestamps)
                    richTextBox1.AppendText($"\n--- Completed at {DateTime.Now:yyyy-MM-dd HH:mm:ss}. Total pages: {index} ---\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during OCR processing: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable UI and restore window
                button5.Enabled = true;
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            OpenSettingsDialog();
        }

        private void toolStripMenuItemSettings_Click(object sender, EventArgs e)
        {
            OpenSettingsDialog();
        }

        private void OpenSettingsDialog()
        {
            using (var settingsForm = new SettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // Settings were saved successfully
                    // User was already notified about restart requirement
                }
            }
        }
    }
}
