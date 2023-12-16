using System.Collections.Generic;
using Components;
using Enemy.Agents;
using LifecycleEvents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : IStartListener, IFinishListener, IPauseListener, IResumeListener,
        IFixedUpdateListener
    {
        private const int MaxSpawnedEnemies = 7;
        private const float EnemiesSpawnFrequencySec = 1;

        private readonly EnemySpawner _enemySpawner;
        private readonly EnemyShooter _enemyShooter;
        private readonly HashSet<GameObject> _activeEnemies = new();
        private readonly List<GameObject> _enemiesCache = new();

        private bool _isSpawningEnemies;
        private float _lastSpawnedEnemyTime;

        public EnemyManager(EnemySpawner enemySpawner, EnemyShooter enemyShooter)
        {
            _enemyShooter = enemyShooter;
            _enemySpawner = enemySpawner;
        }

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

        void IFixedUpdateListener.OnFixedUpdate(float deltaTime)
        {
            SpawnEnemies();
            UpdateEnemies();
        }

        private void StartSpawnEnemies()
        {
            _isSpawningEnemies = true;
        }

        private void StopSpawnEnemies()
        {
            _isSpawningEnemies = false;
        }

        private void SpawnEnemies()
        {
            if (!_isSpawningEnemies)
            {
                return;
            }

            var currentTime = Time.time;
            if (currentTime + EnemiesSpawnFrequencySec < _lastSpawnedEnemyTime ||
                _activeEnemies.Count >= MaxSpawnedEnemies)
            {
                return;
            }

            _lastSpawnedEnemyTime = currentTime;

            SpawnEnemy();
        }
        
        private void SpawnEnemy()
        {
            var enemy = _enemySpawner.SpawnEnemy();

            if (_activeEnemies.Add(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnDeath += OnEnemyDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnShoot += _enemyShooter.Shoot;
            }
        }

        private void UpdateEnemies()
        {
            _enemiesCache.Clear();
            _enemiesCache.AddRange(_activeEnemies);

            for (int i = 0; i < _enemiesCache.Count; i++)
            {
                var enemy = _enemiesCache[i];
                
                //Знаю, что GetComponent в апдейтах плохо, но это не по теме ДЗ
                enemy.GetComponent<EnemyAttackAgent>().ProcessShooting();
                enemy.GetComponent<EnemyMoveAgent>().ProcessMovement();
            }
        }

        private void OnEnemyDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnDeath -= OnEnemyDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnShoot -= _enemyShooter.Shoot;

                _enemySpawner.DespawnEnemy(enemy);
            }
        }
    }
}
