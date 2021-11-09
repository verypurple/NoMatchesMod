using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModSettings;
using UnityEngine;

namespace NoMatchesMod
{
    internal class NoMatchesSettings : JsonModSettings
    {
        [Name("Matches available")]
        [Description("Determines if Matches can be found in the world and when searching containers.")]
        public bool matchesAvailable = true;

        [Name("Firestriker available")]
        [Description("Determines if Firestriker can be found in the world and when searching containers.")]
        public bool firestrikerAvailable = true;

        [Name("Magnifying Lens available")]
        [Description("Determines if Magnifying Lens can be found in the world and when searching containers.")]
        public bool magLensAvailable = true;

        [Name("Flares available")]
        [Description("Determines if Flares can be found in the world and when searching containers.")]
        public bool flaresAvaliable = true;

        protected override void OnConfirm()
        {
            base.OnConfirm();

            Implementation.UpdateGearInScene();
        }
    }

    internal static class Settings
    {
        internal static readonly NoMatchesSettings options = new NoMatchesSettings();

        public static void OnLoad()
        {
            options.AddToModSettings("No Matches", MenuType.InGameOnly);
        }
    }
}
