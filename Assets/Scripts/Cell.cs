using UnityEngine;
using Gameplay.Systems.Interact;

public enum CellStatus { UNOCCUPIED, PLAYER, ENEMY };
namespace Gameplay
{
    public class Cell : MonoBehaviour, IInteractable
    {
        public CellStatus CellData;
        [SerializeField] private SpriteRenderer m_SpriteOnCellView;
        [SerializeField] private CellSpriteData m_CellSpriteData;

        private Triliza m_Triliza;
        private CellPosition m_CellPosition;

        public void Init(Triliza triliza, int x, int y)
        {
            m_Triliza = triliza;
            m_CellPosition = new CellPosition { x = x, y = y };
            ResetCellView();
        }

        public void ResetCellView()
        {
            CellData = CellStatus.UNOCCUPIED;
            m_SpriteOnCellView.sprite = null;
        }

        public void Interact()
        {
            m_Triliza.InteractWithCell(m_CellPosition);
        }

        public void CellSelectedByPlayer(PlayerType playerType)
        {
            switch (playerType)
            {
                case (PlayerType.PLAYER):
                    CellData = CellStatus.PLAYER;
                    m_SpriteOnCellView.sprite = m_CellSpriteData.PlayerIconSprite;
                    break;
                case (PlayerType.ENEMY):
                    CellData = CellStatus.ENEMY;
                    m_SpriteOnCellView.sprite = m_CellSpriteData.EnemyIconSprite;
                    break;
            }
        }
    }
}
