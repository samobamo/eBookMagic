# eBookMagic - GUI Settings Editor User Guide

## ??? Easy Settings Management

eBookMagic now includes a **graphical settings editor** - no need to edit configuration files manually!

---

## ?? How to Access Settings

You can open the Settings dialog in **three ways**:

### **Method 1: Settings Button (Recommended)**
1. Look at the top toolbar
2. Click the **"Settings..."** button (next to Export)
3. The Settings dialog opens

### **Method 2: System Tray Menu**
1. Right-click the eBookMagic icon in the **system tray** (notification area)
2. Click **"Settings..."**
3. The Settings dialog opens

### **Method 3: Keyboard Shortcut** (if implemented)
- Press **Ctrl+,** (comma) - *Future feature*

---

## ?? Settings Dialog Overview

The Settings dialog is organized into **four sections**:

```
???????????????????????????????????????????????
?  eBookMagic Settings                    [×] ?
???????????????????????????????????????????????
?                                             ?
?  ?? OCR Engine Settings                     ?
?     - Language selection                    ?
?     - Engine mode                           ?
?     - Tessdata folder path                  ?
?                                             ?
?  ?? Timing Settings                         ?
?     - Page turn delay                       ?
?     - Screenshot delay                      ?
?     - Form close delay                      ?
?                                             ?
?  ?? Batch Processing Settings               ?
?     - Max empty pages                       ?
?     - Default pages to read                 ?
?                                             ?
?  ?? User Interface Settings                 ?
?     - Progress update interval              ?
?     - Show timestamps                       ?
?     - Enable logging                        ?
?                                             ?
?  [Reset to Defaults]      [Save] [Cancel]  ?
???????????????????????????????????????????????
```

---

## ?? Settings Explained

### **?? OCR Engine Settings**

#### **Language**
Choose the language for OCR recognition:
- **English** (eng)
- **Slovenian** (slv) - Default
- **German** (deu)
- **Spanish** (spa)
- **French** (fra)
- **Italian** (ita)
- **Portuguese** (por)
- **Russian** (rus)
- **Polish** (pol)
- **Dutch** (nld)

**?? Note:** You must have the corresponding `.traineddata` file in the tessdata folder!

**How to add more languages:**
1. Download from: https://github.com/tesseract-ocr/tessdata
2. Place the `.traineddata` file in the tessdata folder
3. Type the language code manually (e.g., "jpn", "ara")

---

#### **Engine Mode**
Choose Tesseract processing mode:
- **Default** - Balanced (Recommended)
- **TesseractOnly** - Legacy engine only
- **LstmOnly** - Neural network only (faster, less accurate)
- **TesseractAndLstm** - Both engines combined (slower, more accurate)

**?? Tip:** Use "Default" unless you have specific accuracy/speed requirements.

---

#### **Tessdata Path**
Location of language data files.
- Default: `.\tessdata` (same folder as application)
- Click **"Browse..."** to select a different folder
- Folder must contain `.traineddata` files

---

### **?? Timing Settings (milliseconds)**

#### **Page Turn Delay**
Time to wait **between page turns** during batch processing.

| Value | Speed | Use Case |
|-------|-------|----------|
| 100-150ms | ? Fast | Modern PC, quick eReaders |
| 200-300ms | ?? Balanced | Default, most systems |
| 350-500ms | ?? Slow | Older PC, ensure accuracy |

**Default:** 250ms

**?? Tip:** If pages are being missed, increase by 50-100ms

---

#### **Screenshot Delay**
Time to wait **before** taking a screenshot.

| Value | Use Case |
|-------|----------|
| 150-200ms | Fast systems |
| 250-300ms | Default |
| 350-400ms | Slow rendering |

**Default:** 250ms

---

#### **Form Close Delay**
Internal timing for selection overlay to disappear.
- **Range:** 100-1000ms
- **Default:** 250ms
- **?? Tip:** Only change if selection border appears in screenshots

---

### **?? Default Batch Processing Settings**

#### **Max Empty Pages**
How many consecutive **blank/duplicate pages** before stopping automatically.

| Value | Behavior |
|-------|----------|
| 1-2 | Stop quickly (may stop too early) |
| 3-5 | Balanced |
| 6-10 | Keep reading even with some blanks |

**Default:** 4 pages

**?? Tip:** Increase if book has intentional blank pages (chapter breaks)

---

#### **Default Pages to Read**
Maximum pages to process in one batch (if input field is empty).

| Value | Use Case |
|-------|----------|
| 100-500 | Short articles/chapters |
| 1000-5000 | Standard books |
| 100000+ | Full books, auto-stop on blanks |

**Default:** 100000 pages

**?? Tip:** This is a safety limit. Batch usually stops on blank pages first.

---

### **?? User Interface Settings**

#### **Progress Update Interval**
Show progress message every **N pages**.

| Value | Output Frequency |
|-------|-----------------|
| 5-10 | Frequent updates (detailed tracking) |
| 10-25 | Balanced |
| 50-100 | Minimal updates (less clutter) |

**Default:** 10 pages

**Example Output:**
```
[Progress: 10 pages processed]
[Progress: 20 pages processed]
[Progress: 30 pages processed]
```

---

#### **Show Timestamps**
Display start/end times in OCR output.

