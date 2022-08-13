using System;
using UnityEngine;

namespace EntityComponents.Input
{
    public class InputHandler : MonoBehaviour
    {
        public InputData CurrentInputData { get; private set; }
        public event Action<InputData> InputUpdated;

        private void Update()
        {
            float horizontal = 0;
            bool isJumping = false;
            bool isDashing = false;

            if (UnityEngine.Input.GetKey(KeyCode.A))
                horizontal = -1;

            if (UnityEngine.Input.GetKey(KeyCode.D))
                horizontal = 1;

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                isJumping = true;

            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
                isDashing = true;

            CurrentInputData = new InputData(horizontal, isDashing, isJumping);

            InputUpdated?.Invoke(CurrentInputData);
        }
    }

    public struct InputData
    {
        public float Horizontal;
        public bool IsDashed;
        public bool IsJumping;

        public InputData(float horizontal, bool isDashed, bool isJumping)
        {
            Horizontal = horizontal;
            IsDashed = isDashed;
            IsJumping = isJumping;
        }
    }
}