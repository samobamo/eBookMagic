# ?? Phase 2 Features - Proposal

## **Overview**

Phase 1 delivered essential improvements (menu bar, status bar, save/load, progress tracking). Phase 2 focuses on **quality-of-life features** that enhance productivity and user experience.

---

## **? Proposed Phase 2 Features**

### **Priority: HIGH** ??

---

### **1. Cancel/Stop Button During OCR** ??

**Problem:** Once OCR starts, user must wait for completion or kill the process.

**Solution:** Add cancel button and cancellation token support.

```
???????????????????????????????????????????????????
? Processing... 45/100 pages                      ?
? ???????????????????? 45%                        ?
?                                                  ?
?              [?? Stop OCR]                       ?
???????????????????????????????????????????????????
```

**Implementation:**
```csharp
private CancellationTokenSource _cancellationTokenSource;

private async void button5_Click(object sender, EventArgs e)
{
    _cancellationTokenSource = new CancellationTokenSource();
    buttonStop.Visible = true;
    
    while (!readCompleted && index < pagesToRead)
    {
        if (_cancellationTokenSource.Token.IsCancellationRequested)
        {
            UpdateStatus("OCR cancelled by user");
            break;
        }
        
        await Task.Delay(AppConfig.PageTurnDelayMs, _cancellationTokenSource.Token);
        // ... processing
    }
}

private void buttonStop_Click(object sender, EventArgs e)
{
    _cancellationTokenSource?.Cancel();
    buttonStop.Enabled = false;
}
```

**Benefits:**
- ? User can stop anytime
- ? Graceful cancellation
- ? Saves partial results
- ? Better UX control

**Effort:** Low  
**Impact:** High  
**Priority:** ?????

---

### **2. Recent Files Menu (MRU)** ??

**Problem:** Users must navigate file system to reopen recent extractions.

**Solution:** Track last 5-10 opened/saved files.

```
File
?? New
?? Open...
?? Save As...
??????????????
?? Recent Files ?
?  ?? OCR_Extract_20250411_223015.txt
?  ?? MyBook_Chapter1.txt
?  ?? Chapter2_Notes.txt
?  ?? BookExtract_2025.txt
??????????????
?? Exit
```

**Implementation:**
```csharp
// In app.config or Settings
<userSettings>
  <setting name="RecentFiles" serializeAs="Xml">
    <value>
      <ArrayOfString>
        <string>C:\path\to\file1.txt</string>
        <string>C:\path\to\file2.txt</string>
      </ArrayOfString>
    </value>
  </setting>
</userSettings>

// Code
private void AddToRecentFiles(string filePath)
{
    var recent = Properties.Settings.Default.RecentFiles ?? new System.Collections.Specialized.StringCollection();
    
    if (recent.Contains(filePath))
        recent.Remove(filePath);
    
    recent.Insert(0, filePath);
    
    // Keep only last 10
    while (recent.Count > 10)
        recent.RemoveAt(recent.Count - 1);
    
    Properties.Settings.Default.RecentFiles = recent;
    Properties.Settings.Default.Save();
    
    UpdateRecentFilesMenu();
}

private void OpenRecentFile(string filePath)
{
    if (File.Exists(filePath))
    {
        richTextBox1.Text = File.ReadAllText(filePath);
        UpdateStatus($"Loaded {Path.GetFileName(filePath)}");
    }
    else
    {
        MessageBox.Show("File not found. It may have been moved or deleted.");
        RemoveFromRecentFiles(filePath);
    }
}
```

**Benefits:**
- ? Quick access to recent work
- ? Productivity boost
- ? Standard UX pattern
- ? Auto-cleans missing files

**Effort:** Low-Medium  
**Impact:** Medium-High  
**Priority:** ????

---

### **3. Statistics Panel** ??

**Problem:** Users don't know detailed metrics about OCR results.

**Solution:** Add expandable statistics panel.

```
?? OCR Statistics ?????????????????????????????????
? ? Show Statistics                               ?
???????????????????????????????????????????????????
? Pages Processed:      125                       ?
? Characters Extracted: 45,123                    ?
? Words Extracted:      7,234                     ?
? Lines Extracted:      1,456                     ?
? Average Page Length:  361 chars/page            ?
? Processing Time:      00:04:32                  ?
? Processing Speed:     27.5 pages/min            ?
? Average Confidence:   87.3% (if available)      ?
???????????????????????????????????????????????????
```

