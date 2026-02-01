# How to Check Your Application Data

This guide explains where your EVERY-DAY application stores data and how to view, backup, and manage it.

## 📁 Data Storage Location

Your application data is stored in a **LiteDB database file** at:

```
C:\Users\sarat\AppData\Roaming\EveryDay\everyday.db
```

This location is:
- ✅ **User-specific** - Each Windows user has their own data
- ✅ **Persistent** - Data survives application restarts and updates
- ✅ **Backed up** - Included in Windows user profile backups
- ✅ **Portable** - You can copy this file to backup or transfer data

## 🔍 Quick Ways to Check Your Data

### Method 1: PowerShell Script (Easiest)

Run the included script to see database information:

```powershell
.\check-database.ps1
```

This will show:
- Database file location
- File size
- Last modified date
- Quick commands to view data

### Method 2: Open Database Folder

Open the folder in Windows Explorer:

```powershell
explorer "$env:APPDATA\EveryDay"
```

Or manually navigate to:
```
C:\Users\sarat\AppData\Roaming\EveryDay
```

### Method 3: Database Viewer Tool (Most Detailed)

Use the built-in database viewer for detailed inspection:

```powershell
cd Tools
dotnet run                    # Show menu and database info
dotnet run list               # List all your blocks/notes
dotnet run stats              # Show statistics
dotnet run export             # Export to JSON file
dotnet run search "keyword"   # Search for specific content
```

## 📊 What Data is Stored?

The database contains all your blocks/notes with:

- **Content** - The text you entered
- **Type** - Text, Header, or Checkbox
- **Section** - Which section it belongs to (Notes, Tasks, etc.)
- **Order** - The position in the list
- **Timestamps** - When created and last updated
- **Checkbox state** - For todo items (checked/unchecked)
- **Unique ID** - For each block

## 💾 How to Backup Your Data

### Option 1: Copy the Database File

```powershell
# Create a backup
Copy-Item "$env:APPDATA\EveryDay\everyday.db" -Destination ".\backup_$(Get-Date -Format 'yyyy-MM-dd').db"
```

### Option 2: Export to JSON

```powershell
cd Tools
dotnet run export "my_backup.json"
```

This creates a human-readable JSON file with all your data.

### Option 3: Automated Backup Script

Create a scheduled task to backup automatically:

```powershell
# Create backup folder
$backupFolder = "$env:USERPROFILE\Documents\EveryDay_Backups"
New-Item -ItemType Directory -Force -Path $backupFolder

# Copy database with timestamp
$timestamp = Get-Date -Format "yyyy-MM-dd_HHmmss"
Copy-Item "$env:APPDATA\EveryDay\everyday.db" -Destination "$backupFolder\everyday_$timestamp.db"
```

## 🔄 How to Restore Data

### From Database Backup

1. Close the application completely
2. Replace the database file:
   ```powershell
   Copy-Item ".\your_backup.db" -Destination "$env:APPDATA\EveryDay\everyday.db" -Force
   ```
3. Start the application

### From JSON Export

You'll need to write a custom import tool or manually re-enter data (JSON export is mainly for viewing/archiving).

## 🛠️ Advanced: View Data with LiteDB Studio

For advanced users, you can use **LiteDB Studio** (a free database viewer):

1. Download from: https://github.com/mbdavid/LiteDB.Studio/releases
2. Open LiteDB Studio
3. File → Open → Navigate to `C:\Users\sarat\AppData\Roaming\EveryDay\everyday.db`
4. Browse collections and data visually

## 📈 Database Size Management

The database file grows as you add more data:

- **Small usage** (< 100 blocks): ~50-100 KB
- **Medium usage** (100-1000 blocks): ~100-500 KB  
- **Heavy usage** (1000+ blocks): ~500 KB - 5 MB

LiteDB is very efficient, so even with thousands of entries, the database remains small.

### If Database Gets Too Large

1. **Archive old data**: Export to JSON and delete old blocks
2. **Compact database**: LiteDB auto-compacts, but you can force it:
   ```csharp
   using (var db = new LiteDatabase(dbPath))
   {
       db.Rebuild();
   }
   ```

## 🔒 Data Security

### Current Security

- ✅ Stored locally on your PC (not in the cloud)
- ✅ Protected by Windows user account permissions
- ❌ **Not encrypted** - Anyone with access to your PC can read it

### To Encrypt Your Data

If you need encryption, you can:

1. **Use Windows EFS** (Encrypting File System):
   ```powershell
   # Encrypt the entire folder
   cipher /e "$env:APPDATA\EveryDay"
   ```

2. **Use BitLocker** to encrypt your entire drive

3. **Modify the code** to use LiteDB's built-in encryption:
   ```csharp
   var connectionString = new ConnectionString
   {
       Filename = path,
       Password = "your-secure-password"
   };
   Database = new LiteDatabase(connectionString);
   ```

## 📝 Database Schema

The database has one main collection: `blocks`

Each block document contains:
```json
{
  "Id": "guid",
  "Type": "Text|Header|Check",
  "Content": "Your text here",
  "Section": "Notes|Tasks|Ideas",
  "Order": 0,
  "CreatedAt": "2026-02-01T10:30:00",
  "UpdatedAt": "2026-02-01T15:30:00",
  "IsChecked": false  // Only for checkbox blocks
}
```

## 🚨 Troubleshooting

### Database is Locked

**Error**: "Database is locked" or "Cannot access file"

**Solution**: 
1. Close all instances of the application
2. Check Task Manager for any lingering processes
3. Try again

### Database is Corrupted

**Error**: "Invalid database format" or crashes when opening

**Solution**:
1. Restore from backup (see above)
2. If no backup, try LiteDB recovery:
   ```powershell
   # Attempt to recover
   cd Tools
   dotnet run export "recovered_data.json"
   ```

### Data Not Saving

**Issue**: Changes don't persist after closing

**Check**:
1. Database file location is writable
2. No disk space issues
3. Check `crash_log.txt` for errors

## 📞 Quick Reference Commands

```powershell
# View database location and info
.\check-database.ps1

# Open database folder
explorer "$env:APPDATA\EveryDay"

# Backup database
Copy-Item "$env:APPDATA\EveryDay\everyday.db" -Destination ".\backup.db"

# View all data
cd Tools
dotnet run list

# Export to JSON
cd Tools
dotnet run export

# Search for content
cd Tools
dotnet run search "your search term"
```

---

**Last Updated**: 2026-02-01  
**Database Format**: LiteDB v5.0.19  
**Location**: `%APPDATA%\EveryDay\everyday.db`
