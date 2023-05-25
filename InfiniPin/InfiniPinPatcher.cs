using HarmonyLib;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;

namespace InfiniPin
{
    [HarmonyPatch(typeof(MinimapPanel), "SetPin")]
    public class InfiniPinPatcher
    {
        public static void Prefix(ref float ___pinVisibleDistance)
        {
            MinimapDrawer drawer = GameObject.FindObjectsOfType<MinimapDrawer>().Where(m => m.Pins.Count > 0).FirstOrDefault();
            int totalPins = drawer.Pins.Where(p => p.PinType == MapPinType.Objective).Count();

            if (totalPins >= ConfigManager.Config.RequiredObjectives)
            {
                ___pinVisibleDistance = 100.0f;
            }
            else
            {
                ___pinVisibleDistance = .07f;
            }
        }
    }
}