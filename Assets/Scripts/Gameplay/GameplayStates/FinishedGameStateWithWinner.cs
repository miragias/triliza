namespace Gameplay.GameplayStates
{
    public class FinishedGameStateWithWinner : GameplayState
    {
        private readonly PlayerType m_PlayerWon;
        private readonly bool pvpMatch;
        private readonly Triliza m_Triliza;

        public FinishedGameStateWithWinner(bool gameWasPvP, PlayerType playerWon , Triliza triliza)
        {
            m_PlayerWon = playerWon;
            pvpMatch = gameWasPvP;
            m_Triliza = triliza;
        }

        public override void OnEnterState()
        {
            m_Triliza.SwitchInteractOff();
            if (pvpMatch)
            {
                if (m_PlayerWon == PlayerType.PLAYER)
                {
                    GameManager.Instance.GameMenu.InfoText.text = "Player 1 WINS";
                }
                else
                {
                    GameManager.Instance.GameMenu.InfoText.text = "Player 2 WINS";
                }
            }
            else
            {
                if (m_PlayerWon == PlayerType.PLAYER)
                {
                    GameManager.Instance.GameMenu.InfoText.text = "Player WINS";
                }
                else
                {
                    GameManager.Instance.GameMenu.InfoText.text = "COM WINS";
                }
            }
        }

        public override void OnLeaveState()
        {
        }

        public override void Update()
        {
        }
    }
}
