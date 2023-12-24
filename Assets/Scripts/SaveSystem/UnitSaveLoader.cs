using System;
using System.Linq;
using GameEngine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SaveSystem
{
    public class UnitSaveLoader : RepositorySaveLoader<UnitSaveLoader.Data, UnitManager>
    {
        [Serializable]
        public struct Data
        {
            public Unit.Snapshot[] Units;
        }

        private readonly UnitPrefabs _unitPrefabs;

        public UnitSaveLoader(UnitManager service, IRepository<Data> repository, UnitPrefabs unitPrefabs) : base(
            service, repository)
        {
            _unitPrefabs = unitPrefabs;
        }

        protected override Data ExtractData(UnitManager service)
        {
            return new Data()
            {
                Units = service.GetAllUnits().Select(unit => unit.GetSnapshot())
                    .ToArray()
            };
        }

        protected override void RestoreFromData(UnitManager service, Data data)
        {
            ClearManagerUnits();
            ClearSceneUnits();

            foreach (var snapshot in data.Units)
            {
                var prefab = _unitPrefabs.GetPrefabFor(snapshot.Type);
                var unit = service.SpawnUnit(prefab, snapshot.Position, Quaternion.Euler(snapshot.Rotation));
                unit.RestoreFromSnapshot(snapshot);
            }

            void ClearManagerUnits()
            {
                var units = service.GetAllUnits().ToArray();
                foreach (var unit in units)
                {
                    service.DestroyUnit(unit);
                }
            }

            void ClearSceneUnits()
            {
                foreach (var unit in GetUnitsInScene())
                {
                    Object.Destroy(unit.gameObject);
                }
            }
        }

        protected override void SetupDefaultData(UnitManager service)
        {
            //решил оставить дефолтные данные как данные со сцены
            service.SetupUnits(GetUnitsInScene());
        }

        private Unit[] GetUnitsInScene()
        {
            return Object.FindObjectsOfType<Unit>(true);
        }
    }
}
