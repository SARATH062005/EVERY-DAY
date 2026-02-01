@echo off
REM EVERY-DAY Quick Installer for Windows
REM This batch file provides a simple one-click installation

echo ============================================================
echo          EVERY-DAY Quick Installer
echo ============================================================
echo.

REM Check for administrator privileges
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Running with administrator privileges...
) else (
    echo Note: Running without administrator privileges.
    echo Some features may require admin rights.
)
echo.

REM Check if .NET 8.0 is installed
echo Checking for .NET 8.0 Runtime...
dotnet --version >nul 2>&1
if %errorLevel% neq 0 (
    echo [ERROR] .NET 8.0 Runtime is not installed!
    echo.
    echo Please install .NET 8.0 Runtime from:
    echo https://dotnet.microsoft.com/download/dotnet/8.0
    echo.
    echo After installation, run this installer again.
    pause
    exit /b 1
)
echo [OK] .NET Runtime found!
echo.

REM Set installation directory
set "INSTALL_DIR=%LOCALAPPDATA%\EveryDay"
echo Installation directory: %INSTALL_DIR%
echo.

REM Create installation directory
echo Creating installation directory...
if not exist "%INSTALL_DIR%" mkdir "%INSTALL_DIR%"
echo [OK] Directory created!
echo.

REM Build and publish the application
echo Building EVERY-DAY application...
echo This may take a minute...
dotnet publish -c Release -o "%INSTALL_DIR%" --self-contained false
if %errorLevel% neq 0 (
    echo [ERROR] Build failed!
    pause
    exit /b 1
)
echo [OK] Application built successfully!
echo.

REM Create desktop shortcut
echo Creating desktop shortcut...
powershell -Command "$WshShell = New-Object -ComObject WScript.Shell; $Shortcut = $WshShell.CreateShortcut('%USERPROFILE%\Desktop\EVERY-DAY.lnk'); $Shortcut.TargetPath = '%INSTALL_DIR%\EveryDay.exe'; $Shortcut.WorkingDirectory = '%INSTALL_DIR%'; $Shortcut.Description = 'EVERY-DAY - Your Daily Workspace'; $Shortcut.Save()"
echo [OK] Desktop shortcut created!
echo.

REM Create Start Menu shortcut
echo Creating Start Menu shortcut...
set "START_MENU=%APPDATA%\Microsoft\Windows\Start Menu\Programs"
powershell -Command "$WshShell = New-Object -ComObject WScript.Shell; $Shortcut = $WshShell.CreateShortcut('%START_MENU%\EVERY-DAY.lnk'); $Shortcut.TargetPath = '%INSTALL_DIR%\EveryDay.exe'; $Shortcut.WorkingDirectory = '%INSTALL_DIR%'; $Shortcut.Description = 'EVERY-DAY - Your Daily Workspace'; $Shortcut.Save()"
echo [OK] Start Menu shortcut created!
echo.

REM Create uninstaller
echo Creating uninstaller...
(
echo @echo off
echo echo Uninstalling EVERY-DAY...
echo taskkill /F /IM EveryDay.exe ^>nul 2^>^&1
echo del "%USERPROFILE%\Desktop\EVERY-DAY.lnk" ^>nul 2^>^&1
echo del "%START_MENU%\EVERY-DAY.lnk" ^>nul 2^>^&1
echo reg delete "HKCU\Software\Microsoft\Windows\CurrentVersion\Run" /v EveryDayApp /f ^>nul 2^>^&1
echo echo EVERY-DAY has been uninstalled.
echo echo Your data is still preserved at: %%APPDATA%%\EveryDay
echo pause
) > "%INSTALL_DIR%\Uninstall.bat"
echo [OK] Uninstaller created!
echo.

REM Installation complete
echo ============================================================
echo          Installation Complete!
echo ============================================================
echo.
echo EVERY-DAY has been installed successfully!
echo.
echo Installation Location: %INSTALL_DIR%
echo Desktop Shortcut: Created
echo Start Menu: Created
echo.
echo To start EVERY-DAY:
echo   - Double-click the desktop shortcut, OR
echo   - Search for 'EVERY-DAY' in Start Menu
echo.
echo To uninstall:
echo   - Run: %INSTALL_DIR%\Uninstall.bat
echo.
echo.

REM Ask to start the application
set /p START="Would you like to start EVERY-DAY now? (Y/N): "
if /i "%START%"=="Y" (
    start "" "%INSTALL_DIR%\EveryDay.exe"
    echo EVERY-DAY is starting...
)

echo.
echo Thank you for installing EVERY-DAY!
echo.
pause
