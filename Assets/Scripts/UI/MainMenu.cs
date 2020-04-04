using UnityEngine;
using UnityEngine.UI;
using Core.StateMachine;

namespace UI.Menus
{
    public class MainMenu : MonoBehaviour
    {
        public Button VsPlayerButton;
        public Button VsEasyButton;
        public Button VsHardButton;

        private void Awake()
        {
            VsPlayerButton.SetMethod(CreatePlayerVsPlayerGame);
            VsEasyButton.SetMethod(CreatePlayerVsEasyGame);
            VsHardButton.SetMethod(CreatePlayerVsHardGame);
        }

        private void CreatePlayerVsPlayerGame()
        {
            GameManager.Instance.SwitchToState(new InGameState(TrilizaFactory.CreatePlayerVsPlayerGame()));
            SetStateOfUIButtons(false);
        }

        private void CreatePlayerVsEasyGame()
        {
            GameManager.Instance.SwitchToState(new InGameState(TrilizaFactory.CreatePlayerVsEasyGame()));
            SetStateOfUIButtons(false);
        }

        private void CreatePlayerVsHardGame()
        {
            GameManager.Instance.SwitchToState(new InGameState(TrilizaFactory.CreatePlayerVsHardGame()));
            SetStateOfUIButtons(false);
        }

        public void SetStateOfUIButtons(bool state)
        {
            VsPlayerButton.interactable = state;
            VsEasyButton.interactable = state;
            VsHardButton.interactable = state;
        }
    }
}
