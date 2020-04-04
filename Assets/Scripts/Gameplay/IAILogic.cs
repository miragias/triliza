using Gameplay;

namespace Gameplay.AI
{
    public interface IAILogic 
    {
        GameType GetGameType { get; }
        CellPosition GetCellAiChoseBasedOnBoard(Cell[,] board);
    }
}
