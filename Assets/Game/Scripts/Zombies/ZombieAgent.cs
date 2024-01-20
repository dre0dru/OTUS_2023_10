using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Zombies
{
    [Serializable]
    public class ZombieAgent
    {
        [Get(ObjectAPI.Target)]
        [SerializeField]
        private AtomicVariable<AtomicObjectComponent> _target;

        [Section]
        [SerializeField]
        private ZombieMovementAgent _zombieMovementAgent;

        public void Compose(IAtomicObject atomicObject)
        {
            _zombieMovementAgent.Compose(atomicObject);
        }

        public void Update()
        {
            _zombieMovementAgent.Update();
        }
    }
}
