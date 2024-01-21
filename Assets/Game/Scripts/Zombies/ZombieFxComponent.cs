using System;
using Atomic.Objects;
using Atomics.Extensions;
using Game.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.Zombies
{
    [Serializable]
    public class ZombieFxComponent
    {
        [SerializeField]
        private ParticleSystem _hitFx;

        private PlayFxByEventMechanics<int> _playHitFx;

        public void Compose(IAtomicObject atomicObject)
        {
            _playHitFx = new PlayFxByEventMechanics<int>(_hitFx, atomicObject.GetObservable<int>(ObjectAPI.TakeDamage));
        }

        public void OnEnable()
        {
            _playHitFx.OnEnable();
        }

        public void OnDisable()
        {
            _playHitFx.OnDisable();
        }
    }
}
