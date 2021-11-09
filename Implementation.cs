using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;

namespace NoMatchesMod
{
    internal class Implementation : MelonMod
    {
        public override void OnApplicationStart()
        {
            Settings.OnLoad();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            base.OnSceneWasInitialized(buildIndex, sceneName);

            UpdateGearInScene();
        }

        internal static void UpdateGearInScene()
        {
            List<GameObject> rObjs = Utils.GetRootObjects();

            foreach (GameObject rootObj in rObjs)
            {
                List<GameObject> children = new List<GameObject>();

                Utils.GetChildren(rootObj, children);

                PatchObjects(children);
            }
        }

        internal static void UpdateLootTable(LootTable instance)
        {
            for (var i = 0; i < instance.m_Prefabs.Count; i++)
            {
                var prefab = instance.m_Prefabs[i];

                if ((!Settings.options.matchesAvailable && IsMatches(prefab))
                    || (!Settings.options.firestrikerAvailable && IsFireStriker(prefab))
                    || (!Settings.options.magLensAvailable && IsMagnifyingLens(prefab))
                    || (!Settings.options.flaresAvaliable && IsFlare(prefab)))
                {
                    instance.m_Weights[i] = 0;
                }
            }
        }

        private static void PatchObjects(List<GameObject> objs)
        {
            foreach (GameObject obj in objs)
            {
                if (IsMatches(obj))
                {
                    SetVisibility(obj, Settings.options.matchesAvailable);
                }

                if (IsFireStriker(obj))
                {
                    SetVisibility(obj, Settings.options.firestrikerAvailable);
                }

                if (IsMagnifyingLens(obj))
                {
                    SetVisibility(obj, Settings.options.magLensAvailable);
                }

                if (IsFlare(obj))
                {
                    SetVisibility(obj, Settings.options.flaresAvaliable);
                }
            }
        }

        private static void SetVisibility(GameObject obj, bool visibility)
        {
            obj.GetComponent<GearItem>().m_NonInteractive = !visibility;

            for (int i = 0; i < obj.transform.childCount; i++)
            {
                GameObject child = obj.transform.GetChild(i).gameObject;

                if (child.name.EndsWith("_Old"))
                {
                    continue;
                }

                child.active = visibility;
            }
        }

        private static bool IsFlare(GameObject obj)
        {
            return obj.name == "GEAR_FlareA"
                || obj.name == "GEAR_BlueFlare";
        }

        private static bool IsMagnifyingLens(GameObject obj)
        {
            return obj.name == "GEAR_MagnifyingLens";
        }

        private static bool IsFireStriker(GameObject obj)
        {
            return obj.name == "GEAR_Firestriker";
        }

        internal static bool IsMatches(GameObject obj)
        {
            return obj.name == "GEAR_PackMatches" 
                || obj.name == "GEAR_WoodMatches";
        }
    }
}
