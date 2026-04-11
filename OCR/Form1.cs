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
        private ProgressFloater _progressFloater;
        private ComparisonView _comparisonView;
        private Image _lastScreenshot;
        private string _lastExtractedText;
        private float _lastConfidence;

        public Form1()
        {
            InitializeComponent();
            InitializeStuff();
            
            // Set initial tray icon text
            notifyIcon1.Text = "eBookMagic - Ready";
            notifyIcon1.BalloonTipTitle = "eBookMagic";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
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
                pagesToReadTextBox.Text = AppConfig.DefaultPagesToRead.ToString();
                readAttemptTextBox.Text = AppConfig.DefaultMaxReadAttempts.ToString();
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
                // Store last screenshot for comparison view
                _lastScreenshot?.Dispose();
                _lastScreenshot = (Image)activeImage.Clone();
                
                // Create new tessImage and process it
                using (var tempTessImage = Pix.LoadFromMemory(imageBytes))
                {
                    using (var tempRes = tesseractEngine.Process(tempTessImage))
                    {
                        string extractedText = tempRes.GetText();
                        _lastConfidence = tempRes.GetMeanConfidence();
                        _lastExtractedText = extractedText;
                        
                        richTextBox1.AppendText(extractedText);
                        
                        // Update comparison view if open
                        if (_comparisonView != null && !_comparisonView.IsDisposed)
                        {
                            _comparisonView.UpdateComparison(_lastScreenshot, extractedText, _lastConfidence);
                        }
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
            buttonSave.Enabled = false;
            buttonClear.Enabled = false;
            
            UpdateStatus("Starting OCR...");
            ShowProgress(true, 0, pagesToRead);
            notifyIcon1.Text = "eBookMagic - Starting OCR...";
            
            // Create and show floating progress window
            _progressFloater = new ProgressFloater();
            _progressFloater.Show();
            _progressFloater.StartProgress(pagesToRead);
            
            if (AppConfig.ShowTimestamps)
                richTextBox1.AppendText($"\n--- Starting OCR at {DateTime.Now:yyyy-MM-dd HH:mm:ss} ---\n");

            try
            {
                // Process pages
                while (!readCompleted && index < pagesToRead)
                {
                    // Check if user cancelled from progress floater
                    if (_progressFloater.CancelRequested)
                    {
                        readCompleted = true;
                        richTextBox1.AppendText($"\n--- OCR Cancelled at page {index} by user ---\n");
                        UpdateStatus("OCR cancelled by user");
                        break;
                    }
                    
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
                    
                    // Update progress bar and status
                    ShowProgress(true, index, pagesToRead);
                    UpdatePageCount(index);
                    UpdateStatus($"Processing page {index} of {pagesToRead}...");
                    
                    // Update floating progress window
                    _progressFloater?.UpdateProgress(index, pagesToRead, "Processing...");
                    
                    // Update system tray icon for minimized feedback
                    int percentage = (int)((double)index / pagesToRead * 100);
                    notifyIcon1.Text = $"eBookMagic - {index}/{pagesToRead} ({percentage}%)";
                    
                    // Show balloon tip at 25%, 50%, 75% milestones (only once per milestone)
                    if (percentage == 25 || percentage == 50 || percentage == 75)
                    {
                        try
                        {
                            notifyIcon1.BalloonTipTitle = "OCR Progress";
                            notifyIcon1.BalloonTipText = $"{percentage}% complete - {index} of {pagesToRead} pages processed";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                            notifyIcon1.ShowBalloonTip(3000);
                        }
                        catch
                        {
                            // Balloon tips may fail on some Windows configurations
                        }
                    }
                }

                if (AppConfig.ShowTimestamps)
                    richTextBox1.AppendText($"\n--- Completed at {DateTime.Now:yyyy-MM-dd HH:mm:ss}. Total pages: {index} ---\n");
                
                UpdateStatus("OCR completed successfully");
                ShowProgress(false);
                UpdatePageCount(index);
                
                // Update progress floater to show completion
                _progressFloater?.CompleteProgress($"Complete! {index} pages processed.");
                
                // Notify user via system tray
                try
                {
                    notifyIcon1.BalloonTipTitle = "OCR Complete";
                    notifyIcon1.BalloonTipText = $"Successfully processed {index} pages!";
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon1.ShowBalloonTip(5000);
                }
                catch { }
                
                notifyIcon1.Text = "eBookMagic - Ready";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during OCR processing: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("OCR failed with error");
                ShowProgress(false);
                notifyIcon1.Text = "eBookMagic - Error occurred";
                
                // Update progress floater
                _progressFloater?.CompleteProgress("Error occurred!");
                
                try
                {
                    notifyIcon1.BalloonTipTitle = "OCR Error";
                    notifyIcon1.BalloonTipText = "An error occurred during processing.";
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                    notifyIcon1.ShowBalloonTip(5000);
                }
                catch { }
            }
            finally
            {
                // Re-enable UI and restore window
                button5.Enabled = true;
                buttonSave.Enabled = true;
                buttonClear.Enabled = true;
                Show();
                WindowState = FormWindowState.Normal;
                
                // Close progress floater after a delay
                if (_progressFloater != null && !_progressFloater.IsDisposed)
                {
                    await System.Threading.Tasks.Task.Delay(3000); // Show completion for 3 seconds
                    _progressFloater?.Close();
                    _progressFloater?.Dispose();
                    _progressFloater = null;
                }
                
                if (!readCompleted)
                {
                    UpdateStatus("Ready");
                    notifyIcon1.Text = "eBookMagic - Ready";
                }
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

        #region Menu and Button Event Handlers

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            OpenSettingsDialog();
        }

        private void toolStripMenuItemSettings_Click(object sender, EventArgs e)
        {
            OpenSettingsDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
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
                    UpdateStatus("Settings updated. Restart application for changes to take effect.");
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveText();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveText();
        }

        private void SaveText()
        {
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("No text to save.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveDialog.DefaultExt = "txt";
                saveDialog.FileName = $"OCR_Extract_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveDialog.FileName, richTextBox1.Text);
                        UpdateStatus($"Saved to {Path.GetFileName(saveDialog.FileName)}");
                        MessageBox.Show("Text saved successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to save file:\\n{ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadText();
        }

        private void LoadText()
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                openDialog.DefaultExt = "txt";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        richTextBox1.Text = File.ReadAllText(openDialog.FileName);
                        UpdateStatus($"Loaded {Path.GetFileName(openDialog.FileName)}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to load file:\\n{ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearOutput();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearOutput();
        }

        private void ClearOutput()
        {
            if (richTextBox1.Text.Length > 0)
            {
                var result = MessageBox.Show(
                    "Clear all extracted text?",
                    "Confirm Clear",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    richTextBox1.Clear();
                    UpdateStatus("Output cleared");
                    UpdatePageCount(0);
                }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
            
            // Sync the tray menu item
            toolStripMenuItemAlwaysOnTop.Checked = alwaysOnTopToolStripMenuItem.Checked;
            
            if (this.TopMost)
                UpdateStatus("Window set to always on top");
            else
                UpdateStatus("Window normal mode");
        }

        private void toolStripMenuItemAlwaysOnTop_Click(object sender, EventArgs e)
        {
            this.TopMost = toolStripMenuItemAlwaysOnTop.Checked;
            
            // Sync the View menu item
            alwaysOnTopToolStripMenuItem.Checked = toolStripMenuItemAlwaysOnTop.Checked;
            
            if (this.TopMost)
                UpdateStatus("Window set to always on top");
            else
                UpdateStatus("Window normal mode");
        }

        private void showComparisonViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowComparisonView();
        }

        private void ShowComparisonView()
        {
            // Create comparison view if it doesn't exist or was disposed
            if (_comparisonView == null || _comparisonView.IsDisposed)
            {
                _comparisonView = new ComparisonView();
                
                // If we have last screenshot, show it
                if (_lastScreenshot != null)
                {
                    _comparisonView.UpdateComparison(_lastScreenshot, _lastExtractedText, _lastConfidence);
                }
            }

            // Show the comparison view
            if (!_comparisonView.Visible)
            {
                _comparisonView.Show(this);
                UpdateStatus("Comparison view opened");
            }
            else
            {
                _comparisonView.BringToFront();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void userGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string guidePath = Path.Combine(Application.StartupPath, "GUI_SETTINGS_GUIDE.md");
                if (File.Exists(guidePath))
                {
                    System.Diagnostics.Process.Start("notepad.exe", guidePath);
                }
                else
                {
                    MessageBox.Show(
                        "User guide not found.\\n\\n" +
                        "Please visit: https://github.com/samobamo/eBookMagic",
                        "User Guide",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open user guide:\\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "eBookMagic - OCR Text Extractor\\n" +
                "Version 1.0\\n\\n" +
                "Extract text from eBooks using Tesseract OCR\\n\\n" +
                "� 2025 samobamo\\n" +
                "GitHub: https://github.com/samobamo/eBookMagic",
                "About eBookMagic",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        #region Status Bar Helpers

        private void UpdateStatus(string message)
        {
            toolStripStatusLabel1.Text = message;
        }

        private void UpdatePageCount(int pages)
        {
            toolStripStatusLabel3.Text = $"{pages} pages";
        }

        private void ShowProgress(bool show, int value = 0, int maximum = 100)
        {
            toolStripProgressBar1.Visible = show;
            if (show)
            {
                toolStripProgressBar1.Maximum = maximum;
                toolStripProgressBar1.Value = Math.Min(value, maximum);
            }
        }

        #endregion
    }
}
