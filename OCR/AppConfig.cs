using System;
using System.Configuration;
using Tesseract;

namespace OCR
{
    /// <summary>
    /// Centralized configuration management for the OCR application.
    /// Reads settings from App.config and provides strongly-typed access.
    /// </summary>
    public static class AppConfig
    {
        #region OCR Engine Settings

        /// <summary>
        /// Path to the tessdata folder containing language data files.
        /// Default: ".\tessdata"
        /// </summary>
        public static string TessdataPath =>
            ConfigurationManager.AppSettings["TessdataPath"] ?? @".\tessdata";

        /// <summary>
        /// OCR language code (e.g., "eng", "slv", "deu").
        /// Default: "slv" (Slovenian)
        /// </summary>
        public static string OcrLanguage =>
            ConfigurationManager.AppSettings["OcrLanguage"] ?? "slv";

        /// <summary>
        /// Tesseract engine mode.
        /// Default: EngineMode.Default
        /// </summary>
        public static EngineMode OcrEngineMode
        {
            get
            {
                string mode = ConfigurationManager.AppSettings["OcrEngineMode"];
                if (Enum.TryParse<EngineMode>(mode, out var engineMode))
                    return engineMode;
                return EngineMode.Default;
            }
        }

        #endregion

        #region Default OCR Batch Settings

        /// <summary>
        /// Default maximum number of consecutive empty/duplicate pages before stopping.
        /// Default: 4
        /// </summary>
        public static int DefaultMaxReadAttempts =>
            GetIntSetting("DefaultMaxReadAttempts", 4);

        /// <summary>
        /// Default maximum number of pages to read in a batch.
        /// Default: 100000
        /// </summary>
        public static int DefaultPagesToRead =>
            GetIntSetting("DefaultPagesToRead", 100000);

        #endregion

        #region Timing Settings

        /// <summary>
        /// Delay in milliseconds before taking a screenshot.
        /// Default: 250ms
        /// </summary>
        public static int ScreenshotDelayMs =>
            GetIntSetting("ScreenshotDelayMs", 250);

        /// <summary>
        /// Delay in milliseconds between page turns during batch processing.
        /// Default: 250ms
        /// </summary>
        public static int PageTurnDelayMs =>
            GetIntSetting("PageTurnDelayMs", 250);

        /// <summary>
        /// Delay in milliseconds to allow form to disappear before screenshot.
        /// Default: 250ms
        /// </summary>
        public static int FormCloseDelayMs =>
            GetIntSetting("FormCloseDelayMs", 250);

        #endregion

        #region UI Settings

        public static string LastCapturedRegion =>
            ConfigurationManager.AppSettings["LastCapturedRegion"] ?? "100,100,100,100";

        /// <summary>
        /// Number of pages to process before updating progress indicator.
        /// Default: 10
        /// </summary>
        public static int ProgressUpdateInterval =>
            GetIntSetting("ProgressUpdateInterval", 10);

        /// <summary>
        /// Whether to show timestamps in OCR output.
        /// Default: true
        /// </summary>
        public static bool ShowTimestamps =>
            GetBoolSetting("ShowTimestamps", true);

        /// <summary>
        /// Whether to enable application logging.
        /// Default: false
        /// </summary>
        public static bool EnableLogging =>
            GetBoolSetting("EnableLogging", false);

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets an integer setting from configuration with a fallback default value.
        /// </summary>
        private static int GetIntSetting(string key, int defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (int.TryParse(value, out int result) && result > 0)
                return result;
            return defaultValue;
        }

        /// <summary>
        /// Gets a boolean setting from configuration with a fallback default value.
        /// </summary>
        private static bool GetBoolSetting(string key, bool defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (bool.TryParse(value, out bool result))
                return result;
            return defaultValue;
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validates that all required configuration settings are present and valid.
        /// </summary>
        /// <param name="errorMessage">Error message if validation fails.</param>
        /// <returns>True if configuration is valid, false otherwise.</returns>
        public static bool ValidateConfiguration(out string errorMessage)
        {
            // Check if tessdata path exists
            if (!System.IO.Directory.Exists(TessdataPath))
            {
                errorMessage = $"Tessdata folder not found at: {System.IO.Path.GetFullPath(TessdataPath)}\n\n" +
                              "Please ensure the tessdata folder exists with the required language data files.";
                return false;
            }

            // Check if language data file exists
            string langFile = System.IO.Path.Combine(TessdataPath, $"{OcrLanguage}.traineddata");
            if (!System.IO.File.Exists(langFile))
            {
                errorMessage = $"Language data file not found: {langFile}\n\n" +
                              $"Please download '{OcrLanguage}.traineddata' and place it in the tessdata folder.";
                return false;
            }

            errorMessage = null;
            return true;
        }

        #endregion
    }
}
