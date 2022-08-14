using UnityEngine;

namespace EntityComponents.Control
{
    public class PlayerInput : InputHandler
    {
        public void Update()
        {
            float horizontal = 0;
            bool isJumping = false;
            bool isDashing = false;
            bool isMainAttack = false;
            bool isSecondaryAttack = false;

            if (Input.GetKey(KeyCode.A))
                horizontal = -1;

            if (Input.GetKey(KeyCode.D))
                horizontal = 1;

            if (Input.GetKeyDown(KeyCode.Space))
                isJumping = true;

            if (Input.GetKeyDown(KeyCode.LeftShift))
                isDashing = true;

            if (Input.GetMouseButtonDown(0))
                isMainAttack = true;

            if (Input.GetMouseButtonDown(1))
                isSecondaryAttack = true;

            InputUpdated?.Invoke(new InputData(horizontal, isDashing, isJumping, isMainAttack, isSecondaryAttack));
        }
    }
}