using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace NoMatches
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
            List<GameObject> rootObjects = Utils.GetRootObjects();

            foreach (GameObject rootObj in rootObjects)
            {
                List<GameObject> children = new List<GameObject>();

                Utils.GetChildren(rootObj, children);

                PatchObjects(children);
            }
        }

        private static void PatchObjects(List<GameObject> objs)
        {
            foreach (GameObject obj in objs)
            {
                if (GearFilter.IsMatches(obj.name)
                    || GearFilter.IsFireStriker(obj.name)
                    || GearFilter.IsMagnifyingLens(obj.name)
                    || GearFilter.IsFlare(obj.name))
                {
                    SetVisibility(obj, Settings.options.matchesAvailable);
                }
            }
        }

        private static void SetVisibility(GameObject obj, bool isVisible)
        {
            obj.GetComponent<GearItem>().m_NonInteractive = !isVisible;

            foreach (var component in obj.GetComponentsInChildren<MeshRenderer>(true))
            {
                // Skip placeholder assets not used in the game
                if (component.name.EndsWith("_Old"))
                {
                    continue;
                }

                component.enabled = isVisible;
            }
        }
    }
}
