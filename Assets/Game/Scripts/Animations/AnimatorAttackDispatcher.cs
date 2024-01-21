using Atomic.Objects;
using Atomics.Extensions;
using UnityEngine;

namespace Game.Scripts.Animations
{
    public class AnimatorAttackDispatcher : MonoBehaviour
    {
        private IAtomicObject _atomicObject;

        public void Compose(IAtomicObject atomicObject)
        {
            _atomicObject = atomicObject;
        }

        public void InvokeAttack()
        {
            _atomicObject.GetAction(ObjectAPI.AttackEvent).Invoke();
        }
    }
}
