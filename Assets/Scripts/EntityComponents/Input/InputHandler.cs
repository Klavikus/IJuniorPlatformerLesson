using StateMachine;
using UnityEngine;

namespace EntityComponents.Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private SimpleStateMachine playerSimpleStateMachine;

        private const int MaxHorizontalInput = 1;

        private void Update()
        {
            if (UnityEngine.Input.GetKey(KeyCode.A))
                playerSimpleStateMachine.Move(-MaxHorizontalInput);

            if (UnityEngine.Input.GetKey(KeyCode.D))
                playerSimpleStateMachine.Move(MaxHorizontalInput);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                playerSimpleStateMachine.Jump();

            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
                playerSimpleStateMachine.Dash();

            if (UnityEngine.Input.GetMouseButtonDown(0))
                playerSimpleStateMachine.MainAttack();

            if (UnityEngine.Input.GetMouseButtonDown(1))
                playerSimpleStateMachine.SecondaryAttack();
        }
    }
}