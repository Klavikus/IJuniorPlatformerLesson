using StateMachine;
using UnityEngine;

namespace EntityComponents.Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private StateMachine.StateMachine _playerStateMachine;

        private const int MaxHorizontalInput = 1;

        private void Update()
        {
            if (UnityEngine.Input.GetKey(KeyCode.A))
                _playerStateMachine.SwitchState<Move>(-MaxHorizontalInput);

            if (UnityEngine.Input.GetKey(KeyCode.D))
                _playerStateMachine.SwitchState<Move>(MaxHorizontalInput);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                _playerStateMachine.SwitchState<Jump>(0);

            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
                _playerStateMachine.SwitchState<Dash>(0);

            if (UnityEngine.Input.GetMouseButtonDown(0))
                _playerStateMachine.SwitchState<StateMachine.Attack>(0);

            if (UnityEngine.Input.GetMouseButtonDown(1))
                _playerStateMachine.SwitchState<StateMachine.Attack>(1);
        }
    }
}