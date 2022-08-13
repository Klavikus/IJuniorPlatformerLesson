using EntityComponents.Attack;
using EntityComponents.Movement;
using ScriptsForSort;
using ScriptsForSort.States;
using UnityEngine;

public class StateSynchronizer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private MoveController2D _moveController;
    [SerializeField] private DamageHandler _damageHandler;

    private const double MaxError = 0.3f;

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        IState dying = new Dying(_animator);
        IState idle = new Idle();
        IState moving = new Moving(_animator, _moveController);
        IState jumping = new Jumping(_animator, _moveController);

        _stateMachine.AddTransition(moving, idle, () => _moveController.CurrentMoveSpeed < MaxError);
        _stateMachine.AddTransition(jumping, idle, () => _moveController.IsGrounded);

        _stateMachine.AddAnyTransition(dying, () => _damageHandler.IsDead);
        _stateMachine.AddAnyTransition(moving, () => _moveController.CurrentMoveSpeed > MaxError);
        _stateMachine.AddAnyTransition(jumping, () => _moveController.IsGrounded == false);

        _stateMachine.SetState(idle);
    }

    private void Update() => _stateMachine.Tick();
}