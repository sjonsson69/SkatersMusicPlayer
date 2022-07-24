using System.Configuration;

namespace SkatersMusicPlayer
{
    internal static class settings
    {
        private static string isStringNull(string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }
            return value;
        }

        public static bool pauseMusicEnabled
        {
            get
            {
                string value = ConfigurationManager.AppSettings["PauseMusicEnabled"];
                value = isStringNull(value);
                return value.ToLower() == "true";
            }
            set
            {
                saveAppSettings("PauseMusicEnabled", (value ? "true" : "false"));
            }
        }

        public static decimal pauseMusicDelay
        {
            get
            {
                string value = ConfigurationManager.AppSettings["PauseMusicDelay"];
                value = isStringNull(value);
                decimal.TryParse(value, out decimal delay);
                return delay;
            }
            set
            {
                saveAppSettings("PauseMusicDelay", value.ToString());
            }
        }

        public static float pauseVolume
        {
            get
            {
                string value = ConfigurationManager.AppSettings["PauseVolume"];
                if (string.IsNullOrEmpty(value))
                {
                    value = "0,1";
                }
                float.TryParse(value, out float volume);
                return volume;
            }
            set
            {
                saveAppSettings("PauseVolume", value.ToString());
            }
        }


        public static string warmupMusicDirectory
        {
            get
            {
                string value = ConfigurationManager.AppSettings["WarmupMusicDirectory"];
                value = isStringNull(value);
                return value;
            }
            set
            {
                saveAppSettings("WarmupMusicDirectory", value);
            }
        }

        public static string breakMusicDirectory
        {
            get
            {
                string value = ConfigurationManager.AppSettings["BreakMusicDirectory"];
                value = isStringNull(value);
                return value;
            }
            set
            {
                saveAppSettings("BreakMusicDirectory", value);
            }
        }

        private static void saveAppSettings(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
