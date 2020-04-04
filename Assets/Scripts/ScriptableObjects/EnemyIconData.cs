using Gameplay;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyIconData", menuName = "Data/EnemyIconData", order = 0)]
public class EnemyIconData : ScriptableObject
{
    [SerializeField] private Sprite m_EnemyEasySprite;
    [SerializeField] private Sprite m_EnemyHardSprite;
    [SerializeField] private Sprite m_PvPSprite;

    public Sprite GetSpriteBasedOnGameType(GameType gameType)
    {
        switch (gameType)
        {
            case GameType.PVP:
                return m_PvPSprite;
            case GameType.EASY:
                return m_EnemyEasySprite;
            case GameType.HARD:
                return m_EnemyHardSprite;
            default:
                throw new System.Exception("(JohnMir): Should not happen");
        }
    }
}
