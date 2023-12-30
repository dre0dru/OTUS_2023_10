using System.Linq;
using GameEngine;
using UnityEngine;

namespace SaveSystem
{
    public class UnitSaveLoader : RepositorySaveLoader<UnitsData, UnitsFacade>
    {
        public UnitSaveLoader(UnitsFacade service, IRepository repository) : base(
            service, repository)
        {
        }

        protected override UnitsData ExtractData(UnitsFacade service)
        {
            return new UnitsData()
            {
                Units = service.GetAllUnitsSnapshots().ToArray()
            };
        }

        protected override void RestoreFromData(UnitsFacade service, UnitsData data)
        {
            service.Clear();

            foreach (var snapshot in data.Units)
            {
                var unit = service.SpawnUnitByType(snapshot.Type, snapshot.Position, Quaternion.Euler(snapshot.Rotation));
                unit.RestoreFromSnapshot(snapshot);
            }
        }
    }
}
