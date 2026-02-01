using Microsoft.Win32;
using System.Diagnostics;
using System.IO;

namespace EveryDay.Helpers
{
    public static class AutoStartHelper
    {
        private const string RunRegistryKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string AppName = "EveryDayApp";

        public static void SetAutoStart(bool enable)
        {
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunRegistryKey, true))
            {
                if (key == null) return;

                if (enable)
                {
                    var module = Process.GetCurrentProcess().MainModule;
                    if (module != null) 
                    {
                        string exePath = module.FileName;
                        // If it's a dll (dotnet run), we might need the exe wrapper. 
                        // But usually with win-exe output text, it is the exe.
                        if (exePath.EndsWith(".dll")) 
                        {
                           exePath = Path.ChangeExtension(exePath, ".exe");
                        }
                        key.SetValue(AppName, exePath);
                    }
                }
                else
                {
                    key.DeleteValue(AppName, false);
                }
            }
        }
    }
}
