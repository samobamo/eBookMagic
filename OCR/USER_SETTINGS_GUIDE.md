# eBookMagic - User Settings Guide

## How to Change Application Settings

You can customize eBookMagic's behavior **without reinstalling or recompiling** by editing the configuration file.

---

## ?? **Step 1: Find the Configuration File**

The settings file is located in the **same folder** as `OCR.exe`:

```
?? eBookMagic Installation Folder
??? ?? OCR.exe                 ? Your application
??? ?? OCR.exe.config         ? SETTINGS FILE (Edit this!)
??? ?? tessdata\
?   ??? slv.traineddata
?   ??? eng.traineddata
??? ...other files
```

**Common locations:**
- Desktop shortcut: Right-click ? "Open file location"
- Start Menu: Right-click ? "Open file location"
- Default: `C:\Program Files\eBookMagic\`

---

## ?? **Step 2: Edit the Configuration File**

### **Option A: Using Notepad (Easiest)**

1. **Right-click** on `OCR.exe.config`
2. Select **"Edit"** or **"Open with ? Notepad"**
3. Find the setting you want to change (see examples below)
4. **Save** the file (Ctrl+S)
5. **Restart** eBookMagic

### **Option B: Using Any Text Editor**

- Notepad++
- Visual Studio Code
- Sublime Text
- Any plain text editor

**?? Important:** 
- Save as **plain text** (not Word document!)
- Keep the file named `OCR.exe.config`
- Don't change the XML structure, only the `value="..."` parts

---

## ?? **Common Settings to Change**

### **Change OCR Language**

Find this line:
```xml
<add key="OcrLanguage" value="slv" />
```

Change to:
```xml
<add key="OcrLanguage" value="eng" />  <!-- English -->
<add key="OcrLanguage" value="deu" />  <!-- German -->
<add key="OcrLanguage" value="spa" />  <!-- Spanish -->
<add key="OcrLanguage" value="fra" />  <!-- French -->
```

**?? Note:** You must have the corresponding `.traineddata` file in the `tessdata` folder!

---

### **Speed Up Page Processing**

For **faster** eBook reading (modern computers):

```xml
<add key="PageTurnDelayMs" value="150" />
<add key="ScreenshotDelayMs" value="200" />
```

For **slower** or more accurate reading:

```xml
<add key="PageTurnDelayMs" value="400" />
<add key="ScreenshotDelayMs" value="350" />
```

---

### **Reduce Progress Messages**

If you find progress messages annoying, show them less often:

```xml
<add key="ProgressUpdateInterval" value="25" />  <!-- Every 25 pages -->
<add key="ProgressUpdateInterval" value="50" />  <!-- Every 50 pages -->
```

Or more frequently for detailed tracking:

```xml
<add key="ProgressUpdateInterval" value="5" />   <!-- Every 5 pages -->
```

---

### **Hide Timestamps**

Remove start/end timestamps from the output:

```xml
<add key="ShowTimestamps" value="false" />
```

---

### **Change Default Values**

When you leave the input fields empty, these defaults are used:

```xml
<!-- Default pages to read if field is empty -->
<add key="DefaultPagesToRead" value="100000" />

<!-- Stop after this many blank pages in a row -->
<add key="DefaultMaxReadAttempts" value="4" />
```

---

## ?? **Example Configurations**

### **Fast English Mode**
```xml
<add key="OcrLanguage" value="eng" />
<add key="PageTurnDelayMs" value="150" />
<add key="ScreenshotDelayMs" value="200" />
<add key="ProgressUpdateInterval" value="20" />
<add key="ShowTimestamps" value="false" />
```

### **High Accuracy Slovenian Mode**
```xml
<add key="OcrLanguage" value="slv" />
<add key="PageTurnDelayMs" value="400" />
<add key="ScreenshotDelayMs" value="350" />
<add key="DefaultMaxReadAttempts" value="6" />
<add key="ShowTimestamps" value="true" />
```

### **Minimal Output Mode**
```xml
<add key="ProgressUpdateInterval" value="100" />
<add key="ShowTimestamps" value="false" />
```

---

## ??? **All Available Settings**

| Setting | What It Does | Default | Range |
|---------|-------------|---------|-------|
| `OcrLanguage` | Language for OCR recognition | `slv` | `eng`, `slv`, `deu`, etc. |
| `PageTurnDelayMs` | Pause between pages (milliseconds) | `250` | 100-1000ms |
| `ScreenshotDelayMs` | Delay before screenshot | `250` | 100-500ms |
| `DefaultPagesToRead` | Max pages if field is empty | `100000` | Any number |
| `DefaultMaxReadAttempts` | Blank pages before stopping | `4` | 1-10 |
| `ProgressUpdateInterval` | Show progress every N pages | `10` | 1-100 |
| `ShowTimestamps` | Show start/end times | `true` | `true` / `false` |
| `TessdataPath` | Location of language files | `.\tessdata` | Any folder path |
| `FormCloseDelayMs` | Internal timing | `250` | 100-500ms |

---

## ? **Troubleshooting**

### **Problem: Settings Don't Change**

? **Checklist:**
1. Did you edit `OCR.exe.config` (not `App.config`)?
2. Did you **save** the file after editing?
3. Did you **restart** the application?
4. Is the application running from the correct folder?

### **Problem: Application Won't Start**

**Possible Causes:**
- Syntax error in XML (missing quote, bracket, etc.)
- Invalid setting value

**Solution:**
1. Close the application
2. Delete or rename `OCR.exe.config`
3. Copy a fresh version from backup or reinstall
4. The app will use built-in defaults if config is missing

### **Problem: "Language data file not found"**

**Solution:**
1. Download the language file from: https://github.com/tesseract-ocr/tessdata
2. Place `[language].traineddata` in the `tessdata` folder
3. Example: For English, you need `eng.traineddata`

---

## ?? **Backup Your Settings**

Before making changes, create a backup:

1. **Copy** `OCR.exe.config`
2. **Paste** as `OCR.exe.config.backup`
3. Edit the original
4. If something breaks, restore from backup

---

## ?? **Reset to Defaults**

If you want to reset all settings to default:

1. Delete `OCR.exe.config`
2. Copy the file below as `OCR.exe.config`
3. Or reinstall the application

**Default Configuration:**
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

## ?? **Tips for Best Results**

### **For Kindle/eReaders:**
- Start with default settings
- If pages are missed: Increase `PageTurnDelayMs` by 50-100ms
- If too slow: Decrease by 50ms

### **For Different Languages:**
- Change `OcrLanguage` to match your book
- Download corresponding `.traineddata` file
- Restart the app

### **For Long Books:**
- Set `ProgressUpdateInterval` higher (25-50)
- This reduces clutter in the output

---

## ?? **Need Help?**

If you have questions or encounter issues:

1. Check this guide
2. Review the CONFIGURATION.md file (technical details)
3. Open an issue on GitHub: https://github.com/samobamo/eBookMagic

---

**Remember:** 
? Edit `OCR.exe.config` (not source files)  
? Save changes  
? Restart the application  
? No reinstallation or rebuilding needed!

Happy eBook OCR-ing! ???
