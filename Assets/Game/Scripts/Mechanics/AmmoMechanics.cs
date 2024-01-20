using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class AmmoMechanics
    {
        private readonly IAtomicValue<int> _maxAmmo;
        private readonly IAtomicVariable<int> _ammo;
        private readonly IAtomicValue<float> _ammoReplenishCooldown;
        private readonly IAtomicVariable<float> _ammoReplenishTimer;
        private readonly IAtomicEvent _fireEvent;

        public AmmoMechanics(IAtomicValue<int> maxAmmo, IAtomicVariable<int> ammo,
            IAtomicValue<float> ammoReplenishCooldown, IAtomicVariable<float> ammoReplenishTimer,
            IAtomicEvent fireEvent)
        {
            _maxAmmo = maxAmmo;
            _ammo = ammo;
            _ammoReplenishCooldown = ammoReplenishCooldown;
            _ammoReplenishTimer = ammoReplenishTimer;
            _fireEvent = fireEvent;
        }

        public void OnEnable()
        {
            _fireEvent.Subscribe(OnFire);
        }

        public void Update()
        {
            if (_ammo.Value >= _maxAmmo.Value)
            {
                return;
            }

            if (_ammoReplenishTimer.Value >= _ammoReplenishCooldown.Value)
            {
                _ammoReplenishTimer.Value = 0.0f;
                _ammo.Value++;
            }

            _ammoReplenishTimer.Value += Time.deltaTime;
        }

        public void OnDisable()
        {
            _fireEvent.Unsubscribe(OnFire);
        }

        private void OnFire()
        {
            _ammo.Value--;
        }
    }
}
