using System;
using Atomic.Elements;
using Atomic.Objects;
using Atomics.Extensions;
using Game.Scripts.Mechanics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class AttackComponent
    {
        [Get(ObjectAPI.AttackRequest)]
        [SerializeField]
        private FireRequest _fireRequest;

        [ReadOnly]
        [Get(ObjectAPI.AttackEvent)]
        [SerializeField]
        private AtomicEvent _fireEvent;

        [Get(ObjectAPI.CanShoot)]
        [SerializeField]
        private AndExpression _canAttack;

        [SerializeField]
        private AtomicValue<float> _attackCooldown = new(1);

        [SerializeField]
        private AtomicVariable<float> _attackTimer;

        [SerializeField]
        private AtomicValue<int> _damage = new(1);

        private CooldownMechanics _cooldownMechanics;
        private AttackMechanics _attackMechanics;

        public void Compose(IAtomicObject atomicObject)
        {
            _attackMechanics = new AttackMechanics(_fireEvent, _damage, atomicObject.GetValue<AtomicObjectComponent>(ObjectAPI.Target));
            _cooldownMechanics = new CooldownMechanics(_attackTimer, _fireEvent);

            _fireRequest.Compose(_canAttack);

            _canAttack.AddMember(_attackTimer.AsFunction(timer => timer >= _attackCooldown));
            _canAttack.AddMember(atomicObject.GetValue<bool>(ObjectAPI.IsDead).Negate());
        }

        public void OnEnable()
        {
            _cooldownMechanics.OnEnable();
            _attackMechanics.OnEnable();
        }

        public void Update()
        {
            _cooldownMechanics.Update();
        }

        public void OnDisable()
        {
            _cooldownMechanics.OnDisable();
            _attackMechanics.OnDisable();
        }
    }
}
