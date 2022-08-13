using System;
using EntityComponents.Input;
using EntityComponents.Movement;
using UnityEngine;

namespace ScriptsForSort.States
{
    public class Jumping : IState
    {
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");

        private readonly Animator _animator;
        private readonly MoveController2D _moveController;
        private readonly InputHandler _inputHandler;

        public Jumping(Animator animator, MoveController2D moveController, InputHandler inputHandler)
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
            _animator.SetBool(IsJumping, true);
        }

        public void Exit()
        {
            _inputHandler.InputUpdated -= OnInputUpdated;
            _animator.SetBool(IsJumping, false);
        }

        private void OnInputUpdated(InputData inputData)
        {
            // _moveController.HandleInput(inputData);
        }
    }
}