**Implementation:**
```csharp
private class OcrStatistics
{
    public int PagesProcessed { get; set; }
    public int CharactersExtracted { get; set; }
    public int WordsExtracted { get; set; }
    public int LinesExtracted { get; set; }
    public TimeSpan ProcessingTime { get; set; }
    public DateTime StartTime { get; set; }
    
    public double PagesPerMinute => 
        ProcessingTime.TotalMinutes > 0 
            ? PagesProcessed / ProcessingTime.TotalMinutes 
            : 0;
    
    public double AveragePageLength => 
        PagesProcessed > 0 
            ? (double)CharactersExtracted / PagesProcessed 
            : 0;
}

private void UpdateStatistics()
{
    var stats = new OcrStatistics
    {
        PagesProcessed = index,
        CharactersExtracted = richTextBox1.Text.Length,
        WordsExtracted = richTextBox1.Text.Split(new[] { ' ', '\n', '\r', '\t' }, 
            StringSplitOptions.RemoveEmptyEntries).Length,
        LinesExtracted = richTextBox1.Lines.Length,
        ProcessingTime = DateTime.Now - _ocrStartTime
    };
    
    statsLabel.Text = $"Pages: {stats.PagesProcessed} | " +
                     $"Words: {stats.WordsExtracted:N0} | " +
                     $"Chars: {stats.CharactersExtracted:N0} | " +
                     $"Speed: {stats.PagesPerMinute:F1} pages/min";
}
```

**Benefits:**
- ? Insight into OCR performance
- ? Track efficiency
- ? Quality estimation
- ? Useful for comparing settings

**Effort:** Medium  
**Impact:** Medium  
**Priority:** ????

---

### **4. Find & Replace Dialog** ??

**Problem:** Can't search within extracted text.

**Solution:** Add simple find/replace functionality.

```
?? Find ???????????????????????????????????????
? Find what:    [Lorem ipsum          ]      ?
? Replace with: [                      ]      ?
?                                             ?
? ? Match case                               ?
? ? Whole words only                         ?
?                                             ?
? [Find Next] [Replace] [Replace All]        ?
???????????????????????????????????????????????
```

**Implementation:**
```csharp
public class FindReplaceForm : Form
{
    public RichTextBox TargetTextBox { get; set; }
    private int _lastFoundIndex = -1;
    
    private void btnFindNext_Click(object sender, EventArgs e)
    {
        string searchText = txtFind.Text;
        if (string.IsNullOrEmpty(searchText)) return;
        
        StringComparison comparison = chkMatchCase.Checked 
            ? StringComparison.Ordinal 
            : StringComparison.OrdinalIgnoreCase;
        
        int startIndex = _lastFoundIndex >= 0 
            ? _lastFoundIndex + 1 
            : 0;
        
        int foundIndex = TargetTextBox.Text.IndexOf(searchText, startIndex, comparison);
        
        if (foundIndex >= 0)
        {
            TargetTextBox.Select(foundIndex, searchText.Length);
            TargetTextBox.ScrollToCaret();
            _lastFoundIndex = foundIndex;
        }
        else
        {
            MessageBox.Show("No more occurrences found.", "Find");
            _lastFoundIndex = -1;
        }
    }
    
    private void btnReplaceAll_Click(object sender, EventArgs e)
    {
        int count = 0;
        string text = TargetTextBox.Text;
        string find = txtFind.Text;
        string replace = txtReplace.Text;
        
        StringComparison comparison = chkMatchCase.Checked 
            ? StringComparison.Ordinal 
            : StringComparison.OrdinalIgnoreCase;
        
        // Simple replace (can be optimized)
        int index = 0;
        while ((index = text.IndexOf(find, index, comparison)) >= 0)
        {
            text = text.Remove(index, find.Length).Insert(index, replace);
            index += replace.Length;
            count++;
        }
        
        TargetTextBox.Text = text;
        MessageBox.Show($"Replaced {count} occurrence(s).", "Replace All");
    }
}

// In Form1 menu
private void findToolStripMenuItem_Click(object sender, EventArgs e)
{
    using (var findForm = new FindReplaceForm())
    {
        findForm.TargetTextBox = richTextBox1;
        findForm.ShowDialog();
    }
}
```

**Benefits:**
- ? Standard text editing feature
- ? Fix common OCR errors quickly
- ? Productivity enhancement

**Effort:** Medium  
**Impact:** Medium  
**Priority:** ???

---

### **5. Auto-Save Draft** ??

**Problem:** User loses work if app crashes or they forget to save.

**Solution:** Auto-save to temp location every N minutes.

