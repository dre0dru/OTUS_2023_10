using Enemy.Agents;
using LifecycleEvents;
using Pool;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemySpawner
    {
        private readonly EnemyPositions _enemyPositions;
        private readonly Transform _worldTransform;
        private readonly GameObjectPool _pool;
        private readonly GameObject _character;

        public EnemySpawner(EnemyPositions enemyPositions, GameObject character, Transform worldTransform,
            GameObjectPool pool)
        {
            _enemyPositions = enemyPositions;
            _worldTransform = worldTransform;
            _pool = pool;
            _character = character;
        }

        public GameObject SpawnEnemy()
        {
            var enemy = _pool.Get();
            enemy.transform.SetParent(_worldTransform);

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = _enemyPositions.RandomAttackPosition();

            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_character);

            return enemy;
        }

        public void DespawnEnemy(GameObject enemy)
        {
            _pool.Release(enemy);
        }
    }
}
