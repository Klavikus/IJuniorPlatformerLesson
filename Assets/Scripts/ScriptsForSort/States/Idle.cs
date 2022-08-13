using EntityComponents.Input;
using EntityComponents.Movement;
using UnityEngine;

namespace ScriptsForSort.States
{
    public class Idle : IState
    {
        private readonly Animator _animator;
        private readonly MoveController2D _moveController;
        private readonly InputHandler _inputHandler;

        public Idle(Animator animator, MoveController2D moveController, InputHandler inputHandler)
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
        }

        private void OnInputUpdated(InputData inputData)
        {
            // _moveController.HandleInput(inputData);
        }
    }
}