# Distribution & Installation Guide

This guide explains all the ways users can install EVERY-DAY with minimal effort.

## 🚀 One-Step Installation Methods

### Method 1: PowerShell Installer (Recommended) ⭐

**For Users:**
```powershell
# Download and run the installer
Invoke-WebRequest -Uri "https://github.com/yourusername/EVERY-DAY/raw/main/install.ps1" -OutFile "install.ps1"
.\install.ps1
```

**Or if they have the repository:**
```powershell
cd EVERY-DAY
.\install.ps1
```

**What it does:**
- ✅ Checks for .NET 8.0 Runtime
- ✅ Builds the application
- ✅ Creates desktop shortcut
- ✅ Creates Start Menu entry
- ✅ Generates uninstaller
- ✅ Offers to start the app

---

### Method 2: Batch File Installer

**For Users who prefer .bat files:**
```cmd
install.bat
```

**Advantages:**
- No PowerShell execution policy issues
- Works on all Windows versions
- Simple double-click installation

---

### Method 3: Chocolatey Package Manager

**For Chocolatey users:**
```powershell
choco install everyday
```

**To publish to Chocolatey:**
1. Create account at https://community.chocolatey.org/
2. Package the application:
   ```powershell
   cd chocolatey
   choco pack
   ```
3. Push to Chocolatey:
   ```powershell
   choco push everyday.1.0.0.nupkg --source https://push.chocolatey.org/
   ```

---

### Method 4: WinGet (Windows Package Manager)

**For WinGet users:**
```powershell
winget install EveryDay
```

**To publish to WinGet:**
1. Fork https://github.com/microsoft/winget-pkgs
2. Add manifest in `manifests/y/YourName/EveryDay/1.0.0/`
3. Submit pull request

---

### Method 5: Direct Download (GitHub Releases)

**For users who prefer downloads:**

