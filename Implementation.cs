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
            var roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

            foreach (var root in roots)
            {
                var items = root.GetComponentsInChildren<GearItem>(true);
                
                foreach (var item in items)
                {
                    bool isMatch = GearFilter.IsMatch(item.name);

                    item.m_NonInteractive = isMatch;

                    foreach (var renderer in item.GetComponentsInChildren<MeshRenderer>(true))
                    {
                        renderer.forceRenderingOff = isMatch;
                    }
                }
            }
        }
    }
}
