using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class MoveTowardsTargetMechanics
    {
        private readonly Transform _root;
        private readonly IAtomicValue<AtomicObjectComponent> _target;
        private readonly IAtomicValue<float> _stoppingDistance;
        private readonly IAtomicAction _targetReachedEvent;
        private readonly IAtomicVariable<Vector3> _movementDirection;

        public MoveTowardsTargetMechanics(Transform root, IAtomicValue<AtomicObjectComponent> target,
            IAtomicValue<float> stoppingDistance,
            IAtomicAction targetReachedEvent, IAtomicVariable<Vector3> movementDirection)
        {
            _root = root;
            _target = target;
            _stoppingDistance = stoppingDistance;
            _targetReachedEvent = targetReachedEvent;
            _movementDirection = movementDirection;
        }

        public void Update()
        {
            if (_target.Value == null)
            {
                return;
            }

            var direction = _target.Value.transform.position - _root.position;
            var distance = direction.magnitude;

            if (distance <= _stoppingDistance.Value)
            {
                _movementDirection.Value = Vector3.zero;
                _targetReachedEvent.Invoke();
            }
            else
            {
                _movementDirection.Value = direction.normalized;
            }
        }
    }
}
