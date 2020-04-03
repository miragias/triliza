namespace Core.StateMachine
{
    public abstract class State
    {
        public abstract void OnEnterState();
        public abstract void Tick();
        public abstract void OnLeaveState();
    }
}
