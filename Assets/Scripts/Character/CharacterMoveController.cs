using Components;
using GameInput;
using LifecycleEvents;
using UnityEngine;

namespace Character
{
    public sealed class CharacterMoveController : IUpdateListener
    {
        private readonly InputManager _inputManager;
        private readonly MoveComponent _moveComponent;

        public CharacterMoveController(InputManager inputManager, GameObject character)
        {
            _inputManager = inputManager;
            _moveComponent = character.GetComponent<MoveComponent>();
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
