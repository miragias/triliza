using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{

    public class RandomAiChooseLogic
    {
        private readonly Cell[,] m_Board;

        public RandomAiChooseLogic(Cell[,] board)
        {
            m_Board = board;
        }

        public CellPosition ChooseRandomCell()
        {
            return ChooseACellInRandom(GetAllFreeCells());
        }

        public List<CellPosition> GetAllFreeCells()
        {
            List<CellPosition> allFreeCells = new List<CellPosition>();
            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(1); j++)
                {
                    if (m_Board[i, j].CellData == CellStatus.UNOCCUPIED)
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

    [CreateAssetMenu(fileName = "SimpleAi", menuName = "AI/Simple", order = 0)]
    public class AISimple : ScriptableObject, IAILogic
    {
        public RandomAiChooseLogic simpleRandomChooseLogic;

        public CellPosition GetCellAiChoseBasedOnBoard(Cell[,] board)
        {
            simpleRandomChooseLogic = new RandomAiChooseLogic(board);
            return simpleRandomChooseLogic.ChooseRandomCell();
        }
    }
}
