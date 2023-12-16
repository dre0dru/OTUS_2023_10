using System;
using Bullets;
using Components;
using GameInput;
using LifecycleEvents;
using UnityEngine;

namespace Character
{
    public class CharacterMoveController : MonoBehaviour, IUpdateListener
    {
        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private InputManager _inputManager;

        private MoveComponent _moveComponent;

        private void Awake()
        {
            _moveComponent = _character.GetComponent<MoveComponent>();
        }

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            MoveHorizontally();
        }

        private void MoveHorizontally()
        {
            _moveComponent
                .MoveByRigidbodyVelocity(new Vector2(_inputManager.HorizontalInput, 0) * Time.fixedDeltaTime);
        }
    }
}
