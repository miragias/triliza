using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
    [CreateAssetMenu(fileName = "HardAI", menuName = "AI/Hard", order = 0)]
    public class AIAdvanced : ScriptableObject, IAILogic
    {
        public GameType GetGameType => GameType.HARD;

        public CellPosition GetCellAiChoseBasedOnBoard(Cell[,] board)
        {
            bool pickRandom = Random.Range(0, 3) == 0;
            RandomAiChooseLogic simpleRandomChooseLogic = new RandomAiChooseLogic(board);
            CellPosition totallyRandomCell = simpleRandomChooseLogic.ChooseRandomCell();
            /*
            if(pickRandom) //Small chance for the computer to take it easy and just choose a random one
            {
                return totallyRandomCell;
            }
            */

            //Try to do a "hard" AI Algorithm. What we do is check all rows/columns if they have a row/column with more than 1 of the Player symbols.
            //if yes we try and make the COM spawn one on that row. If that fails for all of them use the totallyRandom one we chose above. A more 
            //correct AI would check if it could also win the game on the next move but I didn't want it to always end in ties. For this one 
            // I am only making it to "stop the player" from winning. 
            List<CellPosition> cellsFree = simpleRandomChooseLogic.GetAllFreeCells();

            int[] rowPlayerChosenInfo =  GetRowPlayerChosenData(board); //Find number of player symbols for each row
            for (int i = 0; i < rowPlayerChosenInfo.Length; i++)
            {
                foreach(var cell in cellsFree)
                {
                    if(rowPlayerChosenInfo[i] > 1 && cell.x == i)
                    {
                        return cell;
                    }
                }
            }
            int[] columnPlayerChosenInfo = GetColumnPlayerChosenData(board); //Find number of player symbols for each column
            for (int i = 0; i < columnPlayerChosenInfo.Length; i++)
            {
                foreach(var cell in cellsFree)
                {
                    if(columnPlayerChosenInfo[i] > 1 && cell.y == i)
                    {
                        return cell;
                    }
                }
            }
            //If nothing of interest found above use the totally random cell
            return totallyRandomCell;
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

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[j,i].CellData == CellStatus.PLAYER)
                    {
                        numberOfPlayerColumnPicks[i]++;
                    }
                }
            }
            return numberOfPlayerColumnPicks;
        }
    }
}
