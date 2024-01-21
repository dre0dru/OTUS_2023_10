using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class HealthComponent
    {
        [Get(ObjectAPI.Health)]
        [SerializeField]
        private AtomicVariable<int> _health;

        [Get(ObjectAPI.IsDead)]
        [SerializeField]
        private AtomicVariable<bool> _isDead;

        [Get(ObjectAPI.DeathEvent)]
        [SerializeField]
        private AtomicEvent _deathEvent;

        [Get(ObjectAPI.TakeDamage)]
        [SerializeField]
        private AtomicEvent<int> _takeDamageEvent = new();

        private DeathMechanics _deathMechanics;
        private TakeDamageMechanics _takeDamageMechanics;

        public void Compose()
        {
            _deathMechanics = new DeathMechanics(_isDead, _deathEvent);
            _takeDamageMechanics = new TakeDamageMechanics(_health, _takeDamageEvent, _deathEvent);
        }

        public void OnEnable()
        {
            _deathMechanics.OnEnable();
            _takeDamageMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _deathMechanics.OnDisable();
            _takeDamageMechanics.OnDisable();
        }
    }
}
