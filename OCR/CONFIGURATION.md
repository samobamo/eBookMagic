# eBookMagic Configuration Guide

## Overview

The application now uses a centralized configuration system that allows you to customize behavior without recompiling the code.

## Configuration File Location

All settings are stored in `App.config` in the application directory.

---

## Available Settings

### ?? OCR Engine Settings

| Setting | Default | Description |
|---------|---------|-------------|
| `TessdataPath` | `.\tessdata` | Path to the folder containing Tesseract language data files |
| `OcrLanguage` | `slv` | OCR language code (e.g., `eng`, `slv`, `deu`, `spa`, `fra`) |
| `OcrEngineMode` | `Default` | Tesseract engine mode: `Default`, `TesseractOnly`, `LstmOnly`, `TesseractAndLstm` |

**Supported Languages:**
- `eng` - English
- `slv` - Slovenian
- `deu` - German
- `spa` - Spanish
- `fra` - French
- [Download more](https://github.com/tesseract-ocr/tessdata)

---

### ?? Default OCR Batch Settings

| Setting | Default | Description |
|---------|---------|-------------|
| `DefaultMaxReadAttempts` | `4` | Number of consecutive empty/duplicate pages before stopping |
| `DefaultPagesToRead` | `100000` | Maximum number of pages to read in a batch |

**Note:** These are fallback values if the user leaves the input fields empty.

---

### ?? Timing Settings (milliseconds)

| Setting | Default | Description |
|---------|---------|-------------|
| `ScreenshotDelayMs` | `250` | Delay before taking a screenshot |
| `PageTurnDelayMs` | `250` | Delay between page turns during batch processing |
| `FormCloseDelayMs` | `250` | Delay to allow selection form to disappear before screenshot |

**Tuning Tips:**
- **Faster eReaders:** Reduce to `150-200ms`
- **Slower systems:** Increase to `300-400ms`
- **High accuracy needed:** Increase `PageTurnDelayMs` to ensure pages fully load

---

### ?? UI Settings

| Setting | Default | Description |
|---------|---------|-------------|
| `ProgressUpdateInterval` | `10` | Update progress message every N pages |
| `ShowTimestamps` | `true` | Show start/end timestamps in output |
| `EnableLogging` | `false` | Enable application logging (future feature) |

---

## Example Configurations

### English eBooks (Fast)
```xml
<appSettings>
    <add key="OcrLanguage" value="eng" />
    <add key="PageTurnDelayMs" value="150" />
    <add key="ProgressUpdateInterval" value="20" />
</appSettings>
```

### Slovenian eBooks (High Accuracy)
```xml
<appSettings>
    <add key="OcrLanguage" value="slv" />
    <add key="PageTurnDelayMs" value="350" />
    <add key="DefaultMaxReadAttempts" value="5" />
</appSettings>
```

### Multi-Language Setup
If processing multiple languages, you can:
1. Keep multiple `.traineddata` files in `tessdata/`
2. Change `OcrLanguage` setting before each batch
3. Or restart the app (configuration is loaded on startup)

---

## Adding New Languages

1. Download the language data file (e.g., `fra.traineddata`) from:
   https://github.com/tesseract-ocr/tessdata

2. Place it in the `tessdata` folder

3. Update `App.config`:
   ```xml
   <add key="OcrLanguage" value="fra" />
   ```

4. Restart the application

---

## Troubleshooting

### Error: "Tessdata folder not found"
- Ensure the `tessdata` folder exists in the same directory as the .exe
- Or update `TessdataPath` to point to the correct location:
  ```xml
  <add key="TessdataPath" value="C:\MyFolder\tessdata" />
  ```

### Error: "Language data file not found"
- Download the `.traineddata` file for your language
- Place it in the `tessdata` folder
- Ensure the filename matches the language code (e.g., `slv.traineddata`)

### Pages processing too fast/slow
- Adjust `PageTurnDelayMs`:
  - Too fast? Increase by 50-100ms
  - Too slow? Decrease by 50ms
  
### Too many progress updates
- Increase `ProgressUpdateInterval` (e.g., from 10 to 25)

---

## Performance Optimization

### For Modern Systems
```xml
<add key="PageTurnDelayMs" value="150" />
<add key="ScreenshotDelayMs" value="200" />
<add key="FormCloseDelayMs" value="200" />
```

### For Older Systems
```xml
<add key="PageTurnDelayMs" value="400" />
<add key="ScreenshotDelayMs" value="300" />
<add key="FormCloseDelayMs" value="300" />
```

---

## Code Integration

The configuration is accessed through the `AppConfig` static class:

```csharp
// Example usage in code:
string language = AppConfig.OcrLanguage;
int delay = AppConfig.PageTurnDelayMs;
bool showTime = AppConfig.ShowTimestamps;
```

All configuration is loaded at application startup and validated before the OCR engine initializes.

---

## Default App.config

If you need to reset to defaults, use this template:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
    </startup>
    
    <appSettings>
        <add key="TessdataPath" value=".\tessdata" />
        <add key="OcrLanguage" value="slv" />
        <add key="OcrEngineMode" value="Default" />
        <add key="DefaultMaxReadAttempts" value="4" />
        <add key="DefaultPagesToRead" value="100000" />
        <add key="ScreenshotDelayMs" value="250" />
        <add key="PageTurnDelayMs" value="250" />
        <add key="FormCloseDelayMs" value="250" />
        <add key="ProgressUpdateInterval" value="10" />
        <add key="ShowTimestamps" value="true" />
        <add key="EnableLogging" value="false" />
    </appSettings>
</configuration>
```

---

## Support

For issues or questions, please visit:
https://github.com/samobamo/eBookMagic
