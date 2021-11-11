namespace NoMatches
{
    internal static class GearFilter
    {
        internal static bool IsItemDisabled(string name)
        {
            return (IsMatches(name) && !Settings.options.matchesAvailable)
                || (IsFireStriker(name) && !Settings.options.firestrikerAvailable)
                || (IsMagnifyingLens(name) && !Settings.options.magLensAvailable)
                || (IsFlare(name) && !Settings.options.flaresAvaliable);
        }

        internal static bool IsFlare(string name)
        {
            return name == "GEAR_FlareA"
                || name == "GEAR_BlueFlare";
        }

        internal static bool IsMagnifyingLens(string name)
        {
            return name == "GEAR_MagnifyingLens";
        }

        internal static bool IsFireStriker(string name)
        {
            return name == "GEAR_Firestriker";
        }

        internal static bool IsMatches(string name)
        {
            return name == "GEAR_PackMatches"
                || name == "GEAR_WoodMatches";
        }
    }
}
