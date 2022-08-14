using System;
using System.Collections.Generic;
using EntityComponents.FSM.States;

namespace EntityComponents.FSM
{
    public class StateMachine
    {
        private readonly List<Transition> _emptyTransitions = new List<Transition>(0);
        private readonly Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private readonly List<Transition> _anyTransitions = new List<Transition>();

        private List<Transition> _currentTransitions = new List<Transition>();
        private IState _currentState;

        public void Tick()
        {
            Transition transition = GetTransition();
            
            if (transition != null)
                SetState(transition.To);

            _currentState?.Tick();
        }

        public void SetState(IState state)
        {
            if (state == _currentState)
                return;

            _currentState?.Exit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);

            _currentTransitions ??= _emptyTransitions;

            _currentState.Enter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out List<Transition> transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate) => 
            _anyTransitions.Add(new Transition(state, predicate));

        private Transition GetTransition()
        {
            foreach (Transition transition in _anyTransitions)
                if (transition.Condition())
                    return transition;

            foreach (Transition transition in _currentTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }
    }
}