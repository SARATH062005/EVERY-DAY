# Contributing to EVERY-DAY

First off, thank you for considering contributing to EVERY-DAY! It's people like you that make EVERY-DAY such a great tool.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [How Can I Contribute?](#how-can-i-contribute)
- [Development Setup](#development-setup)
- [Pull Request Process](#pull-request-process)
- [Coding Standards](#coding-standards)
- [Commit Messages](#commit-messages)

## Code of Conduct

This project and everyone participating in it is governed by respect and professionalism. By participating, you are expected to uphold this code.

### Our Standards

- Be respectful and inclusive
- Accept constructive criticism gracefully
- Focus on what is best for the community
- Show empathy towards other community members

## How Can I Contribute?

### Reporting Bugs

Before creating bug reports, please check the existing issues to avoid duplicates. When you create a bug report, include as many details as possible:

**Bug Report Template:**

```markdown
**Describe the bug**
A clear and concise description of what the bug is.

**To Reproduce**
Steps to reproduce the behavior:
1. Go to '...'
2. Click on '....'
3. Scroll down to '....'
4. See error

**Expected behavior**
A clear and concise description of what you expected to happen.

**Screenshots**
If applicable, add screenshots to help explain your problem.

**Environment:**
 - OS: [e.g. Windows 11]
 - .NET Version: [e.g. 8.0.1]
 - Application Version: [e.g. 1.0.0]

**Additional context**
Add any other context about the problem here.
```

### Suggesting Enhancements

Enhancement suggestions are tracked as GitHub issues. When creating an enhancement suggestion, include:

- **Clear title** and description
- **Use case**: Why would this be useful?
- **Mockups** or examples if applicable
- **Implementation ideas** if you have any

### Your First Code Contribution

Unsure where to begin? Look for issues labeled:
- `good first issue` - Good for newcomers
- `help wanted` - Extra attention needed
- `documentation` - Improvements to docs

### Pull Requests

1. Fork the repo and create your branch from `main`
2. If you've added code that should be tested, add tests
3. Ensure your code follows the coding standards
4. Update the documentation
5. Issue that pull request!

## Development Setup

### Prerequisites

- Windows 10/11
- Visual Studio 2022 or VS Code
- .NET 8.0 SDK
- Git

### Setup Steps

1. **Fork and Clone**
   ```bash
   git clone https://github.com/YOUR-USERNAME/EVERY-DAY.git
   cd EVERY-DAY
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the Project**
   ```bash
   dotnet build
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```

### Project Structure

```
EVERY-DAY/
├── Models/          # Data models
├── ViewModels/      # MVVM ViewModels
├── Views/           # UI Views (XAML)
├── Data/            # Database layer
├── Services/        # Business services
├── Helpers/         # Utility classes
└── Resources/       # Themes and resources
```

## Pull Request Process

1. **Create a Branch**
   ```bash
   git checkout -b feature/amazing-feature
   ```

2. **Make Your Changes**
   - Write clean, readable code
   - Follow the coding standards
   - Add comments for complex logic
   - Update documentation

3. **Test Your Changes**
   ```bash
   dotnet build -c Release
   # Test the application thoroughly
   ```

4. **Commit Your Changes**
   ```bash
   git add .
   git commit -m "Add amazing feature"
   ```

5. **Push to Your Fork**
   ```bash
   git push origin feature/amazing-feature
   ```

6. **Create Pull Request**
   - Go to the original repository
   - Click "New Pull Request"
   - Select your branch
   - Fill in the PR template
   - Submit!

### Pull Request Template

```markdown
## Description
Brief description of changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
- [ ] Tested on Windows 10
- [ ] Tested on Windows 11
- [ ] No errors in debug mode
- [ ] Release build works correctly

## Checklist
- [ ] My code follows the style guidelines
- [ ] I have commented my code
- [ ] I have updated the documentation
- [ ] My changes generate no new warnings
- [ ] I have tested my changes
```

## Coding Standards

### C# Style Guide

#### Naming Conventions

```csharp
// Classes: PascalCase
public class WidgetViewModel { }

// Methods: PascalCase
public void LoadData() { }

// Properties: PascalCase
public string CurrentSection { get; set; }

// Private fields: _camelCase
private string _searchText;

// Local variables: camelCase
var itemCount = 10;

// Constants: PascalCase
private const string AppName = "EveryDay";
```

#### Code Organization

```csharp
public class ExampleClass
{
    // 1. Constants
    private const string DefaultValue = "default";
    
    // 2. Fields
    private readonly IRepository _repository;
    private string _data;
    
    // 3. Constructors
    public ExampleClass(IRepository repository)
    {
        _repository = repository;
    }
    
    // 4. Properties
    public string Data 
    { 
        get => _data;
        set => _data = value;
    }
    
    // 5. Public methods
    public void PublicMethod() { }
    
    // 6. Private methods
    private void PrivateMethod() { }
}
```

#### Best Practices

1. **Use meaningful names**
   ```csharp
   // Good
   var userBlocks = GetBlocksByUser(userId);
   
   // Bad
   var data = Get(id);
   ```

2. **Keep methods small**
   ```csharp
   // Good - focused method
   public void SaveBlock(Block block)
   {
       ValidateBlock(block);
       _repository.Save(block);
       NotifyBlockSaved(block);
   }
   
   // Bad - doing too much
   public void DoEverything() { /* 100 lines */ }
   ```

3. **Use async/await properly**
   ```csharp
   // Good
   public async Task<List<Block>> LoadBlocksAsync()
   {
       return await Task.Run(() => _repository.GetAll());
   }
   ```

4. **Handle nulls safely**
   ```csharp
   // Good
   var content = block?.Content ?? "default";
   
   // Or use null-conditional
   _repository?.Save(block);
   ```

### XAML Style Guide

```xml
<!-- Use proper indentation -->
<Border Background="{DynamicResource WindowBackground}" 
        CornerRadius="12" 
        Padding="20">
    <StackPanel>
        <TextBlock Text="Title" 
                   FontSize="16" 
                   FontWeight="Bold"/>
    </StackPanel>
</Border>

<!-- Use resources for repeated values -->
<Window.Resources>
    <SolidColorBrush x:Key="PrimaryColor" Color="#3498db"/>
</Window.Resources>

<!-- Bind to ViewModels -->
<TextBox Text="{Binding Content, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"/>
```

## Commit Messages

### Format

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Types

- **feat**: New feature
- **fix**: Bug fix
- **docs**: Documentation changes
- **style**: Code style changes (formatting, etc.)
- **refactor**: Code refactoring
- **perf**: Performance improvements
- **test**: Adding tests
- **chore**: Build process or auxiliary tool changes

### Examples

```bash
# Good commit messages
feat(blocks): add markdown support for text blocks
fix(database): resolve connection leak on shutdown
docs(readme): update installation instructions
perf(startup): implement lazy loading for faster boot

# Bad commit messages
fixed stuff
update
changes
asdf
```

### Detailed Example

```
feat(themes): add custom theme support

- Allow users to create custom color themes
- Add theme import/export functionality
- Update theme manager to support custom themes
- Add UI for theme customization

Closes #123
```

## Testing Guidelines

### Manual Testing Checklist

Before submitting a PR, test:

- [ ] Application starts without errors
- [ ] All features work as expected
- [ ] No console errors or warnings
- [ ] UI is responsive
- [ ] Data persists correctly
- [ ] Theme switching works
- [ ] Window controls (min/max/close) work
- [ ] System tray integration works
- [ ] Auto-start functionality works

### Performance Testing

- [ ] Startup time < 2 seconds
- [ ] No memory leaks
- [ ] Smooth scrolling with 100+ blocks
- [ ] No UI freezing

## Documentation

### Code Comments

```csharp
/// <summary>
/// Loads all blocks for the specified section from the database.
/// </summary>
/// <param name="section">The section name to load blocks for</param>
/// <returns>List of blocks in the section</returns>
public List<Block> LoadBlocksForSection(string section)
{
    // Implementation
}
```

### README Updates

If your change affects:
- Installation process
- Usage instructions
- Features
- Configuration

Please update the README.md accordingly.

## Questions?

Feel free to:
- Open an issue for discussion
- Ask in pull request comments
- Reach out to maintainers

## Recognition

Contributors will be recognized in:
- README.md contributors section
- Release notes
- Project documentation

Thank you for contributing to EVERY-DAY! 🎉
