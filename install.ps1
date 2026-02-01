# EVERY-DAY Installation Script
# One-step installer for Windows

param(
    [string]$InstallPath = "$env:LOCALAPPDATA\EveryDay"
)

Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║          EVERY-DAY Installation Script                ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
Write-Host ""

# Check if .NET 8.0 is installed
Write-Host "Checking prerequisites..." -ForegroundColor Yellow

$dotnetVersion = dotnet --version 2>$null
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ .NET 8.0 Runtime is not installed!" -ForegroundColor Red
    Write-Host ""
    Write-Host "Please install .NET 8.0 Runtime from:" -ForegroundColor Yellow
    Write-Host "https://dotnet.microsoft.com/download/dotnet/8.0" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "After installation, run this script again." -ForegroundColor Yellow
    
    $response = Read-Host "Would you like to open the download page now? (Y/N)"
    if ($response -eq 'Y' -or $response -eq 'y') {
        Start-Process "https://dotnet.microsoft.com/download/dotnet/8.0"
    }
    exit 1
}

Write-Host "✅ .NET Runtime found: $dotnetVersion" -ForegroundColor Green
Write-Host ""

# Create installation directory
Write-Host "Creating installation directory..." -ForegroundColor Yellow
New-Item -ItemType Directory -Force -Path $InstallPath | Out-Null
Write-Host "✅ Installation path: $InstallPath" -ForegroundColor Green
Write-Host ""

# Build the application
Write-Host "Building EVERY-DAY application..." -ForegroundColor Yellow
$buildOutput = dotnet publish -c Release -o "$InstallPath" --self-contained false 2>&1

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Build failed!" -ForegroundColor Red
    Write-Host $buildOutput
    exit 1
}

Write-Host "✅ Application built successfully!" -ForegroundColor Green
Write-Host ""

# Create desktop shortcut
Write-Host "Creating desktop shortcut..." -ForegroundColor Yellow
$WshShell = New-Object -ComObject WScript.Shell
$Shortcut = $WshShell.CreateShortcut("$env:USERPROFILE\Desktop\EVERY-DAY.lnk")
$Shortcut.TargetPath = "$InstallPath\EveryDay.exe"
$Shortcut.WorkingDirectory = $InstallPath
$Shortcut.Description = "EVERY-DAY - Your Daily Workspace"
$Shortcut.Save()
Write-Host "✅ Desktop shortcut created!" -ForegroundColor Green
Write-Host ""

# Create start menu shortcut
Write-Host "Creating Start Menu shortcut..." -ForegroundColor Yellow
$StartMenuPath = "$env:APPDATA\Microsoft\Windows\Start Menu\Programs"
$StartShortcut = $WshShell.CreateShortcut("$StartMenuPath\EVERY-DAY.lnk")
$StartShortcut.TargetPath = "$InstallPath\EveryDay.exe"
$StartShortcut.WorkingDirectory = $InstallPath
$StartShortcut.Description = "EVERY-DAY - Your Daily Workspace"
$StartShortcut.Save()
Write-Host "✅ Start Menu shortcut created!" -ForegroundColor Green
Write-Host ""

# Create uninstaller
Write-Host "Creating uninstaller..." -ForegroundColor Yellow
$uninstallScript = @"
# EVERY-DAY Uninstaller

Write-Host "Uninstalling EVERY-DAY..." -ForegroundColor Yellow

# Stop the application if running
Get-Process -Name "EveryDay" -ErrorAction SilentlyContinue | Stop-Process -Force

# Remove shortcuts
Remove-Item "$env:USERPROFILE\Desktop\EVERY-DAY.lnk" -ErrorAction SilentlyContinue
Remove-Item "$env:APPDATA\Microsoft\Windows\Start Menu\Programs\EVERY-DAY.lnk" -ErrorAction SilentlyContinue

# Remove auto-start registry entry
Remove-ItemProperty -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run" -Name "EveryDayApp" -ErrorAction SilentlyContinue

# Remove installation directory
Remove-Item "$InstallPath" -Recurse -Force -ErrorAction SilentlyContinue

Write-Host "✅ EVERY-DAY has been uninstalled." -ForegroundColor Green
Write-Host ""
Write-Host "Note: Your data is still preserved at:" -ForegroundColor Yellow
Write-Host "`$env:APPDATA\EveryDay" -ForegroundColor Cyan
Write-Host ""
Write-Host "To remove your data as well, run:" -ForegroundColor Yellow
Write-Host "Remove-Item `"`$env:APPDATA\EveryDay`" -Recurse -Force" -ForegroundColor Gray

Pause
"@

$uninstallScript | Out-File -FilePath "$InstallPath\Uninstall.ps1" -Encoding UTF8
Write-Host "✅ Uninstaller created!" -ForegroundColor Green
Write-Host ""

# Installation complete
Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Green
Write-Host "║          Installation Complete! 🎉                     ║" -ForegroundColor Green
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Green
Write-Host ""
Write-Host "EVERY-DAY has been installed successfully!" -ForegroundColor Green
Write-Host ""
Write-Host "📁 Installation Location: $InstallPath" -ForegroundColor Cyan
Write-Host "🖥️  Desktop Shortcut: Created" -ForegroundColor Cyan
Write-Host "📌 Start Menu: Created" -ForegroundColor Cyan
Write-Host ""
Write-Host "To start EVERY-DAY:" -ForegroundColor Yellow
Write-Host "  1. Double-click the desktop shortcut, OR" -ForegroundColor White
Write-Host "  2. Search for 'EVERY-DAY' in Start Menu, OR" -ForegroundColor White
Write-Host "  3. Run: $InstallPath\EveryDay.exe" -ForegroundColor White
Write-Host ""
Write-Host "To uninstall:" -ForegroundColor Yellow
Write-Host "  Run: $InstallPath\Uninstall.ps1" -ForegroundColor White
Write-Host ""

$response = Read-Host "Would you like to start EVERY-DAY now? (Y/N)"
if ($response -eq 'Y' -or $response -eq 'y') {
    Start-Process "$InstallPath\EveryDay.exe"
    Write-Host "✅ EVERY-DAY is starting..." -ForegroundColor Green
}

Write-Host ""
Write-Host "Thank you for installing EVERY-DAY! 🎉" -ForegroundColor Cyan
