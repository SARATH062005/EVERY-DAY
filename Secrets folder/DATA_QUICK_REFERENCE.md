# Quick Reference: Checking Your Application Data

## 🎯 Fastest Way to Check Your Data

### Run this script:
```powershell
.\view-data.ps1
```

This interactive script will:
- ✅ Show you where your data is stored
- ✅ Display file size and last modified date
- ✅ Let you open the database folder
- ✅ Create backups with one click
- ✅ Guide you on how to view the actual content

## 📁 Where is My Data?

Your data is stored at:
```
C:\Users\sarat\AppData\Roaming\EveryDay\everyday.db
```

## 🔍 Three Ways to View Your Data

### 1. **PowerShell Script** (Easiest)
```powershell
.\view-data.ps1
```
Shows file info and provides quick actions.

### 2. **LiteDB Studio** (Best for browsing)
- Download: https://github.com/mbdavid/LiteDB.Studio/releases
- Open the database file
- Browse all your notes, todos, and data visually

### 3. **Database Viewer Tool** (Command-line)
```powershell
cd Tools
dotnet run info    # Database information
dotnet run list    # List all blocks
dotnet run stats   # Show statistics
dotnet run export  # Export to JSON
```

## 💾 How to Backup

### Quick Backup:
```powershell
Copy-Item "$env:APPDATA\EveryDay\everyday.db" -Destination ".\backup.db"
```

### Or use the interactive script:
```powershell
.\view-data.ps1
# Then select option 2
```

## 📊 What Data is Stored?

The database contains:
- ✅ All your text notes
- ✅ Todo items (with checked/unchecked status)
- ✅ Headers and sections
- ✅ Creation timestamps
- ✅ Order/position of items

## 🔒 Is My Data Safe?

- ✅ Stored locally on your PC (not in cloud)
- ✅ Protected by Windows user permissions
- ✅ Survives app updates and restarts
- ⚠️ Not encrypted by default

To encrypt: Use Windows EFS or BitLocker

## 🚨 Common Questions

**Q: Can I delete the database?**  
A: Yes, but you'll lose all your data. Backup first!

**Q: Can I move the database to another computer?**  
A: Yes! Just copy the .db file to the same location on the other PC.

**Q: How big will the database get?**  
A: Very small! Even with 1000+ items, it's usually under 1 MB.

**Q: Can I edit the database directly?**  
A: Yes, with LiteDB Studio, but be careful! Always backup first.

---

**Need Help?** Check `DATA_STORAGE_GUIDE.md` for detailed information.
