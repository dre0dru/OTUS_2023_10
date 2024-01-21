using System;
using Atomics.Extensions;
using UnityEngine;

namespace Game.Scripts.Zombies
{
    public class ZombiesManager : MonoBehaviour
    {
        [SerializeField]
        private float _spawnInterval;

        [SerializeField]
        private float _spawnTimer;

        [SerializeField]
        private ZombiesSpawner _zombiesSpawner;

        [SerializeField]
        private int _killedZombies;

        public int KilledZombies => _killedZombies;
        public event Action KilledZombiesCountChanged;

        private void Update()
        {
            ProcessSpawnTimer();
        }

        private void ProcessSpawnTimer()
        {
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= _spawnInterval)
            {
                SpawnZombie();
                _spawnTimer = 0.0f;
            }
        }

        private void SpawnZombie()
        {
            var zombie = _zombiesSpawner.Spawn();

            zombie.GetObservable(ObjectAPI.DeathEvent).Subscribe(OnZombieDied);

            void OnZombieDied()
            {
                zombie.GetObservable(ObjectAPI.DeathEvent).Unsubscribe(OnZombieDied);
                _killedZombies++;
                KilledZombiesCountChanged?.Invoke();
            }
        }
    }
}
