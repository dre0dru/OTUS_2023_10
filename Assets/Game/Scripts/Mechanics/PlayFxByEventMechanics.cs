using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class PlayFxByEventMechanics
    {
        private readonly ParticleSystem _fx;
        private readonly IAtomicObservable _event;

        public PlayFxByEventMechanics(ParticleSystem fx, IAtomicObservable @event)
        {
            _fx = fx;
            _event = @event;
        }

        public void OnEnable()
        {
            _event.Subscribe(PlayFx);
        }

        public void OnDisable()
        {
            _event.Unsubscribe(PlayFx);
        }

        private void PlayFx()
        {
            _fx.Play();
        }
    }

    public class PlayFxByEventMechanics<T>
    {
        private readonly ParticleSystem _fx;
        private readonly IAtomicObservable<T> _event;

        public PlayFxByEventMechanics(ParticleSystem fx, IAtomicObservable<T> @event)
        {
            _fx = fx;
            _event = @event;
        }

        public void OnEnable()
        {
            _event.Subscribe(PlayFx);
        }

        public void OnDisable()
        {
            _event.Unsubscribe(PlayFx);
        }

        private void PlayFx(T _)
        {
            _fx.Play();
        }
    }
}
