using System;
using EntityComponents.Attack;
using EntityComponents.Input;
using EntityComponents.Movement;
using ScriptsForSort;
using ScriptsForSort.States;
using UnityEngine;

public class StateSynchronizer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private MoveController2D _moveController;
    [SerializeField] private DamageHandler _damageHandler;
    [SerializeField] private InputHandler _inputHandler;

    private const double MaxError = 0.3f;

    private StateMachine _stateMachine;

    private void OnEnable()
    {
        _inputHandler.InputUpdated += _moveController.HandleInput;
    }

    private void OnDisable()
    {
        _inputHandler.InputUpdated -= _moveController.HandleInput;
    }

    private void Awake()
    {
        _stateMachine = new StateMachine();

        IState dying = new Dying(_animator);
        IState idle = new Idle(_animator, _moveController, _inputHandler);
        IState moving = new Moving(_animator, _moveController, _inputHandler);
        IState jumping = new Jumping(_animator, _moveController, _inputHandler);

        _stateMachine.AddTransition(moving, idle, CheckMovingBelowThreshold());
        _stateMachine.AddTransition(jumping, idle, () => _moveController.IsGrounded);
        _stateMachine.AddTransition(idle, moving, CheckMovingAboveThreshold());

        _stateMachine.AddAnyTransition(dying, () => _damageHandler.IsDead);
        _stateMachine.AddAnyTransition(jumping, () => _moveController.IsGrounded == false);

        _stateMachine.SetState(idle);
    }

    private void Update() => _stateMachine.Tick();

    private Func<bool> CheckMovingAboveThreshold() =>
        () => Mathf.Abs(_inputHandler.CurrentInputData.Horizontal) > MaxError;

    private Func<bool> CheckMovingBelowThreshold() =>
        () => Mathf.Abs(_inputHandler.CurrentInputData.Horizontal) < MaxError;
}