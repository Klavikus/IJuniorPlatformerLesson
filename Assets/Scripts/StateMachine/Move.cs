using EntityComponents.Movement;

namespace StateMachine
{
    public class Move : IState
    {
        private readonly MoveController2D _moveController;

        public Move(MoveController2D moveController)
        {
            _moveController = moveController;
        }

        public void Enter(int payload)
        {
            _moveController.HandleMove(payload);
        }

        public void Exit()
        {
        }
    }
}