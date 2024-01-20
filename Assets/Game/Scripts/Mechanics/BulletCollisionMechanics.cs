using Atomic.Elements;
using Atomic.Objects;
using Atomics.Extensions;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class BulletCollisionMechanics
    {
        private readonly IAtomicValue<int> _damage;
        private readonly IAtomicAction _deathEvent;

        public BulletCollisionMechanics(IAtomicValue<int> damage, IAtomicAction deathEvent)
        {
            _damage = damage;
            _deathEvent = deathEvent;
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (!collider.TryGetComponent<IAtomicObject>(out var atomicObject))
            {
                return;
            }

            if (!atomicObject.Is(TraitsAPI.Zombie))
            {
                return;
            }

            atomicObject.GetAction<int>(ObjectAPI.TakeDamage).Invoke(_damage.Value);
            _deathEvent.Invoke();
        }
    }
}
