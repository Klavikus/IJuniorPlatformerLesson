using EntityComponents.Movement;

namespace StateMachine
{
    public class Jump : IState
    {
        private readonly MoveController2D _moveController;

        public Jump(MoveController2D moveController)
        {
            _moveController = moveController;
        }

        public void Enter(int payload)
        {
            _moveController.HandleJump();
        }

        public void Exit()
        {
        }
    }
}