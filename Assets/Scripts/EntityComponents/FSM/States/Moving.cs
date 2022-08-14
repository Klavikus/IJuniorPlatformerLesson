using EntityComponents.Movement;
using UnityEngine;

namespace EntityComponents.FSM.States
{
    public class Moving : IState
    {
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");

        private readonly Animator _animator;
        private readonly MoveController2D _moveController;

        public Moving(Animator animator, MoveController2D moveController)
        {
            _animator = animator;
            _moveController = moveController;
        }

        public void Tick()
        {
            _animator.SetFloat(MoveSpeed, _moveController.CurrentMoveSpeed);
        }

        public void Enter()
        {
        }

        public void Exit()
        {
            _animator.SetFloat(MoveSpeed, 0);
        }
    }
}