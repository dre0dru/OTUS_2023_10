using System;
using Atomic.Elements;
using Atomic.Objects;
using Atomics.Extensions;
using Game.Scripts.Animations;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class AnimationComponent
    {
        private const int Idle = 0;
        private const int Move = 1;
        private const int Death = 2;

        private static readonly int MainState = Animator.StringToHash("MainState");
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private AnimatorAttackDispatcher _animatorAttackDispatcher;

        private IAtomicValue<Vector3> _moveDirection;
        private IAtomicValue<bool> _isDead;
        private IAtomicObservable _attackRequest;

        public void Compose(IAtomicObject atomicObject)
        {
            _moveDirection = atomicObject.GetValue<Vector3>(ObjectAPI.MovementDirection);
            _isDead = atomicObject.GetValue<bool>(ObjectAPI.IsDead);
            _attackRequest = atomicObject.GetObservable(ObjectAPI.AttackRequest);
            _animatorAttackDispatcher.Compose(atomicObject);
        }

        public void OnEnable()
        {
            _attackRequest.Subscribe(OnAttackRequested);
        }

        public void Update()
        {
            _animator.SetInteger(MainState, GetAnimationValue());
        }

        public void OnDisable()
        {
            _attackRequest.Unsubscribe(OnAttackRequested);
        }

        private void OnAttackRequested()
        {
            _animator.SetTrigger(AttackTrigger);
        }

        private int GetAnimationValue()
        {
            if (_isDead.Value)
            {
                return Death;
            }

            if (_moveDirection.Value != Vector3.zero)
            {
                return Move;
            }

            return Idle;
        }
    }
}
