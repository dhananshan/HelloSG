using Newtonsoft.Json;
using System;
using System.IO;

namespace HelloSG.Common.Config
{
    public static class AppSettings<T> where T : class
    {
        static AppSettings()
        {
            LoadAPISettingsInternal();
        }

        private static void LoadAPISettingsInternal()
        {
            if (Config != null)
                return;

            string appsettingsFilePath = GetAppSettingsPath();

            using (var r = new StreamReader(appsettingsFilePath))
            {
                string jsonSettings = r.ReadToEnd();

                Config = JsonConvert.DeserializeObject<T>(jsonSettings);
            }
        }

        private static string GetAppSettingsPath()
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrEmpty(environmentName))
                return Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            else
            {
                string appsettingsFilePath = $"appsettings.{environmentName}.json";
                return Path.Combine(Directory.GetCurrentDirectory(), appsettingsFilePath);
            }
        }

        //private static bool _isSettingsLoaded = false;

        public static T Config { get; set; }
    }
}

