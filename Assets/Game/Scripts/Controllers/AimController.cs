using Atomic.Objects;
using Atomics.Extensions;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class AimController : MonoBehaviour
    {
        [SerializeField]
        private AtomicObjectComponent _atomicObjectComponent;

        [SerializeField]
        private Camera _camera;

        private void Update()
        {
            if (TryGetPointerPositionWorld(out var pointerPositionWorld))
            {
                var root = _atomicObjectComponent.Get<Transform>(ObjectAPI.Root);
                var aimDirection = (pointerPositionWorld - root.position);
                aimDirection.y = 0;
                _atomicObjectComponent
                    .GetVariable<Vector3>(ObjectAPI.RotationDirection).Value = aimDirection.normalized;
            }
        }

        private bool TryGetPointerPositionWorld(out Vector3 pointerPositionWorld)
        {
            var mousePosition = Input.mousePosition;
            var ray = _camera.ScreenPointToRay(mousePosition);
            var groundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));

            if (groundPlane.Raycast(ray, out var distance))
            {
                pointerPositionWorld = ray.GetPoint(distance);
                return true;
            }

            pointerPositionWorld = Vector3.zero;
            return false;
        }
    }
}
