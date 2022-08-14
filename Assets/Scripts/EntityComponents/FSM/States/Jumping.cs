using UnityEngine;

namespace EntityComponents.FSM.States
{
    public class Jumping : IState
    {
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");

        private readonly Animator _animator;

        public Jumping(Animator animator)
        {
            _animator = animator;
        }

        public void Tick()
        {
        }

        public void Enter()
        {
            _animator.SetBool(IsJumping, true);
        }

        public void Exit()
        {
            _animator.SetBool(IsJumping, false);
        }
    }
}