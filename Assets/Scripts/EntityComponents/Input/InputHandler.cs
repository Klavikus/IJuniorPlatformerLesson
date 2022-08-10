using EntityComponents.Attack;
using EntityComponents.Movement;
using UnityEngine;

namespace EntityComponents.Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private MoveController2D _moveController;
        [SerializeField] private AttackController _attackController;

        private const int MaxHorizontalInput = 1;

        private void Update()
        {
            if (UnityEngine.Input.GetKey(KeyCode.A))
                _moveController.HandleMove(-MaxHorizontalInput);

            if (UnityEngine.Input.GetKey(KeyCode.D))
                _moveController.HandleMove(MaxHorizontalInput);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                _moveController.HandleJump();

            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
                _moveController.HandleDash();

            if (UnityEngine.Input.GetMouseButtonDown(0))
                _attackController.TryAttack();

            if (UnityEngine.Input.GetMouseButtonDown(1))
                _attackController.TrySecondaryAttack();
        }
    }
}