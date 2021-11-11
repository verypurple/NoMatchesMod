using HarmonyLib;
using MelonLoader;

namespace NoMatches
{
    internal class Patches
    {
        [HarmonyPatch(typeof(Container), "PopulateWithScriptedGear")]
        internal static class Container_PopulateWithScriptedGear
        {
            private static void Postfix(Container __instance)
            {
                foreach (var itemName in __instance.m_GearToInstantiate.ToArray())
                {
                    if (GearFilter.IsItemDisabled(itemName))
                    {
                        __instance.m_GearToInstantiate.Remove(itemName);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(LootTable), "GetRandomGearPrefab")]
        internal static class LootTable_GetRandomGearPrefab
        {
            private static void Prefix(LootTable __instance)
            {
                for (var i = 0; i < __instance.m_Prefabs.Count; i++)
                {
                    if (GearFilter.IsItemDisabled(__instance.m_Prefabs[i].name))
                    {
                        __instance.m_Weights[i] = 0;
                    }
                }
            }
        }
    }
}
