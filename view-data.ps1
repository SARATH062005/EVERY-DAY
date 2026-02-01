# Simple Database Data Viewer
# This script shows you what data is stored in your EVERY-DAY application

$dbPath = "$env:APPDATA\EveryDay\everyday.db"

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "  EVERY-DAY Application Data Summary" -ForegroundColor Cyan  
Write-Host "========================================`n" -ForegroundColor Cyan

Write-Host "📍 Database Location:" -ForegroundColor Yellow
Write-Host "   $dbPath`n" -ForegroundColor White

if (Test-Path $dbPath) {
    $fileInfo = Get-Item $dbPath
    
    Write-Host "✅ Database File Found!`n" -ForegroundColor Green
    
    Write-Host "📊 File Details:" -ForegroundColor Yellow
    Write-Host "   Size: $([math]::Round($fileInfo.Length / 1KB, 2)) KB" -ForegroundColor White
    Write-Host "   Created: $($fileInfo.CreationTime.ToString('yyyy-MM-dd HH:mm:ss'))" -ForegroundColor White
    Write-Host "   Last Modified: $($fileInfo.LastWriteTime.ToString('yyyy-MM-dd HH:mm:ss'))" -ForegroundColor White
    Write-Host ""
    
    Write-Host "💡 What's Stored:" -ForegroundColor Yellow
    Write-Host "   - All your notes and text blocks" -ForegroundColor White
    Write-Host "   - Todo items (checkboxes) and their status" -ForegroundColor White
    Write-Host "   - Headers and section organization" -ForegroundColor White
    Write-Host "   - Creation dates for all items" -ForegroundColor White
    Write-Host ""
    
    Write-Host "🔧 How to View the Data:" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "   Option 1: Use LiteDB Studio (Recommended)" -ForegroundColor Cyan
    Write-Host "   - Download from: https://github.com/mbdavid/LiteDB.Studio/releases" -ForegroundColor Gray
    Write-Host "   - Open the .exe file" -ForegroundColor Gray
    Write-Host "   - File → Open → Select the database file above" -ForegroundColor Gray
    Write-Host "   - Browse the 'blocks' collection to see all your data" -ForegroundColor Gray
    Write-Host ""
    
    Write-Host "   Option 2: Backup the Database" -ForegroundColor Cyan
    Write-Host "   - Copy the file to a safe location:" -ForegroundColor Gray
    Write-Host "     Copy-Item '$dbPath' -Destination '.\my_backup.db'" -ForegroundColor DarkGray
    Write-Host ""
    
    Write-Host "   Option 3: Open Database Folder" -ForegroundColor Cyan
    Write-Host "   - Run: explorer '$env:APPDATA\EveryDay'" -ForegroundColor DarkGray
    Write-Host ""
    
    Write-Host "📦 Quick Actions:" -ForegroundColor Yellow
    Write-Host "   [1] Open database folder in Explorer" -ForegroundColor White
    Write-Host "   [2] Create a backup copy" -ForegroundColor White
    Write-Host "   [3] Show file path only" -ForegroundColor White
    Write-Host "   [Q] Quit" -ForegroundColor White
    Write-Host ""
    
    $choice = Read-Host "Select an option (1-3, Q)"
    
    switch ($choice) {
        "1" {
            explorer "$env:APPDATA\EveryDay"
            Write-Host "`n✅ Opened database folder" -ForegroundColor Green
        }
        "2" {
            $backupPath = ".\everyday_backup_$(Get-Date -Format 'yyyyMMdd_HHmmss').db"
            Copy-Item $dbPath -Destination $backupPath
            Write-Host "`n✅ Backup created: $backupPath" -ForegroundColor Green
            Write-Host "   Size: $([math]::Round((Get-Item $backupPath).Length / 1KB, 2)) KB" -ForegroundColor White
        }
        "3" {
            Write-Host "`n📋 Database path copied to clipboard:" -ForegroundColor Green
            Write-Host "   $dbPath" -ForegroundColor White
            Set-Clipboard -Value $dbPath
        }
        default {
            Write-Host "`nGoodbye!" -ForegroundColor Cyan
        }
    }
    
}
else {
    Write-Host "❌ Database file not found`n" -ForegroundColor Red
    Write-Host "This means:" -ForegroundColor Yellow
    Write-Host "   - The application hasn't been run yet, OR" -ForegroundColor White
    Write-Host "   - No data has been added yet" -ForegroundColor White
    Write-Host ""
    Write-Host "💡 To create the database:" -ForegroundColor Yellow
    Write-Host "   1. Run the EVERY-DAY application" -ForegroundColor White
    Write-Host "   2. Add at least one note or todo item" -ForegroundColor White
    Write-Host "   3. The database will be created automatically" -ForegroundColor White
    Write-Host ""
}

Write-Host "`nPress any key to exit..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
