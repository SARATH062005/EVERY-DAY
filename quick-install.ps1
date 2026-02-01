# EVERY-DAY - One-Line Installer
# Run this command to install EVERY-DAY in one step:
# 
# PowerShell:
#   irm https://raw.githubusercontent.com/yourusername/EVERY-DAY/main/quick-install.ps1 | iex
#
# Or download and run:
#   Invoke-WebRequest -Uri "https://raw.githubusercontent.com/yourusername/EVERY-DAY/main/quick-install.ps1" -OutFile "install.ps1"; .\install.ps1

Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║        EVERY-DAY Quick Installer v1.0.0                ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
Write-Host ""

# Check .NET
Write-Host "Checking prerequisites..." -ForegroundColor Yellow
$dotnetVersion = dotnet --version 2>$null
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ .NET 8.0 Runtime required!" -ForegroundColor Red
    Write-Host ""
    Write-Host "Download from: https://dotnet.microsoft.com/download/dotnet/8.0" -ForegroundColor Cyan
    Write-Host ""
    $open = Read-Host "Open download page? (Y/N)"
    if ($open -eq 'Y') { Start-Process "https://dotnet.microsoft.com/download/dotnet/8.0" }
    exit 1
}
Write-Host "✅ .NET $dotnetVersion found!" -ForegroundColor Green
Write-Host ""

# Download or clone repository
Write-Host "Downloading EVERY-DAY..." -ForegroundColor Yellow
$tempDir = Join-Path $env:TEMP "EveryDay-Install"
$installDir = Join-Path $env:LOCALAPPDATA "EveryDay"

if (Test-Path $tempDir) { Remove-Item $tempDir -Recurse -Force }
New-Item -ItemType Directory -Path $tempDir | Out-Null

try {
    # Try to download release
    $releaseUrl = "https://github.com/yourusername/EVERY-DAY/releases/latest/download/EveryDay.zip"
    Invoke-WebRequest -Uri $releaseUrl -OutFile "$tempDir\EveryDay.zip" -ErrorAction Stop
    Expand-Archive -Path "$tempDir\EveryDay.zip" -DestinationPath $installDir -Force
    Write-Host "✅ Downloaded from releases!" -ForegroundColor Green
}
catch {
    Write-Host "⚠️  Release not found, cloning repository..." -ForegroundColor Yellow
    
    # Check if git is available
    git --version 2>$null
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Git is required to clone the repository!" -ForegroundColor Red
        Write-Host "Please install Git or download manually from GitHub." -ForegroundColor Yellow
        exit 1
    }
    
    # Clone and build
    git clone https://github.com/yourusername/EVERY-DAY.git $tempDir
    Set-Location $tempDir
    dotnet publish -c Release -o $installDir --self-contained false
    Write-Host "✅ Built from source!" -ForegroundColor Green
}

Write-Host ""

# Create shortcuts
Write-Host "Creating shortcuts..." -ForegroundColor Yellow
$WshShell = New-Object -ComObject WScript.Shell

$desktop = $WshShell.CreateShortcut("$env:USERPROFILE\Desktop\EVERY-DAY.lnk")
$desktop.TargetPath = "$installDir\EveryDay.exe"
$desktop.WorkingDirectory = $installDir
$desktop.Description = "EVERY-DAY - Your Daily Workspace"
$desktop.Save()

$startMenu = "$env:APPDATA\Microsoft\Windows\Start Menu\Programs"
$start = $WshShell.CreateShortcut("$startMenu\EVERY-DAY.lnk")
$start.TargetPath = "$installDir\EveryDay.exe"
$start.WorkingDirectory = $installDir
$start.Description = "EVERY-DAY - Your Daily Workspace"
$start.Save()

Write-Host "✅ Shortcuts created!" -ForegroundColor Green
Write-Host ""

# Cleanup
Remove-Item $tempDir -Recurse -Force -ErrorAction SilentlyContinue

# Success
Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Green
Write-Host "║          Installation Complete! 🎉                     ║" -ForegroundColor Green
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Green
Write-Host ""
Write-Host "EVERY-DAY is now installed!" -ForegroundColor Green
Write-Host ""
Write-Host "📁 Location: $installDir" -ForegroundColor Cyan
Write-Host "🖥️  Desktop shortcut: Created" -ForegroundColor Cyan
Write-Host "📌 Start Menu: Created" -ForegroundColor Cyan
Write-Host ""

$start = Read-Host "Start EVERY-DAY now? (Y/N)"
if ($start -eq 'Y') {
    Start-Process "$installDir\EveryDay.exe"
    Write-Host "✅ Starting EVERY-DAY..." -ForegroundColor Green
}

Write-Host ""
Write-Host "Thank you for installing EVERY-DAY! 🎉" -ForegroundColor Cyan
Write-Host ""
