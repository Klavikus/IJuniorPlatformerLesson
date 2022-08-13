using EntityComponents.Input;
using EntityComponents.Movement;
using UnityEngine;

namespace ScriptsForSort.States
{
    public class Moving : IState
    {
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");

        private readonly Animator _animator;
        private readonly MoveController2D _moveController;
        private readonly InputHandler _inputHandler;

        public Moving(Animator animator, MoveController2D moveController, InputHandler inputHandler)
        {
            _animator = animator;
            _moveController = moveController;
            _inputHandler = inputHandler;
        }

        public void Tick()
        {
        }

        public void Enter()
        {
            _inputHandler.InputUpdated += OnInputUpdated;
        }

        public void Exit()
        {
            _inputHandler.InputUpdated -= OnInputUpdated;
            _animator.SetFloat(MoveSpeed, 0);
        }

        private void OnInputUpdated(InputData inputData)
        {
            _animator.SetFloat(MoveSpeed, _moveController.CurrentMoveSpeed);

            // _moveController.HandleInput(inputData);
        }
    }
}