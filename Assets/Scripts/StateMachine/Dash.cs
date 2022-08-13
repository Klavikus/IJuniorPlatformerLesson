using EntityComponents.Movement;

namespace StateMachine
{
    public class Dash : IState
    {
        private readonly MoveController2D _moveController;

        public Dash(MoveController2D moveController)
        {
            _moveController = moveController;
        }

        public void Enter(int payload)
        {
            _moveController.HandleDash();
        }

        public void Exit()
        {
        }
    }
}