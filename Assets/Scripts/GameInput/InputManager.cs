using LifecycleEvents;
using UnityEngine;

namespace GameInput
{
    public sealed class InputManager : MonoBehaviour, IUpdateListener
    {
        public bool IsShootInputPressed { get; private set; }
        public float HorizontalInput { get; private set; }

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            IsShootInputPressed = Input.GetKeyDown(KeyCode.Space);

            HorizontalInput = 0;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalInput = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalInput = 1;
            }
        }
    }
}
