using UnityEngine;
using Gameplay.Systems.Interact;
using Gameplay;

public class Cell : MonoBehaviour, IInteractable
{
    public CellStatus CellData;
    [SerializeField] private SpriteRenderer m_SpriteOnCellView; 
    [SerializeField] private CellSpriteData m_CellSpriteData; 

    public enum CellStatus { UNOCCUPIED , PLAYER , ENEMY};
    private Triliza m_Triliza;
    private CellPosition m_CellPosition;

    public void Init(Triliza triliza , int x , int y)
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
        UnityEngine.Debug.Log("<color=blue>INTERACT WITH : "+ m_CellPosition.x+ " " + m_CellPosition.y+ "</color>");
        m_Triliza.InteractWithCell(m_CellPosition);
    }

    public void CellSelectedByPlayer(Triliza.Player player)
    {
        switch(player)
        {
            case (Triliza.Player.PLAYER):
                CellData = CellStatus.PLAYER;
                m_SpriteOnCellView.sprite = m_CellSpriteData.PlayerIconSprite;
                break;
            case (Triliza.Player.ENEMY):
                CellData = CellStatus.ENEMY;
                m_SpriteOnCellView.sprite = m_CellSpriteData.EnemyIconSprite;
                break;
        }
    }
}
