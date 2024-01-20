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

        public void Compose(IAtomicObject atomicObject)
        {
            _animationComponent.Compose(atomicObject);
        }

        public void OnEnable()
        {
            _animationComponent.OnEnable();
        }

        public void Update()
        {
            _animationComponent.Update();
        }

        public void OnDisable()
        {
            _animationComponent.OnDisable();
        }
    }
}
