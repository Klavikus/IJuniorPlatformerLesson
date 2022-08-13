using UnityEngine;

namespace ScriptsForSort.States
{
    public class Dying : IState
    {
        private static readonly int DyingHash = Animator.StringToHash("Dying");

        private readonly Animator _animator;

        public Dying(Animator animator)
        {
            _animator = animator;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _animator.SetBool(DyingHash, true);
        }

        public void OnExit()
        {
            _animator.SetBool(DyingHash, false);
        }
    }
}