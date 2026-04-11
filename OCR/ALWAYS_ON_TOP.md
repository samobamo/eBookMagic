# Always on Top Feature - Implementation Guide

## ? Feature Overview

The **Always on Top** feature allows eBookMagic to stay above all other windows, making it easy to reference extracted text while working in other applications.

---

## ?? Use Cases

### **Scenario 1: Reference While Writing**
```
????????????????????????????????????????????
? eBookMagic (Always on Top) ?             ?
? ???????????????????????????????????????? ?
? ? "Lorem ipsum dolor sit amet..."      ? ?
? ???????????????????????????????????????? ?
????????????????????????????????????????????
       ? User can see this while...
????????????????????????????????????????????
? Microsoft Word (Behind)                  ?
? ???????????????????????????????????????? ?
? ? User types, referencing eBookMagic   ? ?
? ???????????????????????????????????????? ?
????????????????????????????????????????????
```

**Benefit:** No need to Alt+Tab back and forth!

---

### **Scenario 2: Monitor OCR Progress**
```
User restores window during OCR to check progress:
????????????????????????????????????????????
? eBookMagic (Always on Top) ?             ?
? ???????????????? 45% | 45/100 pages     ?
????????????????????????????????????????????
       ? Stays visible over...
????????????????????????????????????????????
? eBook Reader (Behind)                    ?
? Currently reading...                     ?
????????????????????????????????????????????
```

**Benefit:** Monitor progress without hiding eBook!

---

### **Scenario 3: Quick Reference Notes**
Keep extracted text visible while browsing, coding, or researching.

---

## ??? How to Use

### **Method 1: View Menu (Recommended)**
```
1. Click "View" in menu bar
2. Click "Always on Top" (Ctrl+T)
3. ? Checkmark appears ? Window stays on top
4. Click again to disable
```

### **Method 2: Keyboard Shortcut**
```
Press Ctrl+T
- First press: ? Always on Top enabled
- Second press: Always on Top disabled
```

### **Method 3: System Tray Menu**
```
1. Right-click tray icon (??)
2. Click "Always on Top"
3. ? Checkmark appears ? Enabled
4. Click again to disable
```

---

## ?? Menu Locations

### **Main Menu Bar**
```
View
?? ? Always on Top     Ctrl+T
```

### **System Tray Context Menu**
```
Right-click tray icon:
?? Settings...
?? ? Always on Top
?? Kill process
```

**Checkmark (?)** indicates feature is active!

---

## ?? Technical Implementation

### **Form Property**
```csharp
this.TopMost = true;   // Always on top
this.TopMost = false;  // Normal window
```

### **Menu Synchronization**
Both menu items (View menu and tray menu) are synchronized:
```csharp
// Clicking View ? Always on Top
private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
{
    this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
    toolStripMenuItemAlwaysOnTop.Checked = alwaysOnTopToolStripMenuItem.Checked; // Sync
}

// Clicking Tray ? Always on Top  
private void toolStripMenuItemAlwaysOnTop_Click(object sender, EventArgs e)
{
    this.TopMost = toolStripMenuItemAlwaysOnTop.Checked;
    alwaysOnTopToolStripMenuItem.Checked = toolStripMenuItemAlwaysOnTop.Checked; // Sync
}
```

**Result:** Both checkmarks stay in sync regardless of which menu is used!

---

## ?? Behavior Details

### **When Enabled (?)**
- eBookMagic stays **above all windows**
- Including full-screen apps (may vary by Windows version)
- Even when clicking other applications
- Persists until disabled or app closed

### **When Disabled**
- Normal window behavior
- Can be covered by other windows
- Standard Alt+Tab behavior

### **During OCR Processing**
- Window minimizes as normal
- Always on Top **remembers state**
- When window restores ? Always on Top still active (if was enabled)

---

## ?? Important Notes

### **Does NOT Affect:**
- ? Minimizing (window can still minimize)
- ? Closing (window can still close)
- ? Moving/Resizing (window still movable)
- ? OCR processing (independent feature)

