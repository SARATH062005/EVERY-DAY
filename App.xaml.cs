using System;
using System.Threading;
using System.Windows;
using EveryDay.Helpers;
using EveryDay.Services;
using EveryDay.Views;
using Forms = System.Windows.Forms;
using Drawing = System.Drawing;

namespace EveryDay
{
    public partial class App : System.Windows.Application
    {
        private static Mutex? _mutex;
        private Forms.NotifyIcon? _trayIcon;
        private WidgetWindow? _widgetWindow;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.DispatcherUnhandledException += (s, args) =>
            {
                System.IO.File.WriteAllText("crash_log.txt", args.Exception.ToString());
                System.Windows.MessageBox.Show("Crash: " + args.Exception.Message);
                args.Handled = true;
            };

            const string appName = "EveryDaySingleInstanceMutex";
            bool createdNew;

            _mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                Shutdown();
                return;
            }

            // AutoStart - do this first, it's lightweight
            AutoStartHelper.SetAutoStart(true);

            // Tray Icon - show immediately for fast startup
            _trayIcon = new Forms.NotifyIcon
            {
                Icon = Drawing.SystemIcons.Application,
                Visible = true,
                Text = "EVERY DAY - Your Daily Workspace"
            };

            _trayIcon.DoubleClick += (s, args) => 
            {
                 ToggleWidget();
            };

            var menu = new Forms.ContextMenuStrip();
            menu.Items.Add("Open EVERY DAY", null, (s, args) => ShowWidget());
            menu.Items.Add("Hide", null, (s, args) => HideWidget());
            menu.Items.Add("-");
            menu.Items.Add("Exit", null, (s, args) => Shutdown());
            _trayIcon.ContextMenuStrip = menu;

            // Theme - initialize lazily in background
            System.Threading.Tasks.Task.Run(() => 
            {
                ThemeManager.Instance.DetectSystemTheme();
            });

            // Don't show widget on startup - let it load in background when user needs it
            // This makes startup much faster
        }

        private void ShowWidget()
        {
            if (_widgetWindow == null)
            {
                _widgetWindow = new WidgetWindow();
                _widgetWindow.Closed += (s, args) => _widgetWindow = null;
                _widgetWindow.Show();
                _widgetWindow.Activate();
            }
            else
            {
                _widgetWindow.Show();
                _widgetWindow.Activate();
            }
        }

        private void HideWidget()
        {
            _widgetWindow?.Hide();
        }
        
        private void ToggleWidget()
        {
            if (_widgetWindow != null && _widgetWindow.IsVisible)
            {
                _widgetWindow.Hide();
            }
            else
            {
                ShowWidget();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (_trayIcon != null)
            {
                _trayIcon.Visible = false;
                _trayIcon.Dispose();
            }
            _mutex?.Dispose();
        }
    }
}
