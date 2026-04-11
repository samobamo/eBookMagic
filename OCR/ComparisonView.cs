using System;
using System.Drawing;
using System.Windows.Forms;

namespace OCR
{
    /// <summary>
    /// Side-by-side comparison view showing original screenshot and extracted OCR text.
    /// </summary>
    public partial class ComparisonView : Form
    {
        private Image _currentScreenshot;
        private string _currentExtractedText;
        private float _ocrConfidence;

        public ComparisonView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates the comparison view with a new screenshot and extracted text.
        /// </summary>
        public void UpdateComparison(Image screenshot, string extractedText, float confidence = 0)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Image, string, float>(UpdateComparison), screenshot, extractedText, confidence);
                return;
            }

            _currentScreenshot = screenshot;
            _currentExtractedText = extractedText;
            _ocrConfidence = confidence;

            // Update image
            if (screenshot != null)
            {
                pictureBoxOriginal.Image = (Image)screenshot.Clone();
                pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
            }

            // Update text
            txtExtracted.Text = extractedText ?? string.Empty;

            // Update confidence indicator
            if (confidence > 0)
            {
                lblConfidence.Text = $"OCR Confidence: {confidence:F1}%";
                
                if (confidence >= 85)
                {
                    lblConfidence.ForeColor = Color.Green;
                    lblQualityIndicator.Text = "? High Quality";
                    lblQualityIndicator.ForeColor = Color.Green;
                }
                else if (confidence >= 70)
                {
                    lblConfidence.ForeColor = Color.Orange;
                    lblQualityIndicator.Text = "? Medium Quality";
                    lblQualityIndicator.ForeColor = Color.Orange;
                }
                else
                {
                    lblConfidence.ForeColor = Color.Red;
                    lblQualityIndicator.Text = "? Low Quality - Review Needed";
                    lblQualityIndicator.ForeColor = Color.Red;
                }
            }
            else
            {
                lblConfidence.Text = "OCR Confidence: N/A";
                lblConfidence.ForeColor = SystemColors.ControlText;
                lblQualityIndicator.Text = "";
            }

            UpdateStatistics();
        }

        /// <summary>
        /// Updates the statistics panel.
        /// </summary>
        private void UpdateStatistics()
        {
            if (string.IsNullOrEmpty(_currentExtractedText))
            {
                lblStats.Text = "No text extracted";
                return;
            }

            int charCount = _currentExtractedText.Length;
            int wordCount = _currentExtractedText.Split(new[] { ' ', '\n', '\r', '\t' }, 
                StringSplitOptions.RemoveEmptyEntries).Length;
            int lineCount = _currentExtractedText.Split('\n').Length;

            lblStats.Text = $"Characters: {charCount:N0} | Words: {wordCount:N0} | Lines: {lineCount}";
        }

        /// <summary>
        /// Clears the comparison view.
        /// </summary>
        public void ClearComparison()
        {
            pictureBoxOriginal.Image = null;
            txtExtracted.Clear();
            lblConfidence.Text = "OCR Confidence: N/A";
            lblQualityIndicator.Text = "";
            lblStats.Text = "";
        }

        /// <summary>
        /// Copies the extracted text to clipboard.
        /// </summary>
        private void btnCopyText_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExtracted.Text))
            {
                Clipboard.SetText(txtExtracted.Text);
                MessageBox.Show("Text copied to clipboard!", "Copied", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Saves the screenshot to a file.
        /// </summary>
        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            if (_currentScreenshot == null)
            {
                MessageBox.Show("No image to save.", "Save Image", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|All Files (*.*)|*.*";
                saveDialog.DefaultExt = "png";
                saveDialog.FileName = $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _currentScreenshot.Save(saveDialog.FileName);
                        MessageBox.Show("Image saved successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to save image:\n{ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Zooms in on the image.
        /// </summary>
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (pictureBoxOriginal.SizeMode == PictureBoxSizeMode.Zoom)
            {
                pictureBoxOriginal.SizeMode = PictureBoxSizeMode.AutoSize;
                btnZoomIn.Text = "Fit";
            }
            else
            {
                pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
                btnZoomIn.Text = "100%";
            }
        }

        /// <summary>
        /// Closes the comparison view.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Cleanup resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _currentScreenshot?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
