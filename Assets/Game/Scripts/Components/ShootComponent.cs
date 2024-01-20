using System;
using Atomic.Elements;
using Atomic.Objects;
using Atomics.Extensions;
using Game.Scripts.Bullets;
using Game.Scripts.Mechanics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class ShootComponent
    {
        [Header("General")]
        [Get(ObjectAPI.AttackRequest)]
        [SerializeField]
        private FireRequest _fireRequest;

        [ReadOnly]
        [Get(ObjectAPI.AttackEvent)]
        [SerializeField]
        private AtomicEvent _fireEvent;

        [Get(ObjectAPI.CanShoot)]
        [SerializeField]
        private AndExpression _canShoot;

        [Header("Shot")]
        [SerializeField]
        private AtomicValue<float> _shotCooldown = new(0.5f);

        [SerializeField]
        private AtomicVariable<float> _shotTimer;

        [Header("Ammo")]
        [SerializeField]
        private AtomicVariable<int> _ammoCount;

        [SerializeField]
        private AtomicVariable<int> _maxAmmoCount;

        [SerializeField]
        private AtomicValue<float> _ammoReplenishCooldown;

        [SerializeField]
        private AtomicVariable<float> _ammoReplenishTimer;

        [Header("References")]
        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private Bullet _bulletPrefab;

        private ShootMechanics _shootMechanics;
        private AmmoMechanics _ammoMechanics;
        private CooldownMechanics _cooldownMechanics;

        public void Compose(IAtomicObject atomicObject)
        {
            _shootMechanics = new ShootMechanics(_fireEvent, _firePoint, _bulletPrefab,
                atomicObject.Get<Transform>(ObjectAPI.Root));
            _ammoMechanics = new AmmoMechanics(_maxAmmoCount, _ammoCount, _ammoReplenishCooldown, _ammoReplenishTimer, _fireEvent);
            _cooldownMechanics = new CooldownMechanics(_shotTimer, _fireEvent);

            _fireRequest.Compose(_canShoot);

            _canShoot.AddMember(_shotTimer.AsFunction(timer => timer >= _shotCooldown));
            _canShoot.AddMember(_ammoCount.AsFunction(count => count > 0));
            _canShoot.AddMember(atomicObject.GetValue<bool>(ObjectAPI.IsDead).Negate());
        }

        public void OnEnable()
        {
            _shootMechanics.OnEnable();
            _cooldownMechanics.OnEnable();
            _ammoMechanics.OnEnable();
        }

        public void Update()
        {
            _ammoMechanics.Update();
            _cooldownMechanics.Update();
        }

        public void OnDisable()
        {
            _shootMechanics.OnDisable();
            _cooldownMechanics.OnDisable();
            _ammoMechanics.OnDisable();
        }
    }
}
