using Gameplay;

namespace Core.StateMachine
{
    public class InGameState: State
    {
        private readonly Triliza m_Triliza;

        public InGameState(Triliza triliza)
        {
            m_Triliza = triliza;
        }

        public override void OnEnterState()
        {
        }
        public override void OnLeaveState()
        {
        }

        public override void Tick()
        {
            m_Triliza.Tick();
        }
    }
}
