# EVERY-DAY Database Inspector
# Quick script to check your application's stored data

$dbPath = "$env:APPDATA\EveryDay\everyday.db"

Write-Host "============================================================" -ForegroundColor Cyan
Write-Host "     EVERY-DAY Database Location & Information              " -ForegroundColor Cyan
Write-Host "============================================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "Database Location:" -ForegroundColor Yellow
Write-Host "   $dbPath" -ForegroundColor White
Write-Host ""

if (Test-Path $dbPath) {
    $fileInfo = Get-Item $dbPath
    
    Write-Host "Database exists!" -ForegroundColor Green
    Write-Host ""
    Write-Host "File Information:" -ForegroundColor Yellow
    $sizeKB = [math]::Round($fileInfo.Length / 1KB, 2)
    Write-Host "   Size: $sizeKB KB ($($fileInfo.Length) bytes)" -ForegroundColor White
    Write-Host "   Created: $($fileInfo.CreationTime)" -ForegroundColor White
    Write-Host "   Last Modified: $($fileInfo.LastWriteTime)" -ForegroundColor White
    Write-Host "   Last Accessed: $($fileInfo.LastAccessTime)" -ForegroundColor White
    Write-Host ""
    
    Write-Host "To view the database contents, you can:" -ForegroundColor Yellow
    Write-Host "   1. Use the Database Viewer tool (see below)" -ForegroundColor White
    Write-Host "   2. Open with LiteDB Studio" -ForegroundColor White
    Write-Host "   3. Copy the database file to backup" -ForegroundColor White
    Write-Host ""
    
    Write-Host "Database Viewer Tool Commands:" -ForegroundColor Yellow
    Write-Host "   cd Tools" -ForegroundColor Gray
    Write-Host "   dotnet run info          # Show database info" -ForegroundColor Gray
    Write-Host "   dotnet run list          # List all stored blocks" -ForegroundColor Gray
    Write-Host "   dotnet run stats         # Show statistics" -ForegroundColor Gray
    Write-Host "   dotnet run export        # Export to JSON" -ForegroundColor Gray
    Write-Host "   dotnet run search <term> # Search for content" -ForegroundColor Gray
    Write-Host ""
    
} else {
    Write-Host "Database does not exist yet." -ForegroundColor Red
    Write-Host ""
    Write-Host "The database will be created automatically when you:" -ForegroundColor Yellow
    Write-Host "   1. Run the application for the first time" -ForegroundColor White
    Write-Host "   2. Add your first block/note" -ForegroundColor White
    Write-Host ""
}

Write-Host "To open the database folder in Explorer:" -ForegroundColor Yellow
Write-Host "   explorer '$env:APPDATA\EveryDay'" -ForegroundColor Gray
Write-Host ""
