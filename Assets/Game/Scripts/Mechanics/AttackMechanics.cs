using Atomic.Elements;
using Atomic.Objects;
using Atomics.Extensions;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class AttackMechanics
    {
        private readonly IAtomicEvent _attackEvent;
        private readonly IAtomicValue<int> _damage;
        private readonly IAtomicValue<IAtomicObject> _target;

        public AttackMechanics(IAtomicEvent attackEvent, IAtomicValue<int> damage, IAtomicValue<IAtomicObject> target)
        {
            _attackEvent = attackEvent;
            _damage = damage;
            _target = target;
        }

        public void OnEnable()
        {
            _attackEvent.Subscribe(OnFire);
        }

        public void OnDisable()
        {
            _attackEvent.Unsubscribe(OnFire);
        }

        private void OnFire()
        {
            _target.Value.GetAction<int>(ObjectAPI.TakeDamage).Invoke(_damage.Value);
        }
    }
}
