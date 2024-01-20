using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class TakeDamageMechanics
    {
        private readonly IAtomicVariable<int> _health;
        private readonly IAtomicObservable<int> _takeDamageEvent;
        private readonly IAtomicAction _deathEvent;

        public TakeDamageMechanics(IAtomicVariable<int> health, IAtomicObservable<int> takeDamageEvent,
            IAtomicAction deathEvent)
        {
            _health = health;
            _takeDamageEvent = takeDamageEvent;
            _deathEvent = deathEvent;
        }

        public void OnEnable()
        {
            _takeDamageEvent.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            _takeDamageEvent.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int damage)
        {
            var hitPoint = _health.Value - damage;
            _health.Value = Mathf.Max(0, hitPoint);

            if (_health.Value == 0)
            {
                _deathEvent?.Invoke();
            }
        }
    }
}
