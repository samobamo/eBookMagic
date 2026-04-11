namespace OCR
{
    partial class ComparisonView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelImage = new System.Windows.Forms.Panel();
            this.pictureBoxOriginal = new System.Windows.Forms.PictureBox();
            this.panelImageHeader = new System.Windows.Forms.Panel();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.lblOriginal = new System.Windows.Forms.Label();
            this.panelText = new System.Windows.Forms.Panel();
            this.txtExtracted = new System.Windows.Forms.RichTextBox();
            this.panelTextHeader = new System.Windows.Forms.Panel();
            this.btnCopyText = new System.Windows.Forms.Button();
            this.lblExtracted = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblQualityIndicator = new System.Windows.Forms.Label();
            this.lblConfidence = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).BeginInit();
            this.panelImageHeader.SuspendLayout();
            this.panelText.SuspendLayout();
            this.panelTextHeader.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelImage);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelText);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 600);
            this.splitContainer1.SplitterDistance = 480;
            this.splitContainer1.TabIndex = 0;
            // 
            // panelImage
            // 
            this.panelImage.AutoScroll = true;
            this.panelImage.BackColor = System.Drawing.Color.White;
            this.panelImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImage.Controls.Add(this.pictureBoxOriginal);
            this.panelImage.Controls.Add(this.panelImageHeader);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImage.Location = new System.Drawing.Point(0, 0);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(480, 600);
            this.panelImage.TabIndex = 0;
            // 
            // pictureBoxOriginal
            // 
            this.pictureBoxOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxOriginal.Location = new System.Drawing.Point(0, 35);
            this.pictureBoxOriginal.Name = "pictureBoxOriginal";
            this.pictureBoxOriginal.Size = new System.Drawing.Size(478, 563);
            this.pictureBoxOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOriginal.TabIndex = 0;
            this.pictureBoxOriginal.TabStop = false;
            // 
            // panelImageHeader
            // 
            this.panelImageHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelImageHeader.Controls.Add(this.btnZoomIn);
            this.panelImageHeader.Controls.Add(this.btnSaveImage);
            this.panelImageHeader.Controls.Add(this.lblOriginal);
            this.panelImageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelImageHeader.Location = new System.Drawing.Point(0, 0);
            this.panelImageHeader.Name = "panelImageHeader";
            this.panelImageHeader.Size = new System.Drawing.Size(478, 35);
            this.panelImageHeader.TabIndex = 1;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomIn.Location = new System.Drawing.Point(389, 6);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(80, 23);
            this.btnZoomIn.TabIndex = 2;
            this.btnZoomIn.Text = "100%";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveImage.Location = new System.Drawing.Point(281, 6);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(102, 23);
            this.btnSaveImage.TabIndex = 1;
            this.btnSaveImage.Text = "Save Image...";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // lblOriginal
            // 
            this.lblOriginal.AutoSize = true;
            this.lblOriginal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOriginal.Location = new System.Drawing.Point(8, 9);
            this.lblOriginal.Name = "lblOriginal";
            this.lblOriginal.Size = new System.Drawing.Size(124, 17);
            this.lblOriginal.TabIndex = 0;
            this.lblOriginal.Text = "Original Screenshot";
            // 
            // panelText
            // 
            this.panelText.Controls.Add(this.txtExtracted);
            this.panelText.Controls.Add(this.panelTextHeader);
            this.panelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelText.Location = new System.Drawing.Point(0, 0);
            this.panelText.Name = "panelText";
            this.panelText.Size = new System.Drawing.Size(516, 600);
            this.panelText.TabIndex = 0;
            // 
            // txtExtracted
            // 
            this.txtExtracted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExtracted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExtracted.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExtracted.Location = new System.Drawing.Point(0, 35);
            this.txtExtracted.Name = "txtExtracted";
            this.txtExtracted.ReadOnly = true;
            this.txtExtracted.Size = new System.Drawing.Size(516, 565);
            this.txtExtracted.TabIndex = 0;
            this.txtExtracted.Text = "";
            // 
            // panelTextHeader
            // 
            this.panelTextHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelTextHeader.Controls.Add(this.btnCopyText);
            this.panelTextHeader.Controls.Add(this.lblExtracted);
            this.panelTextHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTextHeader.Location = new System.Drawing.Point(0, 0);
            this.panelTextHeader.Name = "panelTextHeader";
            this.panelTextHeader.Size = new System.Drawing.Size(516, 35);
            this.panelTextHeader.TabIndex = 1;
            // 
            // btnCopyText
            // 
            this.btnCopyText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyText.Location = new System.Drawing.Point(398, 6);
            this.btnCopyText.Name = "btnCopyText";
            this.btnCopyText.Size = new System.Drawing.Size(110, 23);
            this.btnCopyText.TabIndex = 1;
            this.btnCopyText.Text = "Copy Text";
            this.btnCopyText.UseVisualStyleBackColor = true;
            this.btnCopyText.Click += new System.EventHandler(this.btnCopyText_Click);
            // 
            // lblExtracted
            // 
            this.lblExtracted.AutoSize = true;
            this.lblExtracted.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtracted.Location = new System.Drawing.Point(8, 9);
            this.lblExtracted.Name = "lblExtracted";
            this.lblExtracted.Size = new System.Drawing.Size(100, 17);
            this.lblExtracted.TabIndex = 0;
            this.lblExtracted.Text = "Extracted Text";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Controls.Add(this.lblStats);
            this.panelBottom.Controls.Add(this.lblQualityIndicator);
            this.panelBottom.Controls.Add(this.lblConfidence);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 600);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1000, 50);
            this.panelBottom.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(896, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 28);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblStats
            // 
            this.lblStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStats.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStats.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblStats.Location = new System.Drawing.Point(350, 25);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(540, 13);
            this.lblStats.TabIndex = 2;
            this.lblStats.Text = "Characters: 0 | Words: 0 | Lines: 0";
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblQualityIndicator
            // 
            this.lblQualityIndicator.AutoSize = true;
            this.lblQualityIndicator.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQualityIndicator.ForeColor = System.Drawing.Color.Green;
            this.lblQualityIndicator.Location = new System.Drawing.Point(10, 25);
            this.lblQualityIndicator.Name = "lblQualityIndicator";
            this.lblQualityIndicator.Size = new System.Drawing.Size(0, 15);
            this.lblQualityIndicator.TabIndex = 1;
            // 
            // lblConfidence
            // 
            this.lblConfidence.AutoSize = true;
            this.lblConfidence.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfidence.Location = new System.Drawing.Point(10, 6);
            this.lblConfidence.Name = "lblConfidence";
            this.lblConfidence.Size = new System.Drawing.Size(143, 17);
            this.lblConfidence.TabIndex = 0;
            this.lblConfidence.Text = "OCR Confidence: N/A";
            // 
            // ComparisonView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelBottom);
            this.MinimizeBox = false;
            this.Name = "ComparisonView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OCR Comparison View";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).EndInit();
            this.panelImageHeader.ResumeLayout(false);
            this.panelImageHeader.PerformLayout();
            this.panelText.ResumeLayout(false);
            this.panelTextHeader.ResumeLayout(false);
            this.panelTextHeader.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.PictureBox pictureBoxOriginal;
        private System.Windows.Forms.Panel panelText;
        private System.Windows.Forms.RichTextBox txtExtracted;
        private System.Windows.Forms.Panel panelImageHeader;
        private System.Windows.Forms.Label lblOriginal;
        private System.Windows.Forms.Panel panelTextHeader;
        private System.Windows.Forms.Label lblExtracted;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Button btnCopyText;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblConfidence;
        private System.Windows.Forms.Label lblQualityIndicator;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Button btnClose;
    }
}
