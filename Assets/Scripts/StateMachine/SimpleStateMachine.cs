using System.Collections.Generic;
using System.Linq;
using EntityComponents.Attack;
using EntityComponents.Movement;
using UnityEngine;

namespace StateMachine
{
    public class SimpleStateMachine : MonoBehaviour
    {
        [SerializeField] private MoveController2D _moveController;
        [SerializeField] private AttackController _attackController;

        private List<IState> _states;

        public IState PreviousState { get; private set; }
        public IState CurrentState { get; private set; }

        private void Start()
        {
            _states = new List<IState>
            {
                new Idle(),
                new Move(_moveController),
                new Jump(_moveController),
                new Dash(_moveController),
                new Attack(_attackController),
            };

            SwitchState<Idle>(0);
        }

        public void Move(int horizontalDirection) => SwitchState<Move>(horizontalDirection);
        
        public void Jump() => SwitchState<Jump>(0);
        
        public void Dash() => SwitchState<Dash>(0);

        public void MainAttack()=> SwitchState<Attack>(0);
        public void SecondaryAttack()=> SwitchState<Attack>(1);

        private void SwitchState<T>(int payload) where T : IState
        {
            PreviousState = CurrentState;
            CurrentState?.Exit();
            CurrentState = _states.FirstOrDefault(state => state is T);
            CurrentState?.Enter(payload);
        }
    }
}