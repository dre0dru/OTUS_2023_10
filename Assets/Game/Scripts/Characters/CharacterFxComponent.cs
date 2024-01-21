using System;
using Atomic.Objects;
using Atomics.Extensions;
using Game.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.Characters
{
    [Serializable]
    public class CharacterFxComponent
    {
        [SerializeField]
        private ParticleSystem _shootFx;

        [SerializeField]
        private ParticleSystem _hitFx;

        private PlayFxByEventMechanics _playShootFx;
        private PlayFxByEventMechanics<int> _playHitFx;

        public void Compose(IAtomicObject atomicObject)
        {
            _playShootFx = new PlayFxByEventMechanics(_shootFx, atomicObject.GetObservable(ObjectAPI.AttackEvent));
            _playHitFx = new PlayFxByEventMechanics<int>(_hitFx, atomicObject.GetObservable<int>(ObjectAPI.TakeDamage));
        }

        public void OnEnable()
        {
            _playShootFx.OnEnable();
            _playHitFx.OnEnable();
        }

        public void OnDisable()
        {
            _playShootFx.OnDisable();
            _playHitFx.OnDisable();
        }
    }
}
