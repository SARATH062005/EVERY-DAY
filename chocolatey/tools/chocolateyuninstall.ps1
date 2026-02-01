$ErrorActionPreference = 'Stop'

$packageName = 'everyday'
$installDir = Join-Path $env:LOCALAPPDATA 'EveryDay'

# Stop the application if running
Get-Process -Name "EveryDay" -ErrorAction SilentlyContinue | Stop-Process -Force

# Remove shortcuts
Remove-Item "$env:USERPROFILE\Desktop\EVERY-DAY.lnk" -ErrorAction SilentlyContinue
Remove-Item "$env:APPDATA\Microsoft\Windows\Start Menu\Programs\EVERY-DAY.lnk" -ErrorAction SilentlyContinue

# Remove auto-start registry entry
Remove-ItemProperty -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run" -Name "EveryDayApp" -ErrorAction SilentlyContinue

# Remove installation directory
Remove-Item $installDir -Recurse -Force -ErrorAction SilentlyContinue

Write-Host "EVERY-DAY has been uninstalled." -ForegroundColor Green
Write-Host "Your data is still preserved at: $env:APPDATA\EveryDay" -ForegroundColor Yellow
