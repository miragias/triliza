using UnityEngine;
using UnityEngine.UI;
using Gameplay;
using Core.StateMachine;
using TMPro;

namespace UI.Menus
{
    public class EndMenu : MonoBehaviour
    {
        public Button RestartButton;
        public Button BackToMenuButton;
        public TextMeshProUGUI InfoText;

        [Header("VsBarStuff")] 
        [SerializeField] private Image m_EnemyImage;
        public TextMeshProUGUI PvPText;

        [SerializeField] private EnemyIconData m_EnemyIconData;

        private void Awake()
        {
            RestartButton.SetMethod(RecreateGame);
            BackToMenuButton.SetMethod(LeaveGame);
        }

        private void RecreateGame()
        {
            InfoText.text = "";
            GameManager.Instance.SwitchToState(new InGameState(new Triliza(GameManager.Instance.CurrentTrilizaGame)));
        }

        private void LeaveGame()
        {
            GameManager.Instance.SwitchToState(new MenuState());
        }

        public void SetupGameMenu(GameType gameType)
        {
            m_EnemyImage.sprite = m_EnemyIconData.GetSpriteBasedOnGameType(gameType);
        }
    }
}
