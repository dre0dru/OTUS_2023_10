using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameEngine
{
    [CreateAssetMenu(menuName = "UnitPrefabs", fileName = "UnitPrefabs")]
    public class UnitPrefabs : ScriptableObject
    {
        [SerializeField]
        private List<Unit> _prefabs;

        public Unit GetPrefabFor(string unitType)
        {
            return _prefabs.Single(unit => unitType == unit.Type);
        }
    }
}
