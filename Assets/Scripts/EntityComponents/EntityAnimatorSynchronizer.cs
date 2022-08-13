using EntityComponents.Attack;
using EntityComponents.Movement;
using UnityEngine;

namespace EntityComponents
{
    public class EntityAnimatorSynchronizer : MonoBehaviour
    {
        [SerializeField] private MoveController2D _moveController;
        [SerializeField] private AttackController _attackController;
        [SerializeField] private DamageHandler _damageHandler;
        [SerializeField] private Animator _animator;

        private const float MaxError = 0.3f;

        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
        private static readonly int IsDashing = Animator.StringToHash("IsDashing");
        private static readonly int MainAttack = Animator.StringToHash("MainAttack");
        private static readonly int Died = Animator.StringToHash("Died");

        private void OnEnable()
        {
            _attackController.AttackInitiated += OnAttackInitiated;
            _damageHandler.Died += OnDied;
        }

        private void OnDisable()
        {
            _attackController.AttackInitiated -= OnAttackInitiated;
            _damageHandler.Died -= OnDied;
        }

        private void Update()
        {
            ResetStates();
            SynchronizeMovement();
        }

        private void ResetStates()
        {
            _animator.SetBool(IsDashing, false);
            _animator.SetBool(IsJumping, false);
            _animator.SetFloat(MoveSpeed, 0);
        }

        private void SynchronizeMovement()
        {
            if (_moveController.IsDashing)
            {
                _animator.SetBool(IsDashing, true);
                return;
            }

            if (_moveController.IsGrounded && !_moveController.IsDashing && _moveController.CurrentMoveSpeed > MaxError)
            {
                _animator.SetFloat(MoveSpeed, _moveController.CurrentMoveSpeed);
                return;
            }

            if (!_moveController.IsGrounded)
            {
                _animator.SetBool(IsJumping, true);
            }
        }

        private void OnAttackInitiated() => _animator.SetTrigger(MainAttack);

        private void OnDied()
        {
            _animator.SetBool(Died, true);
            enabled = false;
        }
    }
}