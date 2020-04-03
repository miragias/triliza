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
            DisableUIButtons();
        }

        private void CreatePlayerVsEasyGame()
        {
            GameManager.Instance.SwitchToState(new InGameState(TrilizaFactory.CreatePlayerVsEasyGame()));
            DisableUIButtons();
        }

        private void CreatePlayerVsHardGame()
        {
            GameManager.Instance.SwitchToState(new InGameState(TrilizaFactory.CreatePlayerVsHardGame()));
            DisableUIButtons();
        }

        private void DisableUIButtons()
        {
            VsPlayerButton.interactable = false;
            VsEasyButton.interactable = false;
            VsHardButton.interactable = false;
        }
    }
}
