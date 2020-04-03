using UnityEngine;
using UnityEngine.UI;
using Gameplay;
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
            VsEasyButton.SetMethod(CreatePlayerVsPlayerGame);
            VsHardButton.SetMethod(CreatePlayerVsHardGame);
        }

        private void CreatePlayerVsPlayerGame()
        {
            Triliza triliza = new Triliza();
            GameManager.Instance.SwitchToState(new InGameState(triliza));
            DisableUIButtons();
        }

        private void CreatePlayerVsEasyGame()
        {
            Triliza triliza = new Triliza();
            GameManager.Instance.SwitchToState(new InGameState(triliza));
            DisableUIButtons();
        }

        private void CreatePlayerVsHardGame()
        {
            Triliza triliza = new Triliza();
            GameManager.Instance.SwitchToState(new InGameState(triliza));
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
