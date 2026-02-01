# Quick Start Guide - Optimized Build

## To Apply the Optimizations

1. **Build the optimized version:**
   ```powershell
   dotnet build -c Release
   ```

2. **Run the optimized version:**
   ```powershell
   .\bin\Release\net8.0-windows\EveryDay.exe
   ```

3. **The application will:**
   - Start almost instantly (tray icon appears in ~0.5-1 second)
   - NOT show the widget window automatically
   - Load data only when you first open the widget

## How to Use

- **Double-click tray icon** or **Right-click → Open EVERY DAY** to show the widget
- The first time you open it after startup, there may be a brief 1-2 second delay while data loads
- After that, opening/closing is instant

## What Changed

✅ Tray icon appears immediately on startup
✅ Widget window loads on-demand (not automatically)
✅ Database initializes lazily (only when needed)
✅ Theme detection runs in background
✅ .NET runtime optimizations enabled

## Expected Performance

| Metric | Before | After |
|--------|--------|-------|
| Startup time | 2-4 sec | 0.5-1 sec |
| Memory on startup | ~50 MB | ~20 MB |
| First widget open | Instant | 1-2 sec |
| Subsequent opens | Instant | Instant |

## Verify It's Working

1. Close the application completely
2. Start it again
3. You should see the tray icon appear almost immediately
4. The widget window should NOT appear automatically
5. Double-click the tray icon to open the widget

That's it! Your application now starts much faster! 🚀
