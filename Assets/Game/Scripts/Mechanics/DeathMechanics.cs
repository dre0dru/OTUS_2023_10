using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class DeathMechanics
    {
        private readonly IAtomicVariable<bool> _isDead;
        private readonly IAtomicObservable _deathEvent;

        public DeathMechanics(IAtomicVariable<bool> isDead, IAtomicObservable deathEvent)
        {
            _isDead = isDead;
            _deathEvent = deathEvent;
        }

        public void OnEnable()
        {
            _deathEvent.Subscribe(OnDeath);
        }

        public void OnDisable()
        {
            _deathEvent.Unsubscribe(OnDeath);
        }

        private void OnDeath()
        {
            if (_isDead.Value)
            {
                return;
            }

            _isDead.Value = true;
            Debug.Log($"Entity is dead");
        }
    }
}