**Implementation:**
```csharp
private System.Windows.Forms.Timer _autoSaveTimer;
private string _draftFilePath;

private void InitializeAutoSave()
{
    _draftFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "eBookMagic", "draft.txt");
    
    Directory.CreateDirectory(Path.GetDirectoryName(_draftFilePath));
    
    _autoSaveTimer = new System.Windows.Forms.Timer();
    _autoSaveTimer.Interval = 60000; // 1 minute
    _autoSaveTimer.Tick += AutoSaveTimer_Tick;
    _autoSaveTimer.Start();
    
    // Load draft on startup if exists
    if (File.Exists(_draftFilePath))
    {
        var result = MessageBox.Show(
            "A draft from a previous session was found. Load it?",
            "Recover Draft",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
        
        if (result == DialogResult.Yes)
        {
            richTextBox1.Text = File.ReadAllText(_draftFilePath);
            UpdateStatus("Draft recovered");
        }
    }
}

private void AutoSaveTimer_Tick(object sender, EventArgs e)
{
    try
    {
        if (richTextBox1.Text.Length > 0)
        {
            File.WriteAllText(_draftFilePath, richTextBox1.Text);
            // Silently save - no UI feedback
        }
    }
    catch
    {
        // Fail silently for auto-save
    }
}

protected override void OnFormClosing(FormClosingEventArgs e)
{
    // Delete draft on normal close
    try
    {
        if (File.Exists(_draftFilePath))
            File.Delete(_draftFilePath);
    }
    catch { }
    
    base.OnFormClosing(e);
}
```

**Benefits:**
- ? Prevents data loss
- ? Crash recovery
- ? Peace of mind
- ? Professional feature

**Effort:** Low  
**Impact:** High  
**Priority:** ????

---

## **Priority: MEDIUM** ??

---

### **6. Keyboard Shortcut Customization** ??

**Problem:** Power users may want different shortcuts.

**Solution:** Allow customization of keyboard shortcuts.

```
?? Keyboard Shortcuts ?????????????????????????
? Action              Current      Custom     ?
???????????????????????????????????????????????
? Save               Ctrl+S       [Change]    ?
? Open               Ctrl+O       [Change]    ?
? Export             Ctrl+E       [Change]    ?
? Clear              Ctrl+L       [Change]    ?
? Find               Ctrl+F       [Change]    ?
? Settings           Ctrl+,       [Change]    ?
?                                             ?
?         [Reset to Defaults]  [OK] [Cancel] ?
???????????????????????????????????????????????
```

**Effort:** High  
**Impact:** Low  
**Priority:** ??

---

### **7. Export Format Options** ??

**Problem:** Only Word export available.

**Solution:** Add multiple export formats.

```
Export ?
?? ?? Word Document (.docx)
?? ?? Plain Text (.txt)
?? ?? Markdown (.md)
?? ?? PDF
?? ?? HTML
?? ?? Copy to Clipboard (formatted)
```

**Implementation:**
```csharp
private void ExportToMarkdown()
{
    using (var saveDialog = new SaveFileDialog())
    {
        saveDialog.Filter = "Markdown Files (*.md)|*.md";
        saveDialog.DefaultExt = "md";
        
        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            // Add markdown formatting
            string markdown = $"# OCR Extract - {DateTime.Now:yyyy-MM-dd}\n\n";
            markdown += richTextBox1.Text;
            
            File.WriteAllText(saveDialog.FileName, markdown);
        }
    }
}

private void ExportToPdf()
{
    // Requires PDF library like iTextSharp
    // Or print-to-PDF using PrintDocument
}
```

**Effort:** Medium-High  
**Impact:** Medium  
**Priority:** ???

---

### **8. OCR Quality Presets** ???

**Problem:** Users don't know which settings to use.

**Solution:** Add preset quality modes.

```
?? OCR Quality Mode ???????????????????????????
? ? Fast          (150ms delay, LSTM only)   ?
? ? Balanced      (250ms, Default engine)    ?
? ? Accurate      (400ms, Both engines)      ?
? ? Maximum       (600ms, Best settings)     ?
? ? Custom...     (Advanced users)           ?
???????????????????????????????????????????????
```

**Implementation:**
```csharp
public enum OcrQualityPreset
{
    Fast,       // 150ms, LstmOnly
    Balanced,   // 250ms, Default
    Accurate,   // 400ms, TesseractAndLstm
    Maximum,    // 600ms, Best settings
    Custom      // User-defined
}

private void ApplyQualityPreset(OcrQualityPreset preset)
{
    switch (preset)
    {
        case OcrQualityPreset.Fast:
            AppConfig.PageTurnDelayMs = 150;
            AppConfig.OcrEngineMode = EngineMode.LstmOnly;
            break;
        case OcrQualityPreset.Balanced:
            AppConfig.PageTurnDelayMs = 250;
            AppConfig.OcrEngineMode = EngineMode.Default;
            break;
        case OcrQualityPreset.Accurate:
            AppConfig.PageTurnDelayMs = 400;
            AppConfig.OcrEngineMode = EngineMode.TesseractAndLstm;
            break;
        case OcrQualityPreset.Maximum:
            AppConfig.PageTurnDelayMs = 600;
            AppConfig.OcrEngineMode = EngineMode.TesseractAndLstm;
            AppConfig.ScreenshotDelayMs = 400;
            break;
    }
}
```

