using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Zombies
{
    [Is(TraitsAPI.Zombie)]
    public class Zombie : AtomicObjectComponent
    {
        [Section]
        [SerializeField]
        private ZombieCore _zombieCore;

        [Section]
        [SerializeField]
        private ZombieVisual _zombieVisual;

        [Section]
        [SerializeField]
        private ZombieAgent _zombieAgent;

        private void Awake()
        {
            Compose();
        }

        public override void Compose()
        {
            base.Compose();

            _zombieAgent.Compose(this);
            _zombieCore.Compose(this);
            _zombieVisual.Compose(this);
        }

        private void OnEnable()
        {
            _zombieCore.OnEnable();
            _zombieVisual.OnEnable();
        }

        private void Update()
        {
            _zombieCore.Update();
            _zombieVisual.Update();
            _zombieAgent.Update();
        }

        private void OnDisable()
        {
            _zombieCore.OnDisable();
            _zombieVisual.OnDisable();
        }
    }
}
