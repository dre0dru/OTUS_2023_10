using UnityEngine;

namespace GameInput
{
    public sealed class InputManager : MonoBehaviour
    {
        public bool IsFireInputPressed { get; private set; }
        public float HorizontalInput { get; private set; }

        private void Update()
        {
            IsFireInputPressed = Input.GetKeyDown(KeyCode.Space);

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
