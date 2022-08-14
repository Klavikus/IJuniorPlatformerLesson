using UnityEngine;

namespace EntityComponents.FSM.States
{
    public class Dashing : IState
    {
        private static readonly int IsDashing = Animator.StringToHash("IsDashing");

        private readonly Animator _animator;

        public Dashing(Animator animator)
        {
            _animator = animator;
        }

        public void Tick()
        {
        }

        public void Enter()
        {
            _animator.SetBool(IsDashing, true);
        }

        public void Exit()
        {
            _animator.SetBool(IsDashing, false);
        }
    }
}