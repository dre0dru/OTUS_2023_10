using UnityEngine;

namespace Components
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _speed = 5.0f;

        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = _rigidbody.position + vector * _speed;
            _rigidbody.MovePosition(nextPosition);
        }
    }
}
