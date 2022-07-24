using System;
using System.Configuration;
using System.Text;

namespace SkatersMusicPlayer
{
    internal static class settings
    {
        private static string isStringNull(string value)
        {
            return isStringNull(value, string.Empty);
        }
        private static string isStringNull(string value, string defaultValue)
        {
            if (value == null)
            {
                value = defaultValue;
            }
            return value;
        }

        public static bool pauseMusicEnabled
        {
            get
            {
                string value = ConfigurationManager.AppSettings["PauseMusicEnabled"];
                value = isStringNull(value, "true");
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
                value = isStringNull(value, "20");
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
                value = isStringNull(value, "0,1");
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
                value = isStringNull(value, @".\WarmupMusic");
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
                value = isStringNull(value, @".\BreakMusic");
                return value;
            }
            set
            {
                saveAppSettings("BreakMusicDirectory", value);
            }
        }

        public static string FSMServer
        {
            get
            {
                string value = ConfigurationManager.AppSettings["FSMServer"];
                value = isStringNull(value, "127.0.0.1");
                return value;
            }
            set
            {
                saveAppSettings("FSMServer", value);
            }
        }

        public static string FSMPort
        {
            get
            {
                string value = ConfigurationManager.AppSettings["FSMPort"];
                value = isStringNull(value, "3306");
                return value;
            }
            set
            {
                saveAppSettings("FSMPort", value);
            }
        }
        public static string FSMUsername
        {
            get
            {
                string value = ConfigurationManager.AppSettings["FSMUsername"];
                value = isStringNull(value, "Ogu3GfB1Ni0oLJgB/c5PNw==");
                value = Crypt.crypt.kryptera(value, string.Empty, false);
                return value;
            }
            set
            {
                saveAppSettings("FSMUsername", Crypt.crypt.kryptera(value, string.Empty, true));
            }
        }

        public static string FSMPassword
        {
            get
            {
                string value = ConfigurationManager.AppSettings["FSMPassword"];
                value = isStringNull(value, "GDqbs0arr3WRjT56FjoBab4QafEv+AfwBff94bNpge4=");
                value = Crypt.crypt.kryptera(value, string.Empty, false);
                return value;
            }
            set
            {
                saveAppSettings("FSMPassword", Crypt.crypt.kryptera(value, string.Empty, true));
            }
        }

        private static void saveAppSettings(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //Does the key exist?
            var s = config.AppSettings.Settings[key];
            if (s == null)
            {//No, create key
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {//Update key
                s.Value = value;
            }

            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
