using System.Collections.Generic;

namespace Gameplay
{
    public class CheckWinLogic
    {
        /// <summary>
        /// Keeps count of how many checks a player has done in a specific row/column/diagonal. If a player makes 3 he wins
        /// </summary>
        private class CellArrayData
        {
            public Dictionary<PlayerType , int> playerTypeToHowManyValsInCollectionDictionary = new Dictionary<PlayerType , int>();
        }

        private readonly CellArrayData[] m_Rows;
        private readonly CellArrayData[] m_Columns;
        private readonly CellArrayData[] m_Diagonals;
        private readonly BoardReferences m_BoardReferences;

        public CheckWinLogic(int rowNumber , int columnNumber , int diagonalNumber , PlayerType[] playerTypes)
        {
            m_BoardReferences = GameManager.Instance.BoardReferences;

            m_Rows = new CellArrayData[rowNumber];
            PopulateCellArrayData(m_Rows, playerTypes);
            m_Columns = new CellArrayData[columnNumber];
            PopulateCellArrayData(m_Columns, playerTypes);
            m_Diagonals = new CellArrayData[diagonalNumber];
            PopulateCellArrayData(m_Diagonals, playerTypes);
        }

        private void PopulateCellArrayData(CellArrayData[] arrayData ,  PlayerType[] playerTypes)
        {
            for (int i = 0; i < arrayData.Length; i++)
            {
                arrayData[i] = new CellArrayData();   
                arrayData[i].playerTypeToHowManyValsInCollectionDictionary = new Dictionary<PlayerType, int>();
                for (int j = 0; j < playerTypes.Length; j++)
                {
                    arrayData[i].playerTypeToHowManyValsInCollectionDictionary.Add(playerTypes[j] , 0);
                }
            }
        }

        public bool CheckPlayerWon(PlayerType playerChecking, CellPosition cellJustChosen)
        {
            //Update dictionaries
            m_Rows[cellJustChosen.x].playerTypeToHowManyValsInCollectionDictionary[playerChecking]++;
            m_Columns[cellJustChosen.y].playerTypeToHowManyValsInCollectionDictionary[playerChecking]++;
            //Diagonal dictionaries
            if ((cellJustChosen.x == 0 && cellJustChosen.y == 0) ||
                (cellJustChosen.x == 2 && cellJustChosen.y == 2))
            {
                m_Diagonals[0].playerTypeToHowManyValsInCollectionDictionary[playerChecking]++;
            }
            if ((cellJustChosen.x == 0 && cellJustChosen.y == 2) ||
                (cellJustChosen.x == 2 && cellJustChosen.y == 0))
            {
                m_Diagonals[1].playerTypeToHowManyValsInCollectionDictionary[playerChecking]++;
            }
            if(cellJustChosen.x == 1 && cellJustChosen.y == 1)
            {
                m_Diagonals[0].playerTypeToHowManyValsInCollectionDictionary[playerChecking]++;
                m_Diagonals[1].playerTypeToHowManyValsInCollectionDictionary[playerChecking]++;
            }

            //Check all rows/columns/diagonals
            for (int i = 0; i < m_Rows.Length; i++)
            {
                if(m_Rows[i].playerTypeToHowManyValsInCollectionDictionary[playerChecking] == 3)
                {
                    m_BoardReferences.RowImages[i].enabled = true;
                    return true;
                }
            }
            for (int i = 0; i < m_Columns.Length; i++)
            {
                if(m_Columns[i].playerTypeToHowManyValsInCollectionDictionary[playerChecking] == 3)
                {
                    m_BoardReferences.ColumnImages[i].enabled = true;
                    return true;
                }
            }
            for (int i = 0; i < m_Diagonals.Length; i++)
            {
                if(m_Diagonals[i].playerTypeToHowManyValsInCollectionDictionary[playerChecking] == 3)
                {
                    m_BoardReferences.DiagonalImages[i].enabled = true;
                    return true;
                }
            }
            return false;
        }
    }
}
