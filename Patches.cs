using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;

namespace NoMatchesMod
{
    internal class Patches
    {
        [HarmonyPatch(typeof(LootTable), "GetPrefab")]
        internal static class LootTable_GetPrefab
        {
            private static void Prefix(LootTable __instance)
            {
                Implementation.UpdateLootTable(__instance);
            }
        }

        [HarmonyPatch(typeof(LootTable), "GetRandomGearPrefab")]
        internal static class LootTable_GetRandomGearPrefab
        {
            private static void Prefix(LootTable __instance)
            {
                Implementation.UpdateLootTable(__instance);
            }
        }
    }
}
