using EntityComponents.Attack;
using EntityComponents.Control;
using EntityComponents.FSM.States;
using EntityComponents.Movement;
using UnityEngine;

namespace EntityComponents.FSM
{
    public class StateSynchronizer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private MoveController2D _moveController;
        [SerializeField] private AttackController _attackController;
        [SerializeField] private DamageHandler _damageHandler;
        [SerializeField] private InputHandler _inputHandler;

        private const double MaxError = 0.3f;

        private StateMachine _stateMachine;

        private void OnEnable()
        {
            _inputHandler.InputUpdated += _moveController.HandleInput;
            _inputHandler.InputUpdated += _attackController.HandleInput;
        }

        private void OnDisable()
        {
            _inputHandler.InputUpdated -= _moveController.HandleInput;
            _inputHandler.InputUpdated -= _attackController.HandleInput;
        }

        private void Awake()
        {
            _stateMachine = new StateMachine();

            IState idle = new Idle();
            IState dying = new Dying(_animator);
            IState moving = new Moving(_animator, _moveController);
            IState jumping = new Jumping(_animator);
            IState dashing = new Dashing(_animator);
            IState attacking = new Attacking(_animator);

            _stateMachine.AddTransition(idle, moving, () => Mathf.Abs(_moveController.CurrentMoveSpeed) > MaxError);
            _stateMachine.AddTransition(moving, idle, () => Mathf.Abs(_moveController.CurrentMoveSpeed) < MaxError);
            _stateMachine.AddTransition(jumping, idle, () => _moveController.IsGrounded);
            _stateMachine.AddTransition(dashing, idle, () => _moveController.IsDashing == false);
            _stateMachine.AddTransition(attacking, idle, () => _attackController.IsAttacking == false);

            _stateMachine.AddAnyTransition(dying, () => _damageHandler.IsDead);
            _stateMachine.AddAnyTransition(attacking, () => _attackController.IsAttacking);
            _stateMachine.AddAnyTransition(dashing, () => _moveController.IsDashing);
            _stateMachine.AddAnyTransition(jumping, () => _moveController.IsGrounded == false);

            _stateMachine.SetState(idle);
        }

        private void Update() => 
            _stateMachine.Tick();
    }
}