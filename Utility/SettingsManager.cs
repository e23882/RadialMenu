using System;
using System.IO;
using System.Text.Json;

namespace RadialMenu.Utility
{
    public static class SettingsManager
    {
        private static readonly string SettingsFilePath = "settings.json";

        public static event EventHandler SettingsChanged;

        public static AppSettings LoadSettings()
        {
            if (File.Exists(SettingsFilePath))
            {
                string jsonString = File.ReadAllText(SettingsFilePath);
                return JsonSerializer.Deserialize<AppSettings>(jsonString);
            }
            return new AppSettings(); // Return default settings if file doesn't exist
        }

        public static void SaveSettings(AppSettings settings)
        {
            string jsonString = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, jsonString);
            OnSettingsChanged();
        }

        private static void OnSettingsChanged()
        {
            SettingsChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
