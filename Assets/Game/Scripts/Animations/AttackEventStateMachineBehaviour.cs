using UnityEngine;

namespace Game.Scripts.Animations
{
    public class AttackEventStateMachineBehaviour : StateMachineBehaviour
    {
        [SerializeField]
        private float _eventTime = 0.5f;

        [SerializeField]
        private bool _isEventRaised = false;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _isEventRaised = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime >= _eventTime && !_isEventRaised &&
                animator.TryGetComponent<AnimatorAttackDispatcher>(out var dispatcher))
            {
                _isEventRaised = true;
                dispatcher.InvokeAttack();
            }
        }
    }
}
