using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
    [CreateAssetMenu(fileName = "HardAI", menuName = "AI/Hard", order = 0)]
    public class AIAdvanced : ScriptableObject, IAILogic
    {
        public CellPosition GetCellAiChoseBasedOnBoard(Cell[,] board)
        {
            bool pickRandom = Random.Range(0, 3) == 0;
            if(pickRandom) //Case that we let the player take it easy
            {
                RandomAiChooseLogic simpleRandomChooseLogic = new RandomAiChooseLogic(board);
                return simpleRandomChooseLogic.ChooseRandomCell();
            }

            //Hard mode
            int[] rowPlayerChosenInfo =  GetRowPlayerChosenData(board);
            int[] columnPlayerChosenInfo = GetColumnPlayerChosenData(board);
        }

        private int[] GetRowPlayerChosenData(Cell[,] board)
        {
            int[] numberOfPlayerPicksOnRows = new int[3];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i,j].CellData == CellStatus.PLAYER)
                    {
                        numberOfPlayerPicksOnRows[i]++;
                    }
                }
            }
            return numberOfPlayerPicksOnRows;
        }

        private int[] GetColumnPlayerChosenData(Cell[,] board)
        {
            int[] numberOfPlayerColumnPicks = new int[3];

            for (int i = 0; i < board.GetLength(1); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if(board[i,j].CellData == CellStatus.PLAYER)
                    {
                        numberOfPlayerColumnPicks[i]++;
                    }
                }
            }
            return numberOfPlayerColumnPicks;
        }
    }
}
