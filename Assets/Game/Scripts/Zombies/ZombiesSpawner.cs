using UnityEngine;

namespace Game.Scripts.Zombies
{
    public class ZombiesSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _spawnPoints;

        [SerializeField]
        private int _currentSpawnPoint;

        [SerializeField]
        private ZombiesFactory _zombiesFactory;

        public Zombie Spawn()
        {
            return _zombiesFactory.Create(GetSpawnPoint().position);
        }

        private Transform GetSpawnPoint()
        {
            var point = _spawnPoints[_currentSpawnPoint % _spawnPoints.Length];
            _currentSpawnPoint++;

            return point;
        }
    }
}
