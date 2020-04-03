using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
    public class AISimple : ScriptableObject, IAILogic
    {
        public CellPosition GetCellAiChoseBasedOnBoard(Cell[,] board)
        {
            CellPosition cellChosen = ChooseACellInRandom(GetAllFreeCells(board));
            return cellChosen;
        }

        //TODO(JohnMir): Maybe move this to the triliza class?
        private List<CellPosition> GetAllFreeCells(Cell[,] board)
        {
            List<CellPosition> allFreeCells = new List<CellPosition>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j].CellData == Cell.CellStatus.UNOCCUPIED)
                    {
                        CellPosition currentIteratingCellPos = new CellPosition { x = i, y = j };
                        allFreeCells.Add(currentIteratingCellPos);
                    }
                }
            }
            return allFreeCells;
        }

        private CellPosition ChooseACellInRandom(List<CellPosition> allNonOccupiedCellPositions)
        {
            int randomIndex = Random.Range(0, allNonOccupiedCellPositions.Count);
            return allNonOccupiedCellPositions[randomIndex];
        }
    }
}
