using Atomic.Objects;
using Atomics.Extensions;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField]
        private AtomicObjectComponent _atomicObjectComponent;

        private void Update()
        {
            var movementDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.A))
            {
                movementDirection += Vector3.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                movementDirection += Vector3.right;
            }

            if (Input.GetKey(KeyCode.W))
            {
                movementDirection += Vector3.forward;
            }

            if (Input.GetKey(KeyCode.S))
            {
                movementDirection += Vector3.back;
            }

            _atomicObjectComponent.GetVariable<Vector3>(ObjectAPI.MovementDirection).Value = movementDirection;
        }
    }
}
