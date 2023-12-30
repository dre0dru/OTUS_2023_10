using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
    public class UnitsFacade
    {
        private readonly UnitManager _unitManager;
        private readonly UnitPrefabs _unitPrefabs;

        public UnitsFacade(UnitManager unitManager, UnitPrefabs unitPrefabs)
        {
            _unitManager = unitManager;
            _unitPrefabs = unitPrefabs;
        }

        public Unit SpawnUnitByType(string unitType, Vector3 position, Quaternion rotation)
        {
            var prefab = _unitPrefabs.GetPrefabFor(unitType);
            return _unitManager.SpawnUnit(prefab, position, rotation);
        }

        public IEnumerable<Unit.Snapshot> GetAllUnitsSnapshots()
        {
            return _unitManager.GetAllUnitsSnapshots();
        }

        public void Clear()
        {
            _unitManager.ClearAllUnits();
        }
    }
}
