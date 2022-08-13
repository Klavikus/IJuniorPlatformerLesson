namespace ScriptsForSort
{
    public interface IState
    {
        void Tick();
        void Enter();
        void Exit();
    }
}