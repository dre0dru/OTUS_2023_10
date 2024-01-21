using System;
using Atomic.Objects;
using Game.Scripts.Components;
using UnityEngine;

namespace Game.Scripts.Characters
{
    [Serializable]
    public class CharacterVisual
    {
        [Section]
        [SerializeField]
        private AnimationComponent _animationComponent;

        [Section]
        [SerializeField]
        private CharacterFxComponent _characterFxComponent;

        public void Compose(IAtomicObject atomicObject)
        {
            _animationComponent.Compose(atomicObject);
            _characterFxComponent.Compose(atomicObject);
        }

        public void OnEnable()
        {
            _animationComponent.OnEnable();
            _characterFxComponent.OnEnable();
        }

        public void Update()
        {
            _animationComponent.Update();
        }

        public void OnDisable()
        {
            _animationComponent.OnDisable();
            _characterFxComponent.OnDisable();
        }
    }
}
