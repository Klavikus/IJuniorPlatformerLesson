namespace EntityComponents.FSM.States
{
    public interface IState
    {
        void Tick();
        void Enter();
        void Exit();
    }
}