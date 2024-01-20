using Atomic.Elements;
using Atomic.Objects;
using Atomics.Extensions;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class ShootMechanics
    {
        private readonly IAtomicEvent _fireEvent;
        private readonly Transform _firePoint;
        private readonly AtomicObjectComponent _bulletPrefab;
        private readonly Transform _root;

        public ShootMechanics(IAtomicEvent fireEvent, Transform firePoint, AtomicObjectComponent bulletPrefab,
            Transform root)
        {
            _fireEvent = fireEvent;
            _firePoint = firePoint;
            _bulletPrefab = bulletPrefab;
            _root = root;
        }

        public void OnEnable()
        {
            _fireEvent.Subscribe(OnFire);
        }

        public void OnDisable()
        {
            _fireEvent.Unsubscribe(OnFire);
        }

        private void OnFire()
        {
            var bullet = Object.Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            bullet.GetVariable<Vector3>(ObjectAPI.MovementDirection).Value = _root.forward;
        }
    }
}
