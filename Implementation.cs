using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace NoMatchesMod
{
    internal class Implementation : MelonMod
    {
        public override void OnApplicationStart()
        {
            Settings.OnLoad();
            GearFilter.Update();
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
                GearItem gearItem = obj.GetComponent<GearItem>();

                if (gearItem)
                {
                    bool isMatch = GearFilter.IsMatch(obj.name);

                    gearItem.m_NonInteractive = isMatch;

                    foreach (var renderer in obj.GetComponentsInChildren<MeshRenderer>(true))
                    {
                        // Skip placeholder assets not used in the game
                        if (renderer.name.EndsWith("_Old"))
                        {
                            continue;
                        }

                        renderer.enabled = !isMatch;
                    }
                }
            }
        }
    }
}
