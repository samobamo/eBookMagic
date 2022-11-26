using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tesseract;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Office.Interop.Word;

namespace OCR
{
    public partial class Form1 : Form
    {
        Capture capture;
        System.Drawing.Rectangle captureRect;
        TesseractEngine tesseractEngine;                
        //OcrResult Result;        
        SHA1CryptoServiceProvider sha1;
        String CurrentHash, PrevHash;
        int readAttempt, maxReadAttempt, pagesToRead, index;
        Image ActiveImage;
        bool readCompleted;
        Tesseract.Page res;
        Tesseract.Pix tessImage;        

        public Form1()
        {
            InitializeComponent();            
            InitializeStuff();
        } 
        private void InitializeStuff()
        {
            /*
            TesseractConfiguration config = new TesseractConfiguration();
            config.TesseractVersion = TesseractVersion.Tesseract5;
            config.EngineMode = TesseractEngineMode.TesseractAndLstm;
            config.PageSegmentationMode = TesseractPageSegmentationMode.AutoOsd;
            tesseract = new IronTesseract(config);
            tesseract.Language = OcrLanguage.SloveneBest;
            */
            sha1 = new SHA1CryptoServiceProvider();
            PrevHash = string.Empty;            
            index = 0;
            tesseractEngine = new TesseractEngine(@".\tessdata", "slv", EngineMode.Default);
        }     

        private string GetHashSHA1(byte[] data)
        {
            return string.Concat(sha1.ComputeHash(data).Select(x => x.ToString("X2")));
        }

        private void ProcessImage()
        {
            ActiveImage = Clipboard.GetImage();
            CurrentHash = GetHashSHA1(ImageToByteArray(ActiveImage));
            tessImage = Pix.LoadFromMemory(ImageToByteArray(ActiveImage));            
            if ((CurrentHash != string.Empty) && (PrevHash != CurrentHash))
            {                
                res = tesseractEngine.Process(tessImage);
                richTextBox1.AppendText(res.GetText());
                PrevHash = CurrentHash;
                res.Dispose();
            }
            else
            {
                readAttempt += 1;
                if (readAttempt == maxReadAttempt)
                    readCompleted = true;
            }
        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {                
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }    
        private void GetCaptureRectangle()
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            /*
            capture = new Capture();
            capture.InstanceRef = this;
            capture.ShowDialog();
            captureRect = capture.GetRectangle();
            */
            ScreenShot2 screenShot2 = new ScreenShot2();
            screenShot2.InstanceRef = this;
            screenShot2.ShowDialog();
            captureRect = screenShot2.GetRectangle();
            readCompleted = screenShot2.GetClosedFlag();

        }        

        private void button2_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            app.Visible = true;

            var oDoc = app.Documents.Add();            

            var paragraph1 = oDoc.Content.Paragraphs.Add();
            paragraph1.Range.Text = richTextBox1.Text;            
            paragraph1.Format.SpaceAfter = 24;            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            ScreenShot2 screenShot2 = new ScreenShot2();
            screenShot2.InstanceRef = this;
            screenShot2.ShowDialog();
            captureRect = screenShot2.GetRectangle();
            MessageBox.Show(captureRect.X.ToString()+" "+captureRect.Y.ToString() + " " + captureRect.Width.ToString() + " " + captureRect.Height.ToString());
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void TakeScreenShot()
        {

            System.Drawing.Point StartPoint = new System.Drawing.Point(captureRect.X, captureRect.Y);
            ScreenShot.AutoCaptureImage(StartPoint, System.Drawing.Point.Empty, captureRect);            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //batch            
            readCompleted = false;
            GetCaptureRectangle();

            if (string.IsNullOrEmpty(readAttemptTextBox.Text))
                maxReadAttempt = 4;
            else
                maxReadAttempt = Convert.ToInt32(readAttemptTextBox.Text);
            
            if (string.IsNullOrEmpty(pagesToReadTextBox.Text))
                pagesToRead = 100000;
            else
                pagesToRead = Convert.ToInt32(pagesToReadTextBox.Text);
            index = 0;
            
            readAttempt = 0;

            while (!readCompleted && index < pagesToRead)
            {
                System.Threading.Thread.Sleep(250);
                TakeScreenShot();
                ProcessImage();
                SendKeys.Send("{PGDN}");
                index++;
            }       
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();                
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
    }
}
