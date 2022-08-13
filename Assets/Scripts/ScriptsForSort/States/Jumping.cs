using EntityComponents.Movement;
using UnityEngine;

namespace ScriptsForSort.States
{
    public class Jumping : IState
    {
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");

        private readonly Animator _animator;
        private readonly MoveController2D _moveController;

        public Jumping(Animator animator, MoveController2D moveController)
        {
            _animator = animator;
            _moveController = moveController;
        }


        public void Tick()
        {
        }

        public void OnEnter()
        {
            _animator.SetBool(IsJumping, true);
        }

        public void OnExit()
        {
        }
    }
}