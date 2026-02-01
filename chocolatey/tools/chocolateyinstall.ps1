$ErrorActionPreference = 'Stop'

$packageName = 'everyday'
$toolsDir = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$installDir = Join-Path $env:LOCALAPPDATA 'EveryDay'

# Package parameters
$packageArgs = @{
    packageName    = $packageName
    fileType       = 'exe'
    silentArgs     = '/S'
    validExitCodes = @(0)
    url            = 'https://github.com/yourusername/EVERY-DAY/releases/download/v1.0.0/EveryDay-Setup.exe'
    checksum       = 'CHECKSUM_HERE'
    checksumType   = 'sha256'
}

# Install .NET 8.0 Runtime if not present
$dotnetVersion = & dotnet --version 2>$null
if ($LASTEXITCODE -ne 0) {
    Write-Host ".NET 8.0 Runtime is required. Installing..."
    choco install dotnet-8.0-runtime -y
}

# Create installation directory
New-Item -ItemType Directory -Force -Path $installDir | Out-Null

# Copy application files
Copy-Item -Path "$toolsDir\*" -Destination $installDir -Recurse -Force

# Create shortcuts
$WshShell = New-Object -ComObject WScript.Shell

# Desktop shortcut
$desktopShortcut = $WshShell.CreateShortcut("$env:USERPROFILE\Desktop\EVERY-DAY.lnk")
$desktopShortcut.TargetPath = "$installDir\EveryDay.exe"
$desktopShortcut.WorkingDirectory = $installDir
$desktopShortcut.Description = "EVERY-DAY - Your Daily Workspace"
$desktopShortcut.Save()

# Start Menu shortcut
$startMenuPath = "$env:APPDATA\Microsoft\Windows\Start Menu\Programs"
$startMenuShortcut = $WshShell.CreateShortcut("$startMenuPath\EVERY-DAY.lnk")
$startMenuShortcut.TargetPath = "$installDir\EveryDay.exe"
$startMenuShortcut.WorkingDirectory = $installDir
$startMenuShortcut.Description = "EVERY-DAY - Your Daily Workspace"
$startMenuShortcut.Save()

Write-Host "EVERY-DAY has been installed successfully!" -ForegroundColor Green
Write-Host "You can start it from the desktop shortcut or Start Menu." -ForegroundColor Cyan
