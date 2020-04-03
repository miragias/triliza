namespace Gameplay.GameplayStates
{
    public class FinishedGameStateWithTie : GameplayState
    {
        private readonly Triliza m_Triliza;
        public FinishedGameStateWithTie(Triliza triliza)
        {
            m_Triliza = triliza;
        }

        public override void OnEnterState()
        {
            m_Triliza.SwitchInteractOff();
            GameManager.Instance.InfoText.text = "TIE";
        }

        public override void OnLeaveState()
        {
        }

        public override void Update()
        {
        }
    }
}