1. Go to [Releases](https://github.com/yourusername/EVERY-DAY/releases)
2. Download `EveryDay-v1.0.0.zip`
3. Extract to desired location
4. Run `EveryDay.exe`

**To create release package:**
```powershell
# Build release version
dotnet publish -c Release -o publish --self-contained false

# Create zip file
Compress-Archive -Path publish\* -DestinationPath EveryDay-v1.0.0.zip
```

---

### Method 6: Self-Contained Executable

**For users without .NET Runtime:**

```powershell
# Build self-contained (includes .NET runtime)
dotnet publish -c Release -r win-x64 --self-contained true -o publish-standalone

# Create zip
Compress-Archive -Path publish-standalone\* -DestinationPath EveryDay-v1.0.0-standalone.zip
```

**Note:** File size will be larger (~70 MB vs ~5 MB) but requires no dependencies.

---

## 📦 Creating Distribution Packages

### For GitHub Releases

1. **Build the application:**
   ```powershell
   dotnet publish -c Release -o release-build --self-contained false
   ```

2. **Create release package:**
   ```powershell
   # Standard version (requires .NET 8.0)
   Compress-Archive -Path release-build\* -DestinationPath EveryDay-v1.0.0.zip
   
   # Standalone version (includes .NET)
   dotnet publish -c Release -r win-x64 --self-contained true -o standalone-build
   Compress-Archive -Path standalone-build\* -DestinationPath EveryDay-v1.0.0-standalone.zip
   ```

3. **Create GitHub Release:**
   - Go to repository → Releases → Create new release
   - Tag: `v1.0.0`
   - Title: `EVERY-DAY v1.0.0`
   - Upload both zip files
   - Add release notes

---

### For Chocolatey

1. **Prepare package:**
   ```powershell
   cd chocolatey
   
   # Build application
   dotnet publish -c Release -o tools --self-contained false
   
   # Create package
   choco pack
   ```

2. **Test locally:**
   ```powershell
   choco install everyday -source .
   ```

3. **Publish:**
   ```powershell
   choco push everyday.1.0.0.nupkg --source https://push.chocolatey.org/ --api-key YOUR-API-KEY
   ```

---

### For Microsoft Store (Advanced)

To distribute via Microsoft Store:

1. **Create MSIX package:**
   - Install Windows Application Packaging Project
   - Add to solution
   - Configure package manifest
   - Build MSIX package

2. **Submit to Store:**
   - Create Partner Center account
   - Submit app for certification
   - Wait for approval

**Benefits:**
- Automatic updates
- Trusted installation
- Wider reach

---

## 🎯 Recommended Distribution Strategy

### For Maximum Reach:

1. **GitHub Releases** (Primary)
   - Upload both standard and standalone versions
   - Include installer scripts
   - Provide clear installation instructions

2. **Chocolatey** (For power users)
   - Easy installation for developers
   - Automatic dependency management

3. **WinGet** (For Windows 11 users)
   - Native Windows package manager
   - Growing user base

4. **Microsoft Store** (Optional, for mainstream users)
   - Requires more setup
   - Best for non-technical users

---

## 📝 Installation Instructions for Users

### Quick Start (Copy-paste ready)

Add this to your README:

```markdown
## 🚀 Quick Installation

### Option 1: One-Click Installer (Easiest)

1. Download [install.bat](https://github.com/yourusername/EVERY-DAY/raw/main/install.bat)
2. Double-click to run
3. Follow the prompts

### Option 2: PowerShell

```powershell
# Download and run
Invoke-WebRequest -Uri "https://github.com/yourusername/EVERY-DAY/raw/main/install.ps1" -OutFile "install.ps1"
.\install.ps1
```

### Option 3: Package Managers

**Chocolatey:**
```powershell
choco install everyday
```

**WinGet:**
```powershell
winget install EveryDay
```

### Option 4: Manual Download

1. Download the latest release from [Releases](../../releases)
2. Extract the ZIP file
3. Run `EveryDay.exe`

**Prerequisites:** .NET 8.0 Runtime ([Download](https://dotnet.microsoft.com/download/dotnet/8.0))
```

---

## 🔧 Troubleshooting Distribution

### Common Issues

**"Script execution is disabled"**
```powershell
# Run PowerShell as Administrator
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

**Or use the batch installer instead:**
```cmd
install.bat
```

**".NET Runtime not found"**
- Direct users to: https://dotnet.microsoft.com/download/dotnet/8.0
- Or provide standalone version

**"Access denied"**
- Run installer as Administrator
- Or install to user directory (default)

---

## 📊 Distribution Checklist

Before releasing:

- [ ] Build Release version
- [ ] Test on clean Windows installation
- [ ] Create standard ZIP package
- [ ] Create standalone ZIP package
- [ ] Test installer scripts
- [ ] Create GitHub Release
- [ ] Update README with installation instructions
- [ ] Create Chocolatey package (optional)
- [ ] Submit to WinGet (optional)
- [ ] Announce release

---

## 🎉 Why Docker Doesn't Work

**Important Note:** Docker is **not suitable** for this application because:

1. ❌ WPF requires Windows desktop environment
2. ❌ Docker containers are headless (no GUI)
3. ❌ System tray requires Windows Shell
4. ❌ Registry access not available in containers
5. ❌ Windows Forms components need full Windows session

**Instead, use:**
- ✅ Native Windows installers (PowerShell/Batch)
- ✅ Package managers (Chocolatey/WinGet)
- ✅ GitHub Releases with ZIP files
- ✅ Microsoft Store (MSIX packages)

---

## 📈 Analytics & Updates

### Tracking Downloads

Use GitHub Release download counts:
- View in repository → Releases
- Each asset shows download count

### Automatic Updates

Consider implementing:
1. **GitHub Releases API** - Check for new versions
2. **Squirrel.Windows** - Auto-update framework
3. **ClickOnce** - Microsoft's update mechanism

---

## 🌐 Distribution Platforms Comparison

| Platform | Ease of Setup | User Reach | Auto-Update | Cost |
|----------|---------------|------------|-------------|------|
| GitHub Releases | ⭐⭐⭐⭐⭐ | Medium | No | Free |
| Chocolatey | ⭐⭐⭐⭐ | Medium | Yes | Free |
| WinGet | ⭐⭐⭐ | Growing | Yes | Free |
| Microsoft Store | ⭐⭐ | High | Yes | $19 one-time |
| Direct Download | ⭐⭐⭐⭐⭐ | Low | No | Free |

**Recommendation:** Start with GitHub Releases + Chocolatey, then expand to WinGet and Store.

---

**Last Updated:** 2026-02-01
