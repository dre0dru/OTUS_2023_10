using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class DestroyMechanics
    {
        private readonly IAtomicObservable _deathEvent;
        private readonly GameObject _gameObject;

        public DestroyMechanics(IAtomicObservable deathEvent, GameObject gameObject)
        {
            _deathEvent = deathEvent;
            _gameObject = gameObject;
        }

        public void OnEnable()
        {
            _deathEvent.Subscribe(OnDeath);
        }

        public void OnDisable()
        {
            _deathEvent.Unsubscribe(OnDeath);
        }

        private void OnDeath()
        {
            Object.Destroy(_gameObject);
        }
    }
}
