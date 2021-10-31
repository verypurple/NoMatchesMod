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
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            base.OnSceneWasInitialized(buildIndex, sceneName);

            if (ExperienceModeManager.GetCurrentExperienceModeType() != ExperienceModeType.Interloper)
            {
                return;
            }

            List<GameObject> rObjs = Utils.GetRootObjects();

            foreach (GameObject rootObj in rObjs)
            {
                List<GameObject> children = new List<GameObject>();

                Utils.GetChildren(rootObj, children);

                PatchObjects(children);
            }
        }

        internal static void RemoveMatchesFromLootTable(LootTable instance)
        {
            if (ExperienceModeManager.GetCurrentExperienceModeType() != ExperienceModeType.Interloper)
            {
                return;
            }

            for (var i = 0; i < instance.m_Prefabs.Count; i++)
            {
                if (IsMatches(instance.m_Prefabs[i]))
                {
                    instance.m_Weights[i] = 0;
                }
            }
        }

        internal static void PatchObjects(List<GameObject> objs)
        {
            foreach (GameObject obj in objs)
            {
                if (IsMatches(obj))
                {
                    UnityEngine.Object.Destroy(obj);
                }
            }
        }

        internal static bool IsMatches(GameObject obj)
        {
            return obj.name == "GEAR_PackMatches" 
                || obj.name == "GEAR_WoodMatches";
        }
    }
}
