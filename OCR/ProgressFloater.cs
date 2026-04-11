using System;
using System.Drawing;
using System.Windows.Forms;

namespace OCR
{
    /// <summary>
    /// Floating progress window that displays OCR progress and allows cancellation.
    /// </summary>
    public partial class ProgressFloater : Form
    {
        private bool _cancelRequested = false;

        public ProgressFloater()
        {
            InitializeComponent();
            InitializeFloater();
        }

        private void InitializeFloater()
        {
            // Position in bottom-right corner of screen
            this.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Right - this.Width - 10,
                Screen.PrimaryScreen.WorkingArea.Bottom - this.Height - 10);
        }

        /// <summary>
        /// Gets whether the user requested cancellation.
        /// </summary>
        public bool CancelRequested => _cancelRequested;

        /// <summary>
        /// Updates the progress display.
        /// </summary>
        public void UpdateProgress(int current, int total, string status = null)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int, int, string>(UpdateProgress), current, total, status);
                return;
            }

            lblCurrentPage.Text = $"Page: {current} / {total}";
            progressBar1.Maximum = total;
            progressBar1.Value = Math.Min(current, total);
            
            int percentage = total > 0 ? (int)((double)current / total * 100) : 0;
            lblPercentage.Text = $"{percentage}%";

            if (!string.IsNullOrEmpty(status))
                lblStatus.Text = status;

            // Calculate estimated time remaining
            if (current > 0 && _startTime.HasValue)
            {
                TimeSpan elapsed = DateTime.Now - _startTime.Value;
                double avgTimePerPage = elapsed.TotalSeconds / current;
                int remaining = total - current;
                TimeSpan estimatedRemaining = TimeSpan.FromSeconds(avgTimePerPage * remaining);
                
                if (estimatedRemaining.TotalHours >= 1)
                    lblTimeRemaining.Text = $"Est: {estimatedRemaining:h\\:mm\\:ss} remaining";
                else
                    lblTimeRemaining.Text = $"Est: {estimatedRemaining:mm\\:ss} remaining";
            }
            else
            {
                lblTimeRemaining.Text = "Calculating...";
            }
        }

        private DateTime? _startTime;

        /// <summary>
        /// Starts the progress tracking.
        /// </summary>
        public void StartProgress(int totalPages)
        {
            _startTime = DateTime.Now;
            _cancelRequested = false;
            btnCancel.Enabled = true;
            UpdateProgress(0, totalPages, "Starting OCR...");
        }

        /// <summary>
        /// Completes the progress tracking.
        /// </summary>
        public void CompleteProgress(string message = "Complete!")
        {
            lblStatus.Text = message;
            btnCancel.Text = "Close";
            
            if (_startTime.HasValue)
            {
                TimeSpan elapsed = DateTime.Now - _startTime.Value;
                lblTimeRemaining.Text = $"Completed in {elapsed:mm\\:ss}";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Text == "Close")
            {
                this.Close();
            }
            else
            {
                _cancelRequested = true;
                btnCancel.Enabled = false;
                lblStatus.Text = "Cancelling...";
            }
        }

        /// <summary>
        /// Prevents user from closing the window using X button during processing.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_startTime.HasValue && btnCancel.Text != "Close")
            {
                e.Cancel = true;
                MessageBox.Show(
                    "OCR is still in progress.\n\nPlease use the Cancel button to stop.",
                    "Cannot Close",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            base.OnFormClosing(e);
        }

        /// <summary>
        /// Resets the floater for a new operation.
        /// </summary>
        public void Reset()
        {
            _startTime = null;
            _cancelRequested = false;
            progressBar1.Value = 0;
            lblCurrentPage.Text = "Page: 0 / 0";
            lblPercentage.Text = "0%";
            lblStatus.Text = "Ready";
            lblTimeRemaining.Text = "";
            btnCancel.Text = "Cancel";
            btnCancel.Enabled = true;
        }
    }
}
