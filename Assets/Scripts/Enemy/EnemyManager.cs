using System.Collections;
using System.Collections.Generic;
using Bullets;
using Components;
using Enemy.Agents;
using LifecycleEvents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour, IStartListener, IFinishListener, IPauseListener, IResumeListener
    {
        [SerializeField]
        private EnemySpawner _enemySpawner;

        [SerializeField]
        private BulletSystem _bulletSystem;

        [SerializeField]
        private BulletConfig _bulletConfig;

        [SerializeField]
        private int _maxSpawnedEnemies = 7;

        private readonly HashSet<GameObject> _activeEnemies = new();
        private Coroutine _spawnRoutine;

        void IStartListener.OnStartGame()
        {
            StartSpawnEnemies();
        }

        void IFinishListener.OnFinishGame()
        {
            StopSpawnEnemies();
        }

        void IResumeListener.OnResumeGame()
        {
            StartSpawnEnemies();
        }

        void IPauseListener.OnPauseGame()
        {
            StopSpawnEnemies();
        }

        private void StartSpawnEnemies()
        {
            _spawnRoutine = StartCoroutine(SpawnEnemiesRoutine());
        }

        private void StopSpawnEnemies()
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
                _spawnRoutine = null;
            }
        }

        private IEnumerator SpawnEnemiesRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                if (_activeEnemies.Count < _maxSpawnedEnemies)
                {
                    SpawnEnemy();
                }
            }
        }

        private void SpawnEnemy()
        {
            var enemy = _enemySpawner.SpawnEnemy();

            if (_activeEnemies.Add(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnDeath += OnEnemyDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnShoot += OnShoot;
            }
        }

        private void OnEnemyDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnDeath -= OnEnemyDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnShoot -= OnShoot;

                _enemySpawner.DespawnEnemy(enemy);
            }
        }

        private void OnShoot(Vector2 position, Vector2 direction)
        {
            _bulletSystem.ShootBullet(new BulletArgs
            {
                IsPlayer = false,
                PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = position,
                Velocity = direction * _bulletConfig.Speed
            });
        }
    }
}
