using System.Collections.Generic;

namespace NoMatchesMod
{
    internal static class GearFilter
    {
        internal static Dictionary<string, bool> gearTable;

        internal static bool IsMatch(string gearName)
        {
            return gearTable.ContainsKey(gearName) && !gearTable[gearName];
        }

        internal static void Update()
        {
            gearTable = new Dictionary<string, bool>()
            {
                { "GEAR_PackMatches", Settings.options.matchesAvailable },
                { "GEAR_WoodMatches", Settings.options.matchesAvailable },
                { "GEAR_Firestriker", Settings.options.firestrikerAvailable },
                { "GEAR_MagnifyingLens", Settings.options.magLensAvailable },
                { "GEAR_FlareA", Settings.options.flaresAvaliable },
                { "GEAR_BlueFlare", Settings.options.flaresAvaliable },
            };
        }
    }
}
