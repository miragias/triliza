namespace Gameplay.GameplayStates
{
    //NOTE(JohnMir): No real reason for this state existence but if we add fancy animations etc so the state stays more then it's handy. For now it's instant.
    public class AiChooseState : GameplayState
    {
        private readonly Triliza m_Triliza;

        public AiChooseState(Triliza triliza)
        {
            m_Triliza = triliza;
        }

        public override void OnEnterState()
        {
            m_Triliza.PickAiMove();
            if(!m_Triliza.GameHasEnded)
            {
                m_Triliza.SwitchToGameplayState(new PlayerChooseState(m_Triliza));
            }
        }

        public override void OnLeaveState()
        {
            m_Triliza.SwitchInteractOff();
        }

        public override void Update()
        {
        }
    }
}
