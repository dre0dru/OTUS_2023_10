using Components;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public delegate void ShootHandler(GameObject enemy, Vector2 position, Vector2 direction);

        public event ShootHandler OnShoot;

        [SerializeField]
        private WeaponComponent _weaponComponent;

        [SerializeField]
        private EnemyMoveAgent _moveAgent;

        [SerializeField]
        private float _countdown;

        private GameObject _target;
        private float _currentTime;

        private void FixedUpdate()
        {
            if (!CanShoot())
            {
                return;
            }

            ShootByCountdown();
        }

        private void Reset()
        {
            _currentTime = _countdown;
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        private bool CanShoot()
        {
            return _moveAgent.IsDestinationReached && 
                   _target.GetComponent<HitPointsComponent>().IsHitPointsExists();
        }

        private void ShootByCountdown()
        {
            _currentTime -= Time.fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Shoot();
                _currentTime += _countdown;
            }
        }

        private void Shoot()
        {
            var startPosition = _weaponComponent.Position;
            var vector = (Vector2)_target.transform.position - startPosition;
            var direction = vector.normalized;
            OnShoot?.Invoke(gameObject, startPosition, direction);
        }
    }
}
