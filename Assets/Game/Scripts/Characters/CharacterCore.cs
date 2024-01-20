using System;
using Atomic.Elements;
using Atomic.Objects;
using Atomics.Extensions;
using Game.Scripts.Components;
using UnityEngine;

namespace Game.Scripts.Characters
{
    [Serializable]
    public class CharacterCore
    {
        [Get(ObjectAPI.Root)]
        [SerializeField]
        private Transform _root;

        [Section]
        [SerializeField]
        private MovementComponent _movementComponent;

        [Section]
        [SerializeField]
        private RotationComponent _rotationComponent;

        [Section]
        [SerializeField]
        private HealthComponent _healthComponent;

        [Section]
        [SerializeField]
        private ShootComponent _shootComponent;

        public void Compose(IAtomicObject atomicObject)
        {
            _movementComponent.Compose(atomicObject);
            _rotationComponent.Compose(atomicObject);
            _healthComponent.Compose();
            _shootComponent.Compose(atomicObject);

            var canMove = atomicObject.GetExpression<bool>(ObjectAPI.CanMove);
            var isDead = atomicObject.GetValue<bool>(ObjectAPI.IsDead);
            canMove.AddMember(isDead.Negate());
        }

        public void OnEnable()
        {
            _healthComponent.OnEnable();
            _shootComponent.OnEnable();
        }

        public void Update()
        {
            _movementComponent.Update();
            _rotationComponent.Update();
            _shootComponent.Update();
        }

        public void OnDisable()
        {
            _healthComponent.OnDisable();
            _shootComponent.OnDisable();
        }
    }
}
