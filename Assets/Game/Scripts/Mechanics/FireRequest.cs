using System;
using Atomic.Elements;
using Sirenix.OdinInspector;

namespace Game.Scripts.Mechanics
{
    [Serializable]
    public class FireRequest : IAtomicEvent
    {
        private event Action _onEvent;
        private IAtomicValue<bool> _canAttack;

        public void Compose(IAtomicValue<bool> canShoot)
        {
            _canAttack = canShoot;
        }

        [Button]
        public void Invoke()
        {
            if (!_canAttack.Value)
            {
                return;
            }

            _onEvent?.Invoke();
        }

        public void Subscribe(Action action)
        {
            _onEvent += action;
        }

        public void Unsubscribe(Action action)
        {
            _onEvent -= action;
        }
    }
}
