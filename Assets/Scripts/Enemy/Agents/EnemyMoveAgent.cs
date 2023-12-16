using Components;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        private const float StoppingDistance = 0.25f;

        [SerializeField]
        private MoveComponent _moveComponent;

        private Vector2 _destination;

        private bool _isDestinationReached;

        public bool IsDestinationReached => _isDestinationReached;

        public void ProcessMovement()
        {
            if (_isDestinationReached)
            {
                return;
            }

            MoveToDestinationIfNotReached();
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isDestinationReached = false;
        }

        private void MoveToDestinationIfNotReached()
        {
            var vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= StoppingDistance)
            {
                _isDestinationReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}
