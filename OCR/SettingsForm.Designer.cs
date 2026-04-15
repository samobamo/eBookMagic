namespace OCR
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxOCR = new System.Windows.Forms.GroupBox();
            this.btnBrowseTessdata = new System.Windows.Forms.Button();
            this.txtTessdataPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEngineMode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxTiming = new System.Windows.Forms.GroupBox();
            this.nudFormCloseDelay = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudScreenshotDelay = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudPageTurnDelay = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxBatch = new System.Windows.Forms.GroupBox();
            this.nudMaxPages = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudMaxReadAttempts = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxUI = new System.Windows.Forms.GroupBox();
            this.chkEnableLogging = new System.Windows.Forms.CheckBox();
            this.chkShowTimestamps = new System.Windows.Forms.CheckBox();
            this.nudProgressInterval = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnResetDefaults = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxOCR.SuspendLayout();
            this.groupBoxTiming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFormCloseDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScreenshotDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageTurnDelay)).BeginInit();
            this.groupBoxBatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxReadAttempts)).BeginInit();
            this.groupBoxUI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProgressInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxOCR
            // 
            this.groupBoxOCR.Controls.Add(this.btnBrowseTessdata);
            this.groupBoxOCR.Controls.Add(this.txtTessdataPath);
            this.groupBoxOCR.Controls.Add(this.label6);
            this.groupBoxOCR.Controls.Add(this.cmbEngineMode);
            this.groupBoxOCR.Controls.Add(this.label2);
            this.groupBoxOCR.Controls.Add(this.cmbLanguage);
            this.groupBoxOCR.Controls.Add(this.label1);
            this.groupBoxOCR.Location = new System.Drawing.Point(12, 12);
            this.groupBoxOCR.Name = "groupBoxOCR";
            this.groupBoxOCR.Size = new System.Drawing.Size(460, 140);
            this.groupBoxOCR.TabIndex = 0;
            this.groupBoxOCR.TabStop = false;
            this.groupBoxOCR.Text = "OCR Engine Settings";
            // 
            // btnBrowseTessdata
            // 
            this.btnBrowseTessdata.Location = new System.Drawing.Point(379, 99);
            this.btnBrowseTessdata.Name = "btnBrowseTessdata";
            this.btnBrowseTessdata.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseTessdata.TabIndex = 6;
            this.btnBrowseTessdata.Text = "Browse...";
            this.btnBrowseTessdata.UseVisualStyleBackColor = true;
            this.btnBrowseTessdata.Click += new System.EventHandler(this.btnBrowseTessdata_Click);
            // 
            // txtTessdataPath
            // 
            this.txtTessdataPath.Location = new System.Drawing.Point(120, 100);
            this.txtTessdataPath.Name = "txtTessdataPath";
            this.txtTessdataPath.Size = new System.Drawing.Size(253, 20);
            this.txtTessdataPath.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Tessdata Path:";
            // 
            // cmbEngineMode
            // 
            this.cmbEngineMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEngineMode.FormattingEnabled = true;
            this.cmbEngineMode.Items.AddRange(new object[] {
            "Default",
            "TesseractOnly",
            "LstmOnly",
            "TesseractAndLstm"});
            this.cmbEngineMode.Location = new System.Drawing.Point(120, 62);
            this.cmbEngineMode.Name = "cmbEngineMode";
            this.cmbEngineMode.Size = new System.Drawing.Size(253, 21);
            this.cmbEngineMode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Engine Mode:";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Items.AddRange(new object[] {
            "eng - English",
            "slv - Slovenian",
            "deu - German",
            "spa - Spanish",
            "fra - French",
            "ita - Italian",
            "por - Portuguese",
            "rus - Russian",
            "pol - Polish",
            "nld - Dutch"});
            this.cmbLanguage.Location = new System.Drawing.Point(120, 25);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(253, 21);
            this.cmbLanguage.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Language:";
            // 
            // groupBoxTiming
            // 
            this.groupBoxTiming.Controls.Add(this.nudFormCloseDelay);
            this.groupBoxTiming.Controls.Add(this.label7);
            this.groupBoxTiming.Controls.Add(this.nudScreenshotDelay);
            this.groupBoxTiming.Controls.Add(this.label5);
            this.groupBoxTiming.Controls.Add(this.nudPageTurnDelay);
            this.groupBoxTiming.Controls.Add(this.label4);
            this.groupBoxTiming.Location = new System.Drawing.Point(12, 158);
            this.groupBoxTiming.Name = "groupBoxTiming";
            this.groupBoxTiming.Size = new System.Drawing.Size(460, 120);
            this.groupBoxTiming.TabIndex = 1;
            this.groupBoxTiming.TabStop = false;
            this.groupBoxTiming.Text = "Timing Settings (milliseconds)";
            // 
            // nudFormCloseDelay
            // 
            this.nudFormCloseDelay.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudFormCloseDelay.Location = new System.Drawing.Point(200, 82);
            this.nudFormCloseDelay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudFormCloseDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudFormCloseDelay.Name = "nudFormCloseDelay";
            this.nudFormCloseDelay.Size = new System.Drawing.Size(80, 20);
            this.nudFormCloseDelay.TabIndex = 5;
            this.nudFormCloseDelay.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Form Close Delay (internal timing):";
            // 
            // nudScreenshotDelay
            // 
            this.nudScreenshotDelay.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudScreenshotDelay.Location = new System.Drawing.Point(200, 54);
            this.nudScreenshotDelay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudScreenshotDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudScreenshotDelay.Name = "nudScreenshotDelay";
            this.nudScreenshotDelay.Size = new System.Drawing.Size(80, 20);
            this.nudScreenshotDelay.TabIndex = 3;
            this.nudScreenshotDelay.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Screenshot Delay (before):";
            // 
            // nudPageTurnDelay
            // 
            this.nudPageTurnDelay.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudPageTurnDelay.Location = new System.Drawing.Point(200, 26);
            this.nudPageTurnDelay.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudPageTurnDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPageTurnDelay.Name = "nudPageTurnDelay";
            this.nudPageTurnDelay.Size = new System.Drawing.Size(80, 20);
            this.nudPageTurnDelay.TabIndex = 1;
            this.nudPageTurnDelay.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Page Turn Delay (between):";
            // 
            // groupBoxBatch
            // 
            this.groupBoxBatch.Controls.Add(this.nudMaxPages);
            this.groupBoxBatch.Controls.Add(this.label9);
            this.groupBoxBatch.Controls.Add(this.nudMaxReadAttempts);
            this.groupBoxBatch.Controls.Add(this.label3);
            this.groupBoxBatch.Location = new System.Drawing.Point(12, 284);
            this.groupBoxBatch.Name = "groupBoxBatch";
            this.groupBoxBatch.Size = new System.Drawing.Size(460, 90);
            this.groupBoxBatch.TabIndex = 2;
            this.groupBoxBatch.TabStop = false;
            this.groupBoxBatch.Text = "Default Batch Processing Settings";
            // 
            // nudMaxPages
            // 
            this.nudMaxPages.Location = new System.Drawing.Point(200, 54);
            this.nudMaxPages.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudMaxPages.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxPages.Name = "nudMaxPages";
            this.nudMaxPages.Size = new System.Drawing.Size(80, 20);
            this.nudMaxPages.TabIndex = 3;
            this.nudMaxPages.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Default Pages to Read:";
            // 
            // nudMaxReadAttempts
            // 
            this.nudMaxReadAttempts.Location = new System.Drawing.Point(200, 26);
            this.nudMaxReadAttempts.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudMaxReadAttempts.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxReadAttempts.Name = "nudMaxReadAttempts";
            this.nudMaxReadAttempts.Size = new System.Drawing.Size(80, 20);
            this.nudMaxReadAttempts.TabIndex = 1;
            this.nudMaxReadAttempts.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Max Empty Pages (before stopping):";
            // 
            // groupBoxUI
            // 
            this.groupBoxUI.Controls.Add(this.chkEnableLogging);
            this.groupBoxUI.Controls.Add(this.chkShowTimestamps);
            this.groupBoxUI.Controls.Add(this.nudProgressInterval);
            this.groupBoxUI.Controls.Add(this.label8);
            this.groupBoxUI.Location = new System.Drawing.Point(12, 380);
            this.groupBoxUI.Name = "groupBoxUI";
            this.groupBoxUI.Size = new System.Drawing.Size(460, 100);
            this.groupBoxUI.TabIndex = 3;
            this.groupBoxUI.TabStop = false;
            this.groupBoxUI.Text = "User Interface Settings";
            // 
            // chkEnableLogging
            // 
            this.chkEnableLogging.AutoSize = true;
            this.chkEnableLogging.Location = new System.Drawing.Point(18, 70);
            this.chkEnableLogging.Name = "chkEnableLogging";
            this.chkEnableLogging.Size = new System.Drawing.Size(172, 17);
            this.chkEnableLogging.TabIndex = 3;
            this.chkEnableLogging.Text = "Enable Logging (future feature)";
            this.chkEnableLogging.UseVisualStyleBackColor = true;
            // 
            // chkShowTimestamps
            // 
            this.chkShowTimestamps.AutoSize = true;
            this.chkShowTimestamps.Checked = true;
            this.chkShowTimestamps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowTimestamps.Location = new System.Drawing.Point(18, 47);
            this.chkShowTimestamps.Name = "chkShowTimestamps";
            this.chkShowTimestamps.Size = new System.Drawing.Size(184, 17);
            this.chkShowTimestamps.TabIndex = 2;
            this.chkShowTimestamps.Text = "Show Timestamps in OCR Output";
            this.chkShowTimestamps.UseVisualStyleBackColor = true;
            // 
            // nudProgressInterval
            // 
            this.nudProgressInterval.Location = new System.Drawing.Point(200, 21);
            this.nudProgressInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudProgressInterval.Name = "nudProgressInterval";
            this.nudProgressInterval.Size = new System.Drawing.Size(80, 20);
            this.nudProgressInterval.TabIndex = 1;
            this.nudProgressInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(165, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Progress Update Interval (pages):";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(280, 520);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(376, 520);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnResetDefaults
            // 
            this.btnResetDefaults.Location = new System.Drawing.Point(12, 520);
            this.btnResetDefaults.Name = "btnResetDefaults";
            this.btnResetDefaults.Size = new System.Drawing.Size(120, 30);
            this.btnResetDefaults.TabIndex = 6;
            this.btnResetDefaults.Text = "Reset to Defaults";
            this.btnResetDefaults.UseVisualStyleBackColor = true;
            this.btnResetDefaults.Click += new System.EventHandler(this.btnResetDefaults_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblInfo.Location = new System.Drawing.Point(12, 490);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(305, 13);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "Note: Application restart required for changes to take full effect.";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 562);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnResetDefaults);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxUI);
            this.Controls.Add(this.groupBoxBatch);
            this.Controls.Add(this.groupBoxTiming);
            this.Controls.Add(this.groupBoxOCR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "eBookMagic Settings";
            this.groupBoxOCR.ResumeLayout(false);
            this.groupBoxOCR.PerformLayout();
            this.groupBoxTiming.ResumeLayout(false);
            this.groupBoxTiming.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFormCloseDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScreenshotDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageTurnDelay)).EndInit();
            this.groupBoxBatch.ResumeLayout(false);
            this.groupBoxBatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxReadAttempts)).EndInit();
            this.groupBoxUI.ResumeLayout(false);
            this.groupBoxUI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProgressInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxOCR;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxTiming;
        private System.Windows.Forms.GroupBox groupBoxBatch;
        private System.Windows.Forms.GroupBox groupBoxUI;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnResetDefaults;
        private System.Windows.Forms.ComboBox cmbEngineMode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMaxReadAttempts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudPageTurnDelay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudScreenshotDelay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTessdataPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudFormCloseDelay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudProgressInterval;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkShowTimestamps;
        private System.Windows.Forms.CheckBox chkEnableLogging;
        private System.Windows.Forms.Button btnBrowseTessdata;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.NumericUpDown nudMaxPages;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}
