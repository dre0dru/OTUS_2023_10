using Enemy.Agents;
using LifecycleEvents;
using Pool;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private LifecycleManager _lifecycleManager;
        
        [SerializeField]
        private EnemyPositions _enemyPositions;

        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private GameObjectPool _pool;

        public GameObject SpawnEnemy()
        {
            var enemy = _pool.Get();
            enemy.transform.SetParent(_worldTransform);

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_character);
            
            _lifecycleManager.AddListeners(enemy.gameObject);
            
            return enemy;
        }

        public void DespawnEnemy(GameObject enemy)
        {
            _lifecycleManager.RemoveListeners(enemy.gameObject);
            
            _pool.Release(enemy);
        }
    }
}
