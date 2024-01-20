using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class LifeTimeMechanics
    {
        private readonly IAtomicVariable<float> _lifeTime;
        private readonly IAtomicAction _deathEvent;

        public LifeTimeMechanics(IAtomicVariable<float> lifeTime, IAtomicAction deathEvent)
        {
            _lifeTime = lifeTime;
            _deathEvent = deathEvent;
        }

        public void Update()
        {
            _lifeTime.Value -= Time.deltaTime;

            if (_lifeTime.Value <= 0)
            {
                _deathEvent.Invoke();
            }
        }
    }
}
