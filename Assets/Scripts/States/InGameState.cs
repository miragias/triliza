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
            GameManager.Instance.Board.gameObject.SetActive(true);
            GameManager.Instance.GameMenu.gameObject.SetActive(true);
        }

        public override void OnLeaveState()
        {
            GameManager.Instance.GameMenu.gameObject.SetActive(false);
            GameManager.Instance.Board.gameObject.SetActive(false);
        }

        public override void Tick()
        {
            m_Triliza.Tick();
        }
    }
}
