using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniPin
{
    public static class ConfigManager
    {
        public static string ConfigPath = Path.Combine(LoadingManager.PersistantDataPath, "mods/InfiniPin/infinipin.json");
        public static ConfigModel Config;

        public static ConfigModel DefaultConfig
        {
            get
            {
                return new ConfigModel()
                {
                    RequiredObjectives = 3,
                    ModVersion = 1.0
                };
            }
        }

        static ConfigManager()
        {
            LoadOrCreateConfig();
        }

        private static void GetConfig()
        {
            try
            {
                LoadOrCreateConfig();
            }

            catch (Exception)
            {
                File.AppendAllText(ModLoader.LogPath, $"Failed to load/save config file at: {ConfigPath}. Using Defaults");
                Config = DefaultConfig;
            }

        }

        private static void LoadOrCreateConfig()
        {
            if (File.Exists(ConfigPath))
            {
                File.AppendAllText(ModLoader.LogPath, $"Loaded config file at: {ConfigPath}");
                var configModel = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText(ConfigPath));
                if (configModel.ModVersion != DefaultConfig.ModVersion)
                {
                    Config = new ConfigModel()
                    {
                        RequiredObjectives = configModel.RequiredObjectives == 0 ? DefaultConfig.RequiredObjectives : configModel.RequiredObjectives,
                        ModVersion = DefaultConfig.ModVersion
                    };

                    File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(Config));
                    File.AppendAllText(ModLoader.LogPath, $"Updated config file to new version at: {ConfigPath}");
                }
            }

            if (!File.Exists(ConfigPath))
            {
                Config = DefaultConfig;

                File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(Config));
                File.AppendAllText(ModLoader.LogPath, $"Created config file at: {ConfigPath}");
            }
        }
    }
}
