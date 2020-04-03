using Gameplay;

namespace Gameplay.AI
{
    public interface IAILogic 
    {
        CellPosition GetCellAiChoseBasedOnBoard(Cell[,] board);
    }
}
