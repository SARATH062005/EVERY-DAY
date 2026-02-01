# EVERY-DAY 📝

> A beautiful, lightweight desktop workspace application for Windows that helps you organize your daily notes, tasks, and thoughts.

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![WPF](https://img.shields.io/badge/WPF-Windows-blue)
![License](https://img.shields.io/badge/license-MIT-green)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey)

## 📖 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Screenshots](#screenshots)
- [Installation](#installation)
- [Usage](#usage)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Performance Optimizations](#performance-optimizations)
- [Data Storage](#data-storage)
- [Development](#development)
- [Contributing](#contributing)
- [License](#license)

## 🌟 Overview

**EVERY-DAY** is a modern, minimalist desktop application designed to be your daily workspace companion. It provides a clean, distraction-free interface for managing notes, tasks, and journal entries. The application runs in the system tray and auto-starts with Windows, making it always accessible when you need it.

### Why EVERY-DAY?

- **🚀 Lightning Fast**: Optimized startup time (~0.5-1 second) with lazy loading
- **🎨 Beautiful UI**: Modern, clean interface with dark/light theme support
- **💾 Local First**: All data stored locally in a lightweight LiteDB database
- **🔄 Auto-Save**: Changes are saved automatically as you type
- **📱 Always Available**: Runs in system tray, accessible with a click
- **🎯 Focused**: No distractions, just your content

## ✨ Features

### Core Features

- **📝 Multiple Block Types**
  - **Text Blocks**: For notes, ideas, and general writing
  - **Checkbox Blocks**: For todos and task management
  - **Header Blocks**: For organizing and structuring content

- **🗂️ Section Organization**
  - Notes
  - To Do
  - Tasks
  - Journal
  - Custom sections (easily extensible)

- **🎨 Theme Support**
  - Dark mode (default)
  - Light mode
  - Automatic system theme detection
  - Smooth theme transitions

- **💡 Smart Features**
  - Real-time auto-save
  - Drag-and-drop reordering
  - Search functionality
  - Inline editing
  - Delete with confirmation

### Technical Features

- **⚡ Performance Optimized**
  - Lazy initialization for faster startup
  - Asynchronous data loading
  - Efficient database queries
  - Minimal memory footprint (~20 MB on startup)

- **🔒 Data Management**
  - Local LiteDB database
  - Automatic backups
  - Export to JSON
  - Data viewer tools

- **🪟 Window Management**
  - Custom borderless window
  - Draggable title bar
  - Minimize/Maximize/Close controls
  - Resizable with grip
  - System tray integration

## 🖼️ Screenshots

### Main Interface (Dark Theme)
The application features a clean, modern interface with a sidebar for navigation and a main content area for your blocks.

![Main Interface](Docs\imgs\imageDT.png)

### Light Theme
Seamlessly switch between dark and light themes to match your preference or system settings.

![Light Theme](Docs\imgs\imageLT.png)

## 📥 Installation

### 🚀 Quick Install (One-Step)

Choose your preferred installation method:

#### Option 1: PowerShell Installer ⭐ (Recommended)

**One-liner from the web:**
```powershell
irm https://raw.githubusercontent.com/yourusername/EVERY-DAY/main/quick-install.ps1 | iex
```

**Or download and run:**
```powershell
# Download the installer
Invoke-WebRequest -Uri "https://github.com/yourusername/EVERY-DAY/raw/main/install.ps1" -OutFile "install.ps1"

# Run it
.\install.ps1
```

**What it does:**
- ✅ Checks for .NET 8.0 Runtime
- ✅ Builds and installs the application
- ✅ Creates desktop shortcut
- ✅ Creates Start Menu entry
- ✅ Generates uninstaller
- ✅ Offers to start the app

---

#### Option 2: Batch File Installer (No PowerShell Issues)

**Download and double-click:**
```cmd
# Download install.bat from the repository
# Then simply double-click it
install.bat
```

**Perfect for:**
- Users who have PowerShell execution policy restrictions
- Those who prefer traditional batch files
- Simple one-click installation

---

#### Option 3: Package Managers

**Chocolatey:**
```powershell
choco install everyday
```

**WinGet (Windows Package Manager):**
```powershell
winget install EveryDay
```

---

#### Option 4: Manual Download

**Standard Version** (Requires .NET 8.0):
1. Go to the [Releases](../../releases) page
2. Download `EveryDay-v1.0.0.zip`
3. Extract to your preferred location
4. Run `EveryDay.exe`

**Standalone Version** (No prerequisites):
1. Download `EveryDay-v1.0.0-standalone.zip`
2. Extract and run
3. No .NET installation required (larger file size ~70 MB)

---

#### Option 5: Build from Source

```bash
# Clone the repository
git clone https://github.com/yourusername/EVERY-DAY.git
cd EVERY-DAY

# Build in Release mode
dotnet build -c Release

# Run the application
.\bin\Release\net8.0-windows\EveryDay.exe
```

---

### Prerequisites

**For Standard Installation:**
- **Windows 10/11** (64-bit)
- **.NET 8.0 Runtime** ([Download](https://dotnet.microsoft.com/download/dotnet/8.0))

**For Standalone Version:**
- **Windows 10/11** (64-bit) only
- No other prerequisites needed!

---

### Auto-Start Setup

The application automatically configures itself to start with Windows on first run. You can:
- Disable auto-start from the system tray menu
- Or modify `AutoStartHelper.cs` settings

---

### Uninstallation

**If installed via installer scripts:**
```powershell
# Run the generated uninstaller
.\Uninstall.ps1
# or
.\Uninstall.bat
```

**If installed via Chocolatey:**
```powershell
choco uninstall everyday
```

**If installed via WinGet:**
```powershell
winget uninstall EveryDay
```

**Manual uninstall:**
1. Delete the application folder
2. Remove shortcuts from Desktop and Start Menu
3. (Optional) Delete data: `%APPDATA%\EveryDay`

---

### 📦 Distribution Note

**Why not Docker?** This is a Windows desktop GUI application (WPF) that requires a Windows desktop environment, system tray access, and Windows registry integration. Docker is designed for server applications and cannot run GUI applications. See [WHY_NOT_DOCKER.md](WHY_NOT_DOCKER.md) for details and better alternatives.

For detailed distribution information, see [DISTRIBUTION.md](DISTRIBUTION.md).

## 🚀 Usage

### Getting Started

1. **Launch the Application**
   - Double-click `EveryDay.exe` or find it in the system tray

2. **Create Your First Block**
   - Click `+ Text`, `+ Todo`, or `+ Header` at the bottom
   - Start typing immediately

3. **Organize with Sections**
   - Use the sidebar to switch between Notes, To Do, Tasks, and Journal
   - Each section maintains its own list of blocks

4. **Manage Blocks**
   - **Edit**: Click on any block to edit inline
   - **Reorder**: Drag and drop blocks to rearrange
   - **Delete**: Click the 🗑️ icon to remove
   - **Check**: Click checkboxes to mark todos complete

### Keyboard Shortcuts

- **Minimize**: Click minimize button or close to tray
- **Theme Toggle**: Click the 🌓 button in sidebar

### System Tray

- **Double-click**: Show/hide the window
- **Right-click**: Access context menu
  - Open EVERY DAY
  - Hide
  - Exit

## 🏗️ Architecture

### Technology Stack

- **Framework**: .NET 8.0
- **UI**: WPF (Windows Presentation Foundation)
- **Database**: LiteDB 5.0.19
- **Pattern**: MVVM (Model-View-ViewModel)
- **Language**: C# 12

### Design Patterns

#### MVVM Architecture

```
┌─────────────┐      ┌──────────────┐      ┌─────────┐
│    View     │ ───▶ │  ViewModel   │ ───▶ │  Model  │
│   (XAML)    │ ◀─── │   (Logic)    │ ◀─── │  (Data) │
└─────────────┘      └──────────────┘      └─────────┘
```

- **Models**: `Block`, `TextBlock`, `CheckboxBlock`, `HeaderBlock`
- **ViewModels**: `WidgetViewModel`, `BaseViewModel`
- **Views**: `WidgetWindow.xaml`
- **Services**: `ThemeManager`, `BlockRepository`, `LiteDbContext`

#### Key Components

1. **Data Layer**
   - `LiteDbContext`: Database connection and configuration
   - `BlockRepository`: CRUD operations for blocks

2. **Business Logic**
   - `WidgetViewModel`: Main application logic
   - `RelayCommand`: Command pattern implementation

3. **UI Layer**
   - `WidgetWindow`: Main window with custom chrome
   - `BlockTemplateSelector`: Dynamic template selection
   - Theme resources (Dark/Light)

4. **Helpers**
   - `AutoStartHelper`: Windows registry integration
   - `ThemeManager`: Theme switching and detection

## 📁 Project Structure

```
EVERY-DAY/
├── App.xaml                    # Application entry point
├── App.xaml.cs                 # Application startup logic
├── EveryDay.csproj             # Project configuration
│
├── Models/                     # Data models
│   └── Block.cs               # Block base class and implementations
│
├── ViewModels/                 # MVVM ViewModels
│   ├── BaseViewModel.cs       # Base class with INotifyPropertyChanged
│   ├── WidgetViewModel.cs     # Main window logic
│   └── RelayCommand.cs        # Command implementation
│
├── Views/                      # UI Views
│   ├── WidgetWindow.xaml      # Main window XAML
│   └── WidgetWindow.xaml.cs   # Main window code-behind
│
├── Data/                       # Data access layer
│   ├── LiteDbContext.cs       # Database context
│   └── BlockRepository.cs     # Data repository
│
├── Services/                   # Application services
│   └── ThemeManager.cs        # Theme management
│
├── Helpers/                    # Utility classes
│   └── AutoStartHelper.cs     # Auto-start functionality
│
├── Resources/                  # Application resources
│   ├── DarkTheme.xaml         # Dark theme colors
│   └── LightTheme.xaml        # Light theme colors
│
├── Converters/                 # Value converters
│   └── StrikeConverter.cs     # Strikethrough for completed todos
│
├── Selectors/                  # Template selectors
│   └── BlockTemplateSelector.cs
│
├── Tools/                      # Utility tools
│   ├── DatabaseViewer.cs      # Database inspection tool
│   ├── Program.cs             # CLI entry point
│   └── DbViewer.csproj        # Tool project file
│
├── chocolatey/                 # Chocolatey package
│   ├── everyday.nuspec        # Package specification
│   └── tools/                 # Package tools
│       ├── chocolateyinstall.ps1
│       └── chocolateyuninstall.ps1
│
├── Installation/               # Installation scripts
│   ├── install.ps1            # PowerShell installer
│   ├── install.bat            # Batch installer
│   └── quick-install.ps1      # One-liner web installer
│
└── Documentation/
    ├── README.md              # This file
    ├── CONTRIBUTING.md        # Contribution guidelines
    ├── DISTRIBUTION.md        # Distribution guide
    ├── WHY_NOT_DOCKER.md      # Docker alternatives
    ├── STARTUP_OPTIMIZATIONS.md
    ├── DATA_STORAGE_GUIDE.md
    ├── DATA_QUICK_REFERENCE.md
    ├── QUICK_START.md
    ├── check-database.ps1     # Database location script
    └── view-data.ps1          # Interactive data viewer
```

## ⚡ Performance Optimizations

The application has been heavily optimized for fast startup and smooth operation:

### Startup Optimizations

1. **Lazy Widget Loading**
   - Widget window created on-demand, not at startup
   - Reduces initial load time by 60-80%

2. **Asynchronous Initialization**
   - Theme detection runs in background
   - Database initialization deferred until needed

3. **Optimized Database**
   - Removed unnecessary index creation
   - Shared connection mode for better performance

4. **.NET Runtime Optimizations**
   - Tiered compilation enabled
   - Quick JIT for faster cold starts
   - ReadyToRun compilation
   - Workstation GC for desktop performance

### Performance Metrics

| Metric | Before | After | Improvement |
|--------|--------|-------|-------------|
| Startup Time | 2-4 sec | 0.5-1 sec | **75% faster** |
| Memory (startup) | ~50 MB | ~20 MB | **60% less** |
| First Paint | Delayed | Instant | **Immediate** |

See [STARTUP_OPTIMIZATIONS.md](STARTUP_OPTIMIZATIONS.md) for detailed information.

## 💾 Data Storage

### Database Location

All data is stored locally in a LiteDB database:

```
C:\Users\<YourUsername>\AppData\Roaming\EveryDay\everyday.db
```

### Data Structure

Each block is stored with:
- **Id**: Unique identifier (GUID)
- **Type**: Text, Checkbox, or Header
- **Content**: The actual text content
- **Section**: Which section it belongs to
- **Order**: Position in the list
- **CreatedAt**: Timestamp
- **IsChecked**: For checkbox blocks

### Viewing Your Data

#### Quick Check
```powershell
.\view-data.ps1
```

#### Database Viewer Tool
```powershell
cd Tools
dotnet run list    # List all blocks
dotnet run stats   # Show statistics
dotnet run export  # Export to JSON
```

#### LiteDB Studio (GUI)
Download [LiteDB Studio](https://github.com/mbdavid/LiteDB.Studio) to browse the database visually.

### Backup & Restore

#### Create Backup
```powershell
Copy-Item "$env:APPDATA\EveryDay\everyday.db" -Destination ".\backup.db"
```

#### Restore from Backup
```powershell
Copy-Item ".\backup.db" -Destination "$env:APPDATA\EveryDay\everyday.db" -Force
```

See [DATA_STORAGE_GUIDE.md](DATA_STORAGE_GUIDE.md) for comprehensive data management information.

## 🛠️ Development

### Building the Project

```bash
# Debug build
dotnet build

# Release build (optimized)
dotnet build -c Release

# Run from source
dotnet run

# Publish self-contained
dotnet publish -c Release --self-contained true -r win-x64
```

### Development Requirements

- **Visual Studio 2022** or **Visual Studio Code**
- **.NET 8.0 SDK**
- **Windows 10/11**

### Project Configuration

The project uses:
- **Target Framework**: net8.0-windows
- **Output Type**: WinExe (Windows application)
- **Nullable**: Enabled
- **Implicit Usings**: Enabled

### Adding New Features

#### Adding a New Block Type

1. Create a new class in `Models/Block.cs`:
```csharp
public class CustomBlock : Block
{
    private string _content = "";
    public string Content 
    { 
        get => _content;
        set 
        { 
            if (_content != value)
            {
                _content = value;
                OnPropertyChanged();
            }
        }
    }
    public CustomBlock() { Type = "Custom"; }
}
```

2. Add a DataTemplate in `Views/WidgetWindow.xaml`

3. Update `BlockTemplateSelector.cs`

4. Add command in `WidgetViewModel.cs`

#### Adding a New Section

1. Add button in sidebar (WidgetWindow.xaml):
```xml
<Button Command="{Binding ChangeSectionCommand}" 
        CommandParameter="NewSection" 
        Style="{StaticResource NavButton}">
    <StackPanel Orientation="Horizontal">
        <TextBlock Text="🆕" FontSize="16" Margin="0,0,8,0"/>
        <TextBlock Text="New Section"/>
    </StackPanel>
</Button>
```

2. The section will automatically work with existing infrastructure!

### Testing

```bash
# Run the application in debug mode
dotnet run

# Build and test release version
dotnet build -c Release
.\bin\Release\net8.0-windows\EveryDay.exe
```

## 🤝 Contributing

Contributions are welcome! Here's how you can help:

### Ways to Contribute

1. **Report Bugs**: Open an issue with details
2. **Suggest Features**: Share your ideas
3. **Submit Pull Requests**: Fix bugs or add features
4. **Improve Documentation**: Help others understand the project
5. **Share**: Tell others about EVERY-DAY

### Development Workflow

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Code Style

- Follow C# coding conventions
- Use meaningful variable names
- Comment complex logic
- Keep methods focused and small
- Write self-documenting code

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- **LiteDB** - Lightweight NoSQL database
- **.NET Team** - For the amazing framework
- **WPF Community** - For resources and inspiration

## 📞 Support

- **Issues**: [GitHub Issues](../../issues)
- **Discussions**: [GitHub Discussions](../../discussions)
- **Email**: your.email@example.com

## 🗺️ Roadmap

### Planned Features

- [ ] Cloud sync (optional)
- [ ] Markdown support
- [ ] Tags and categories
- [ ] Search with filters
- [ ] Export to multiple formats (PDF, Markdown, HTML)
- [ ] Keyboard shortcuts
- [ ] Custom themes
- [ ] Plugins/Extensions system
- [ ] Mobile companion app

### Version History

#### v1.0.0 (Current)
- ✅ Core functionality
- ✅ Multiple block types
- ✅ Section organization
- ✅ Dark/Light themes
- ✅ Auto-save
- ✅ Drag-and-drop
- ✅ System tray integration
- ✅ Performance optimizations

## 💡 Tips & Tricks

### Productivity Tips

1. **Use Sections Wisely**: Separate work, personal, and journal entries
2. **Headers for Organization**: Use headers to structure long lists
3. **Quick Capture**: Keep the app in tray for instant access
4. **Daily Review**: Use the Journal section for daily reflections

### Advanced Usage

- **Database Location**: Bookmark the AppData folder for quick backups
- **Export Regularly**: Use the export tool to create JSON backups
- **Theme Switching**: Match your system theme for consistency

## 🔧 Troubleshooting

### Common Issues

**Application won't start**
- Ensure .NET 8.0 Runtime is installed
- Check Windows Event Viewer for errors
- Try running as administrator

**Data not saving**
- Check disk space
- Verify write permissions to AppData folder
- Check `crash_log.txt` for errors

**Performance issues**
- Ensure you're using the Release build
- Check database size (should be < 5 MB)
- Close other resource-intensive applications

**Database corruption**
- Restore from backup
- Use the database viewer tool to export data
- Create a new database and re-import

See [DATA_STORAGE_GUIDE.md](DATA_STORAGE_GUIDE.md) for more troubleshooting steps.

---

<div align="center">

**Made with ❤️ for productivity enthusiasts**

[⬆ Back to Top](#every-day-)

</div>
