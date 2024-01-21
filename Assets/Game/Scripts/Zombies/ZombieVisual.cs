using System;
using Atomic.Objects;
using Game.Scripts.Components;
using UnityEngine;

namespace Game.Scripts.Zombies
{
    [Serializable]
    public class ZombieVisual
    {
        [Section]
        [SerializeField]
        private AnimationComponent _animationComponent;

        [Section]
        [SerializeField]
        private ZombieFxComponent _zombieFxComponent;

        public void Compose(IAtomicObject atomicObject)
        {
            _animationComponent.Compose(atomicObject);
            _zombieFxComponent.Compose(atomicObject);
        }

        public void OnEnable()
        {
            _animationComponent.OnEnable();
            _zombieFxComponent.OnEnable();
        }

        public void Update()
        {
            _animationComponent.Update();
        }

        public void OnDisable()
        {
            _animationComponent.OnDisable();
            _zombieFxComponent.OnDisable();
        }
    }
}
