using System;
using Atomic.Objects;
using Atomics.Extensions;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField]
        private AtomicObjectComponent _atomicObjectComponent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                _atomicObjectComponent.GetAction(ObjectAPI.AttackRequest).Invoke();
            }
        }
    }
}
