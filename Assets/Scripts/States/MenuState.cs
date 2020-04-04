namespace Core.StateMachine
{
    public class MenuState : State
    {
        public override void OnEnterState()
        {
            GameManager.Instance.MainMenu.gameObject.SetActive(true);
            GameManager.Instance.MainMenu.SetStateOfUIButtons(true);
        }
        public override void OnLeaveState()
        {
            GameManager.Instance.MainMenu.gameObject.SetActive(false);
        }

        public override void Tick()
        {
        }
    }
}
