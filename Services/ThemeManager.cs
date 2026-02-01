using System;
using System.Windows;
using Microsoft.Win32;

namespace EveryDay.Services
{
    public class ThemeManager
    {
        private static ThemeManager? _instance;
        public static ThemeManager Instance => _instance ??= new ThemeManager();

        public enum Theme { Light, Dark }
        public Theme CurrentTheme { get; private set; }

        private ThemeManager() { }

        public void ApplyTheme(Theme theme)
        {
            CurrentTheme = theme;
            string dictPath = theme == Theme.Dark ? "Resources/DarkTheme.xaml" : "Resources/LightTheme.xaml";
            
            try 
            {
                var uri = new Uri(dictPath, UriKind.RelativeOrAbsolute);
                ResourceDictionary resourceDict = new ResourceDictionary() { Source = uri };
                
                // We want to replace the theme dictionary, not clear all (which might remove controls styles)
                // But for this MVP, assuming app.xaml only has theme, clearing is okay.
                // Ideally we tag the theme dictionary and replace only that.
                System.Windows.Application.Current.Resources.MergedDictionaries.Clear();
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            catch (Exception ex)
            {
                // Fallback or log
                System.Diagnostics.Debug.WriteLine($"Error loading theme: {ex.Message}");
            }
        }
        
        public void DetectSystemTheme()
        {
             try
             {
                 using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                 {
                     if (key != null)
                     {
                         var value = key.GetValue("AppsUseLightTheme");
                         if (value is int i && i == 1)
                         {
                             ApplyTheme(Theme.Light);
                             return;
                         }
                     }
                 }
             }
             catch { }
             
             ApplyTheme(Theme.Dark); // Default
        }
    }
}
