using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Characters;
using Game.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class MovementComponent
    {
        [Get(ObjectAPI.MovementDirection)]
        [SerializeField]
        private AtomicVariable<Vector3> _movementDirection;

        [SerializeField]
        private AtomicValue<float> _movementSpeed;

        [Get(ObjectAPI.CanMove)]
        [SerializeField]
        private AndExpression _canMove;

        private MovementMechanics _movementMechanics;

        public void Compose(IAtomicObject atomicObject)
        {
            var root = atomicObject.Get<Transform>(ObjectAPI.Root);
            _canMove.AddMember(new AtomicFunction<bool>(() => true));

            _movementMechanics = new MovementMechanics(_movementSpeed, _movementDirection,
                root, _canMove);
        }

        public void Update()
        {
            _movementMechanics.Update();
        }
    }
}
