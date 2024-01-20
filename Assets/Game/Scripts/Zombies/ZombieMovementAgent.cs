using System;
using Atomic.Elements;
using Atomic.Objects;
using Atomics.Extensions;
using Game.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.Zombies
{
    [Serializable]
    public class ZombieMovementAgent
    {
        [SerializeField]
        private AtomicValue<float> _stoppingDistance = new(1.0f);

        private MoveTowardsTargetMechanics _moveTowardsTargetMechanics;

        public void Compose(IAtomicObject atomicObject)
        {
            _moveTowardsTargetMechanics = new MoveTowardsTargetMechanics(atomicObject.Get<Transform>(ObjectAPI.Root),
                atomicObject.GetVariable<AtomicObjectComponent>(ObjectAPI.Target).Value.transform.AsValue(),
                _stoppingDistance, atomicObject.GetAction(ObjectAPI.AttackRequest),
                atomicObject.GetVariable<Vector3>(ObjectAPI.MovementDirection));
        }

        public void Update()
        {
            _moveTowardsTargetMechanics.Update();
        }
    }
}
