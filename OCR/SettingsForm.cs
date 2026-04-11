using System;
using System.Configuration;
using System.Windows.Forms;
using Tesseract;

namespace OCR
{
    /// <summary>
    /// Settings dialog for easy configuration without editing XML.
    /// </summary>
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadCurrentSettings();
        }

        /// <summary>
        /// Loads current settings from AppConfig into the form controls.
        /// </summary>
        private void LoadCurrentSettings()
        {
            // OCR Settings
            SetLanguageComboBox(AppConfig.OcrLanguage);
            cmbEngineMode.Text = AppConfig.OcrEngineMode.ToString();
            txtTessdataPath.Text = AppConfig.TessdataPath;

            // Timing Settings
            nudPageTurnDelay.Value = AppConfig.PageTurnDelayMs;
            nudScreenshotDelay.Value = AppConfig.ScreenshotDelayMs;
            nudFormCloseDelay.Value = AppConfig.FormCloseDelayMs;

            // Batch Settings
            nudMaxReadAttempts.Value = AppConfig.DefaultMaxReadAttempts;
            nudMaxPages.Value = AppConfig.DefaultPagesToRead;

            // UI Settings
            nudProgressInterval.Value = AppConfig.ProgressUpdateInterval;
            chkShowTimestamps.Checked = AppConfig.ShowTimestamps;
            chkEnableLogging.Checked = AppConfig.EnableLogging;
        }

        /// <summary>
        /// Sets the language combo box based on the language code.
        /// </summary>
        private void SetLanguageComboBox(string langCode)
        {
            foreach (string item in cmbLanguage.Items)
            {
                if (item.StartsWith(langCode + " "))
                {
                    cmbLanguage.Text = item;
                    return;
                }
            }
            // If not found in predefined list, just set the code
            cmbLanguage.Text = langCode;
        }

        /// <summary>
        /// Extracts the language code from the combo box selection.
        /// </summary>
        private string GetLanguageCode()
        {
            string selected = cmbLanguage.Text;
            if (selected.Contains(" - "))
            {
                return selected.Substring(0, selected.IndexOf(" "));
            }
            return selected;
        }

        /// <summary>
        /// Saves settings to the configuration file.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate tessdata path
                if (!System.IO.Directory.Exists(txtTessdataPath.Text))
                {
                    var result = MessageBox.Show(
                        $"The tessdata folder does not exist:\n{txtTessdataPath.Text}\n\n" +
                        "Do you want to save anyway?",
                        "Folder Not Found",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                        return;
                }

                // Open configuration for editing
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Update OCR settings
                UpdateSetting(config, "OcrLanguage", GetLanguageCode());
                UpdateSetting(config, "OcrEngineMode", cmbEngineMode.Text);
                UpdateSetting(config, "TessdataPath", txtTessdataPath.Text);

                // Update Timing settings
                UpdateSetting(config, "PageTurnDelayMs", nudPageTurnDelay.Value.ToString());
                UpdateSetting(config, "ScreenshotDelayMs", nudScreenshotDelay.Value.ToString());
                UpdateSetting(config, "FormCloseDelayMs", nudFormCloseDelay.Value.ToString());

                // Update Batch settings
                UpdateSetting(config, "DefaultMaxReadAttempts", nudMaxReadAttempts.Value.ToString());
                UpdateSetting(config, "DefaultPagesToRead", nudMaxPages.Value.ToString());

                // Update UI settings
                UpdateSetting(config, "ProgressUpdateInterval", nudProgressInterval.Value.ToString());
                UpdateSetting(config, "ShowTimestamps", chkShowTimestamps.Checked.ToString().ToLower());
                UpdateSetting(config, "EnableLogging", chkEnableLogging.Checked.ToString().ToLower());

                // Save changes
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                MessageBox.Show(
                    "Settings saved successfully!\n\n" +
                    "Please restart the application for changes to take full effect.",
                    "Settings Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to save settings:\n{ex.Message}\n\n" +
                    "Make sure the application has write permissions to its folder.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates or adds a setting in the configuration.
        /// </summary>
        private void UpdateSetting(Configuration config, string key, string value)
        {
            if (config.AppSettings.Settings[key] != null)
                config.AppSettings.Settings[key].Value = value;
            else
                config.AppSettings.Settings.Add(key, value);
        }

        /// <summary>
        /// Cancels and closes the form.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Resets all settings to default values.
        /// </summary>
        private void btnResetDefaults_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "This will reset all settings to their default values.\n\n" +
                "Are you sure you want to continue?",
                "Reset to Defaults",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // OCR defaults
                cmbLanguage.Text = "slv - Slovenian";
                cmbEngineMode.Text = "Default";
                txtTessdataPath.Text = @".\tessdata";

                // Timing defaults
                nudPageTurnDelay.Value = 250;
                nudScreenshotDelay.Value = 250;
                nudFormCloseDelay.Value = 250;

                // Batch defaults
                nudMaxReadAttempts.Value = 4;
                nudMaxPages.Value = 100000;

                // UI defaults
                nudProgressInterval.Value = 10;
                chkShowTimestamps.Checked = true;
                chkEnableLogging.Checked = false;

                MessageBox.Show(
                    "Default values have been loaded.\n\n" +
                    "Click 'Save' to apply these settings.",
                    "Defaults Loaded",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Browse for tessdata folder.
        /// </summary>
        private void btnBrowseTessdata_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Select the tessdata folder containing language data files:";
            folderBrowserDialog1.ShowNewFolderButton = false;
            
            if (!string.IsNullOrEmpty(txtTessdataPath.Text) && System.IO.Directory.Exists(txtTessdataPath.Text))
            {
                folderBrowserDialog1.SelectedPath = System.IO.Path.GetFullPath(txtTessdataPath.Text);
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtTessdataPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}