**Enabled:**
```
--- Starting OCR at 2025-04-11 22:30:15 ---
[Your extracted text here]
--- Completed at 2025-04-11 22:35:42. Total pages: 125 ---
```

**Disabled:**
```
[Your extracted text here]
```

**Default:** Enabled ?

---

#### **Enable Logging**
*(Future feature - currently has no effect)*
- Will log application events to file
- Useful for troubleshooting

**Default:** Disabled

---

## ?? Common Tasks

### **Change Language to English**
1. Open Settings
2. In "OCR Engine Settings", select **"eng - English"**
3. Click **Save**
4. Restart the application

---

### **Speed Up Processing**
1. Open Settings
2. Set **Page Turn Delay** to **150ms**
3. Set **Screenshot Delay** to **200ms**
4. Click **Save**
5. Restart the application

---

### **Reduce Progress Messages**
1. Open Settings
2. Set **Progress Update Interval** to **25** or **50**
3. Click **Save**
4. Restart (or changes apply immediately for UI settings)

---

### **High Accuracy Mode**
1. Open Settings
2. Set **Page Turn Delay** to **400ms**
3. Set **Max Empty Pages** to **6**
4. Set **Engine Mode** to **"TesseractAndLstm"**
5. Click **Save**
6. Restart the application

---

## ?? Reset to Defaults

If you've made changes and want to start fresh:

1. Open Settings
2. Click **"Reset to Defaults"** button (bottom-left)
3. Confirm the action
4. Click **Save** to apply
5. Restart the application

**All settings will revert to:**
- Language: Slovenian (slv)
- Delays: 250ms
- Max Empty Pages: 4
- Progress Interval: 10
- Timestamps: Enabled

---

## ?? Saving Settings

### **Manual Save**
1. Make your changes in the Settings dialog
2. Click **"Save"** button
3. Settings are saved to `OCR.exe.config`
4. Restart eBookMagic for full effect

### **What Happens When You Save**
```
? Settings written to OCR.exe.config
? Configuration file updated
? Confirmation message shown
?? Restart required for OCR engine changes
```

**Some settings take effect immediately:**
- Progress update interval
- Show timestamps

**Most settings require restart:**
- Language changes
- Timing changes
- Batch defaults

---

## ? Canceling Changes

If you change your mind:
1. Click **"Cancel"** button, or
2. Press **Escape** key, or
3. Click the **X** button

**All changes are discarded.**

---

## ?? Validation & Errors

### **Tessdata Folder Not Found**
If you change the tessdata path to a folder that doesn't exist:
```
?? The tessdata folder does not exist:
   C:\MyPath\tessdata

   Do you want to save anyway?
   [Yes]  [No]
```

- Click **No** to fix the path first
- Click **Yes** to save anyway (app may fail to start!)

---

### **Missing Language File**
If you select a language without the corresponding file:
```
? Language data file not found: 
   .\tessdata\eng.traineddata

   Please download 'eng.traineddata' and place 
   it in the tessdata folder.
```

**Solution:**
1. Download the file from: https://github.com/tesseract-ocr/tessdata
2. Place it in the tessdata folder
3. Try again

---

## ?? Quick Reference Table

| Setting | Location | Default | Range | Restart Needed? |
|---------|----------|---------|-------|----------------|
| Language | OCR Engine | slv | any | ? Yes |
| Engine Mode | OCR Engine | Default | 4 options | ? Yes |
| Page Turn Delay | Timing | 250ms | 100-2000ms | ? Yes |
| Screenshot Delay | Timing | 250ms | 100-1000ms | ? Yes |
| Max Empty Pages | Batch | 4 | 1-20 | ? Yes |
| Default Pages | Batch | 100000 | 10-999999 | ? Yes |
| Progress Interval | UI | 10 | 1-100 | ? No |
| Show Timestamps | UI | On | On/Off | ? No |

---

## ?? Pro Tips

### **Optimize for Your System**
1. Start with defaults
2. Run a small test (10-20 pages)
3. If pages are missed: **Increase delays**
4. If too slow: **Decrease delays**
5. Find your sweet spot!

### **Create Profiles**
After finding your optimal settings:
1. Save `OCR.exe.config` as `OCR.exe.config.fast`
2. Create different configs for different scenarios
3. Copy the one you need when switching

### **Backup Settings**
Before making major changes:
1. Copy `OCR.exe.config` to `OCR.exe.config.backup`
2. If something breaks, restore the backup

---

## ?? Troubleshooting

### **Settings Dialog Won't Open**
- Make sure application is not in the middle of OCR processing
- Try closing and reopening eBookMagic

### **Changes Don't Apply**
- Did you click "Save"?
- Did you restart the application?
- Check that `OCR.exe.config` was modified (check timestamp)

### **Settings Reset After Update**
- When updating, back up your `OCR.exe.config`
- Restore it after update

---

## ?? Related Documentation

- **CONFIGURATION.md** - Technical reference
- **USER_SETTINGS_GUIDE.md** - Manual XML editing guide
- **README.md** - General application info

---

**Enjoy easy configuration with eBookMagic! ??**

If you prefer manual editing, you can still edit `OCR.exe.config` directly with Notepad.
Both methods work and save to the same file.
