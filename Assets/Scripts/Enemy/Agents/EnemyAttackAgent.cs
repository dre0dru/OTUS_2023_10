using System;
using Components;
using LifecycleEvents;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public event Action<Vector2, Vector2> OnShoot;

        [SerializeField]
        private WeaponComponent _weaponComponent;

        [SerializeField]
        private EnemyMoveAgent _moveAgent;

        [SerializeField]
        private float _countdown;

        private GameObject _target;
        private float _currentTime;

        public void ProcessShooting()
        {
            if (!CanShoot())
            {
                return;
            }

            ShootByCountdown();
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
            OnShoot?.Invoke(startPosition, direction);
        }

        private void Reset()
        {
            _currentTime = _countdown;
        }
    }
}
