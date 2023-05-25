using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniPin
{
    public class ModLoader : IModLoader
    {
        public static string LogPath = Path.Combine(LoadingManager.PersistantDataPath, "InfiniPinLog.log");

        public void OnCreated()
        {
            Harmony harmony = new Harmony("com.nemosmods.infinipin");
            harmony.PatchAll();
        }

        public void OnGameLoaded(LoadMode mode)
        {
            //
        }

        public void OnGameUnloaded()
        {
            //
        }

        public void OnReleased()
        {
            //
        }
    }
}
