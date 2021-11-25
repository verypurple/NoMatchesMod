using System.Reflection;
using ModSettings;

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

        [Name("Torchbearer Mode")]
        [Description("You start with a lit torch in your hand. It is your only way to make fire. Don't let it go out!")]
        public bool torchbearerEnabled = false;

        protected override void OnConfirm()
        {
            base.OnConfirm();

            GearFilter.Update();
            Implementation.UpdateGearInScene();
        }

        protected override void OnChange(FieldInfo field, object oldValue, object newValue)
        {
            base.OnChange(field, oldValue, newValue);

            if (field.Name == nameof(torchbearerEnabled))
            {
                matchesAvailable = (bool)oldValue;
                firestrikerAvailable = (bool)oldValue;
                magLensAvailable = (bool)oldValue;
                flaresAvaliable = (bool)oldValue;
            }

            RefreshGUI();
        }
    }

    internal static class Settings
    {
        internal static readonly NoMatchesSettings options = new NoMatchesSettings();

        public static void OnLoad()
        {
            options.AddToModSettings("No Matches", MenuType.Both);
        }
    }
}
