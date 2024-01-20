using System;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Characters
{
    [Is(TraitsAPI.Player)]
    public class Character : AtomicObjectComponent
    {
        [Section]
        [SerializeField]
        private CharacterCore _characterCore;

        [Section]
        [SerializeField]
        private CharacterVisual _characterVisual;

        private void Awake()
        {
            Compose();
        }

        public override void Compose()
        {
            base.Compose();

            _characterCore.Compose(this);
            _characterVisual.Compose(this);
        }

        private void OnEnable()
        {
            _characterCore.OnEnable();
            _characterVisual.OnEnable();
        }

        private void Update()
        {
            _characterCore.Update();
            _characterVisual.Update();
        }

        private void OnDisable()
        {
            _characterCore.OnDisable();
            _characterVisual.OnDisable();
        }
    }
}