### **Persists:**
- ? During window minimize/restore
- ? When switching between apps
- ? When restoring from tray

### **Does NOT Persist:**
- ? After app restart (resets to normal)
- ? Between sessions (not saved to config)

---

## ?? Visual Indicators

### **Menu Checkmark**
```
View
?? ? Always on Top     ? Checkmark when enabled
```

### **Status Bar**
```
????????????????????????????????????????????
? Window set to always on top    ? 0 pages?
????????????????????????????????????????????
```

### **Window Title** (Optional Enhancement)
```
Before: eBookMagic - OCR Text Extractor
After:  eBookMagic - OCR Text Extractor [Always on Top]
```

---

## ?? Testing Checklist

- [ ] Click View ? Always on Top ? Window stays on top
- [ ] Press Ctrl+T ? Toggles always on top
- [ ] Right-click tray ? Always on Top ? Works
- [ ] Both menus show synchronized checkmarks
- [ ] Open another app ? eBookMagic stays visible
- [ ] Minimize ? Restore ? Always on Top still active
- [ ] Disable Always on Top ? Window behaves normally
- [ ] Status bar shows confirmation messages

---

## ?? Future Enhancements (Optional)

### **1. Remember Preference**
Save Always on Top state in user settings:
```csharp
// On form load
this.TopMost = Properties.Settings.Default.AlwaysOnTop;
alwaysOnTopToolStripMenuItem.Checked = this.TopMost;

// On change
Properties.Settings.Default.AlwaysOnTop = this.TopMost;
Properties.Settings.Default.Save();
```

### **2. Visual Indicator in Title Bar**
```csharp
private void UpdateWindowTitle()
{
    string title = "eBookMagic - OCR Text Extractor";
    if (this.TopMost)
        title += " [Always on Top]";
    this.Text = title;
}
```

### **3. Temporary Always on Top**
```csharp
// Stay on top for N seconds, then auto-disable
private async void TemporaryAlwaysOnTop(int seconds)
{
    this.TopMost = true;
    await Task.Delay(seconds * 1000);
    this.TopMost = false;
}
```

---

## ?? Comparison with Other Apps

### **How Other Apps Implement This:**

| Application | Always on Top Access |
|-------------|---------------------|
| **Notepad++** | View menu |
| **Visual Studio Code** | Right-click title bar |
| **Calculator** | View menu |
| **Sticky Notes** | Always on by default |
| **eBookMagic** | ? View menu + Tray menu + Ctrl+T |

eBookMagic provides **more access points** than most apps!

---

## ?? Tips & Tricks

### **Quick Toggle**
Learn the keyboard shortcut:
```
Ctrl+T = Toggle Always on Top
```
Fastest way to enable/disable!

### **Use During OCR**
1. Start OCR (window minimizes)
2. Manually restore window
3. Enable Always on Top (Ctrl+T)
4. Window stays visible while OCR continues
5. Monitor progress while reading eBook

### **Combine with Window Sizing**
```
1. Enable Always on Top
2. Resize window to small corner
3. Position over non-critical eBook area
4. Continue reading with OCR visible
```

---

## ?? Summary

**Feature:** Always on Top  
**Access Points:** 3 (View menu, Tray menu, Keyboard)  
**Keyboard Shortcut:** Ctrl+T  
**Synchronized:** ? Yes (both menus sync)  
**Persistent:** Across minimize/restore  
**Status Feedback:** ? Yes  

**Build Status:** ? Successful  
**Documentation:** ? Complete  
**Ready to Use:** ? Yes

---

## ? Implementation Complete!

The Always on Top feature is now fully functional with:
- ? Menu bar access (View ? Always on Top)
- ? System tray access (Right-click ? Always on Top)
- ? Keyboard shortcut (Ctrl+T)
- ? Synchronized checkmarks between menus
- ? Status bar feedback
- ? Complete documentation

**Enjoy keeping eBookMagic always visible!** ??
