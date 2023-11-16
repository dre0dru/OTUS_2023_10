using System.Collections;
using System.Collections.Generic;
using Bullets;
using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
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

        private void Start()
        {
            StartCoroutine(SpawnEnemiesRoutine());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private IEnumerator SpawnEnemiesRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                if (_activeEnemies.Count < _maxSpawnedEnemies)
                {
                    var enemy = _enemySpawner.SpawnEnemy();

                    if (_activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().HpEmpty += OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnShoot += OnShoot;
                    }
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().HpEmpty -= OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnShoot -= OnShoot;

                _enemySpawner.DespawnEnemy(enemy);
            }
        }

        private void OnShoot(GameObject enemy, Vector2 position, Vector2 direction)
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
