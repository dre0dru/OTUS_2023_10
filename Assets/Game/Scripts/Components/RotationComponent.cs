using System;
using Atomic.Elements;
using Atomic.Objects;
using Atomics.Extensions;
using Game.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class RotationComponent
    {
        [Get(ObjectAPI.RotationDirection)]
        [SerializeField]
        private AtomicVariable<Vector3> _rotationDirection;

        private RotationMechanics _rotationMechanics;

        public void Compose(IAtomicObject atomicObject)
        {
            var root = atomicObject.Get<Transform>(ObjectAPI.Root);;

            _rotationMechanics = new RotationMechanics(_rotationDirection, root, atomicObject.GetValue<bool>(ObjectAPI.CanMove));
        }

        public void Update()
        {
            _rotationMechanics.Update();
        }
    }
}
