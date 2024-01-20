using Atomic.Objects;
using Atomics.Extensions;
using UnityEngine;

namespace Game.Scripts.Components
{
    public class AnimatorDispatcher : MonoBehaviour
    {
        private IAtomicObject _atomicObject;

        public void Compose(IAtomicObject atomicObject)
        {
            _atomicObject = atomicObject;
        }

        public void ReceiveEvent(string value)
        {
            if (value == "attack")
            {
                _atomicObject.GetAction(ObjectAPI.AttackEvent).Invoke();
            }
        }

        public void ReceiveString(string value)
        {
            ReceiveEvent(value);
        }
    }
}
