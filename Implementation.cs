using MelonLoader;
using UnityEngine;
using System.Linq;

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
            var items = UnityEngine.SceneManagement.SceneManager
                .GetAllScenes()
                .SelectMany(s => s.GetRootGameObjects())
                .SelectMany(r => r.GetComponentsInChildren<GearItem>(true))
                .Where(gi => GearFilter.Has(gi.name));

            foreach (var item in items)
            {
                var isAvailable = GearFilter.IsAvailable(item.name);

                item.m_NonInteractive = !isAvailable;

                foreach (var renderer in item.GetComponentsInChildren<MeshRenderer>(true))
                {
                    renderer.forceRenderingOff = !isAvailable;
                }

                var lodGroup = item.gameObject.GetComponent<LODGroup>();

                if (lodGroup)
                {
                    lodGroup.enabled = !isAvailable;
                }
            }
        }
    }
}