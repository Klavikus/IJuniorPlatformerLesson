namespace StateMachine
{
    public interface IState
    {
        void Enter(int payload);
        void Exit();
    }
}