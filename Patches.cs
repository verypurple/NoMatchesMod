using HarmonyLib;

namespace NoMatchesMod
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
                    if (GearFilter.IsMatch(itemName))
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
                    if (GearFilter.IsMatch(__instance.m_Prefabs[i].name))
                    {
                        __instance.m_Weights[i] = 0;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(StartSettings), "SetWeather")]
        internal static class Test
        {
            private static void Postfix(StartSettings __instance)
            {
                if (!Settings.options.torchbearerEnabled || GameManager.m_ActiveScene == "MainMenu")
                {
                    return;
                }

                var wind = GameManager.GetWindComponent();
                wind.StartPhaseImmediate(WindDirection.East, WindStrength.Calm);

                var manager = GameManager.GetPlayerManagerComponent();
                var torch = manager.InstantiateItemInPlayerInventory("GEAR_Torch");
                manager.EquipItem(torch, false);

                torch.gameObject.GetComponent<TorchItem>().SetState(TorchState.Burning);
            }
        }
    }
}
