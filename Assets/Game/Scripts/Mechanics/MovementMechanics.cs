using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class MovementMechanics
    {
        private readonly IAtomicValue<float> _speed;
        private readonly IAtomicValue<Vector3> _direction;
        private readonly Transform _root;
        private readonly IAtomicValue<bool> _canMove;

        public MovementMechanics(IAtomicValue<float> speed, IAtomicValue<Vector3> direction, Transform root, IAtomicValue<bool> canMove)
        {
            _speed = speed;
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

            _root.position += _direction.Value * _speed.Value * Time.deltaTime;
        }
    }
}
