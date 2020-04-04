using UnityEngine;
using UnityEngine.UI;
using Gameplay;
using Core.StateMachine;
using Gameplay.AI;

namespace UI.Menus
{
    public class EndMenu : MonoBehaviour
    {
        public Button RestartButton;
        public Button BackToMenuButton;

        private void Awake()
        {
            RestartButton.SetMethod(RecreateGame);
            BackToMenuButton.SetMethod(LeaveGame);
        }

        private void RecreateGame()
        {
            GameManager.Instance.InfoText.text = "";
            GameManager.Instance.SwitchToState(new InGameState(new Triliza(GameManager.Instance.CurrentTrilizaGame)));
        }

        private void LeaveGame()
        {
            GameManager.Instance.SwitchToState(new MenuState());
        }
    }
}
