using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class RotationMechanics
    {
        private readonly IAtomicValue<Vector3> _direction;
        private readonly Transform _root;
        private readonly IAtomicValue<bool> _canMove;

        public RotationMechanics(IAtomicValue<Vector3> direction, Transform root, IAtomicValue<bool> canMove)
        {
            _direction = direction;
            _root = root;
            _canMove = canMove;
        }

        public void Update()
        {
            if (!_canMove.Value)
            {
                return;
            }

            if (_direction.Value == Vector3.zero)
            {
                return;
            }

            var rotation = Quaternion.LookRotation(_direction.Value);
            _root.rotation = Quaternion.Lerp(_root.rotation, rotation, Time.deltaTime * 50f);
        }
    }
}