**Effort:** Low  
**Impact:** High  
**Priority:** ????

---

### **9. Language Quick-Switch** ??

**Problem:** Changing language requires opening Settings.

**Solution:** Add language dropdown to main toolbar.

```
???????????????????????????????????????????????
? Pages:[  ] Max:[  ] Lang:[slv ?] [??Read!] ?
???????????????????????????????????????????????

Language dropdown:
?? eng - English
?? slv - Slovenian  ?
?? deu - German
?? spa - Spanish
?? fra - French
?? More languages...
```

**Implementation:**
```csharp
private ComboBox cmbLanguage;

private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
{
    string langCode = cmbLanguage.SelectedItem.ToString().Split(' ')[0];
    
    // Update config
    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
    config.AppSettings.Settings["OcrLanguage"].Value = langCode;
    config.Save(ConfigurationSaveMode.Modified);
    
    // Reinitialize Tesseract
    tesseractEngine?.Dispose();
    tesseractEngine = new TesseractEngine(AppConfig.TessdataPath, langCode, AppConfig.OcrEngineMode);
    
    UpdateStatus($"Language changed to {langCode}");
}
```

**Effort:** Low  
**Impact:** Medium  
**Priority:** ???

---

### **10. Text Post-Processing** ??

**Problem:** OCR often produces common errors.

**Solution:** Add automatic text cleanup options.

```
Edit
?? Clear
?? Select All
??????????????
?? Post-Processing ?
?  ?? Fix Line Breaks
?  ?? Remove Hyphens
?  ?? Fix Common OCR Errors
?  ?? Remove Extra Spaces
?  ?? Smart Quotes
```

**Implementation:**
```csharp
private string FixLineBreaks(string text)
{
    // Join broken lines
    return Regex.Replace(text, @"([a-z])\s*\n\s*([a-z])", "$1 $2");
}

private string RemoveHyphens(string text)
{
    // Remove end-of-line hyphens
    return Regex.Replace(text, @"-\s*\n\s*", "");
}

private string FixCommonOcrErrors(string text)
{
    // Common OCR mistakes
    text = text.Replace("rn", "m");  // 'rn' often misread as 'm'
    text = text.Replace("vv", "w");  // 'vv' often misread as 'w'
    text = text.Replace("l1", "ll"); // '1' often misread in 'll'
    // Add more common errors
    return text;
}

private string RemoveExtraSpaces(string text)
{
    return Regex.Replace(text, @"\s+", " ");
}
```

**Effort:** Medium  
**Impact:** High  
**Priority:** ????

---

## **?? Phase 2 Summary**

### **High Priority (Implement First)**

| Feature | Effort | Impact | Priority |
|---------|--------|--------|----------|
| **Cancel/Stop Button** | Low | High | ????? |
| **Recent Files Menu** | Low-Med | Med-High | ???? |
| **Statistics Panel** | Medium | Medium | ???? |
| **Auto-Save Draft** | Low | High | ???? |
| **OCR Quality Presets** | Low | High | ???? |
| **Text Post-Processing** | Medium | High | ???? |

### **Medium Priority (Nice to Have)**

| Feature | Effort | Impact | Priority |
|---------|--------|--------|----------|
| **Find & Replace** | Medium | Medium | ??? |
| **Language Quick-Switch** | Low | Medium | ??? |
| **Export Format Options** | Med-High | Medium | ??? |
| **Keyboard Customization** | High | Low | ?? |

---

## **?? Recommended Implementation Order**

### **Quick Wins (1-2 days)**
1. Cancel/Stop Button
2. Auto-Save Draft
3. Recent Files Menu

### **Week 1**
4. Statistics Panel
5. OCR Quality Presets
6. Language Quick-Switch

### **Week 2**
7. Text Post-Processing
8. Find & Replace
9. Export Format Options

---

## **?? Which Features Would You Like?**

Please let me know:
1. Which Phase 2 features interest you most?
2. Should I implement a specific feature?
3. Would you like a different feature not listed here?

I can start implementing any of these features immediately!
