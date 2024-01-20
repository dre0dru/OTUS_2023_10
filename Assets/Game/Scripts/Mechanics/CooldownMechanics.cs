using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class CooldownMechanics
    {
        private readonly IAtomicVariable<float> _timer;
        private readonly IAtomicEvent _fireEvent;

        public CooldownMechanics(IAtomicVariable<float> timer, IAtomicEvent fireEvent)
        {
            _timer = timer;
            _fireEvent = fireEvent;
        }

        public void OnEnable()
        {
            _fireEvent.Subscribe(OnFire);
        }

        public void Update()
        {
            _timer.Value += Time.deltaTime;
        }

        public void OnDisable()
        {
            _fireEvent.Unsubscribe(OnFire);
        }

        private void OnFire()
        {
            _timer.Value = 0.0f;
        }
    }
}
