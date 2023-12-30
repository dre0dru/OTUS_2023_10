using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public static class UnitMangerExtensions
    {
        public static IEnumerable<Unit.Snapshot> GetAllUnitsSnapshots(this UnitManager unitManager)
        {
            return unitManager.GetAllUnits().Select(unit => unit.GetSnapshot());
        }

        public static void ClearAllUnits(this UnitManager unitManager)
        {
            var units = unitManager.GetAllUnits().ToArray();
            foreach (var unit in units)
            {
                unitManager.DestroyUnit(unit);
            }
        }
    }
}
