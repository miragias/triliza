namespace Gameplay.GameplayStates
{
    public abstract class GameplayState
    {
        public abstract void OnEnterState();
        public abstract void Update();
        public abstract void OnLeaveState();
    }
}
