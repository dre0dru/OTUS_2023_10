using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Components;
using Game.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.Bullets
{
    [Serializable]
    public class BulletCore
    {
        [Get(ObjectAPI.Root)]
        [SerializeField]
        private Transform _root;

        [Section]
        [SerializeField]
        private MovementComponent _movementComponent;

        [SerializeField]
        private AtomicValue<int> _damage;

        [SerializeField]
        private AtomicEvent _deathEvent;

        [SerializeField]
        private AtomicVariable<float> _lifeTime = new(10);

        private BulletCollisionMechanics _collisionMechanics;
        private DestroyMechanics _destroyMechanics;
        private LifeTimeMechanics _lifeTimeMechanics;

        public void Compose(IAtomicObject atomicObject)
        {
            _movementComponent.Compose(atomicObject);
            _collisionMechanics = new BulletCollisionMechanics(_damage, _deathEvent);
            _destroyMechanics = new DestroyMechanics(_deathEvent, _root.gameObject);
            _lifeTimeMechanics = new LifeTimeMechanics(_lifeTime, _deathEvent);
        }

        public void OnEnable()
        {
            _destroyMechanics.OnEnable();
        }

        public void Update()
        {
            _movementComponent.Update();
            _lifeTimeMechanics.Update();
        }

        public void OnDisable()
        {
            _destroyMechanics.OnDisable();
        }

        public void OnTriggerEnter(Collider collider)
        {
            _collisionMechanics.OnTriggerEnter(collider);
        }
    }
}
