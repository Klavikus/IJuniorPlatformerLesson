using UnityEngine;

namespace EntityComponents.FSM.States
{
    public class Attacking : IState
    {
        private readonly Animator _animator;

        private static readonly int MainAttack = Animator.StringToHash("MainAttack");
    
        public Attacking(Animator animator)
        {
            _animator = animator;
        }

        public void Tick()
        {
        }

        public void Enter()
        {
            _animator.SetTrigger(MainAttack);
        }

        public void Exit()
        {
        }
    }
}