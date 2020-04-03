using Gameplay.AI;
using Gameplay.Systems.Interact;
using Gameplay.GameplayStates;
using UnityEngine;

namespace Gameplay
{
    public struct CellPosition
    {
        public int x;
        public int y;
    }

    public class Triliza
    {
        private const int NUMBER_OF_CELLS_PER_LINE = 3;
        private const int NUMBER_OF_CELLS_PER_ROW = 3;
        private readonly IAILogic m_AiLogic;
        private Cell[,] m_BoardCells;

        private InteractSystem m_InteractSystem;
        private NoInteractSystem m_NoInteractSystem;

        private IInteractSystem m_CurrentInteractSystem;
        private GameplayState m_CurrentGameplayState;

        public enum Player { PLAYER, ENEMY };
        private Player m_CurrentPlayer;

        //For PVP
        public Triliza()
        {
            UnityEngine.Debug.Log("<color=blue>CREATE NEW GAME PVP"+ "</color>");
            m_AiLogic = null;
            SetupRestOfStuff();
        }

        //For VS COM
        public Triliza(IAILogic logic)
        {
            UnityEngine.Debug.Log("<color=blue>CREATE NEW GAME PVE"+ "</color>");
            m_AiLogic = logic;
            SetupRestOfStuff();
        }

        private void SetupRestOfStuff()
        {
            CreateCellViewsArray();
            ResetAllCelViews();
            m_InteractSystem = Resources.Load<InteractSystem>("InteractSystem");
            m_InteractSystem.SetupSystem(Camera.main);
            m_NoInteractSystem = Resources.Load<NoInteractSystem>("NoInteractSystem");
            SwitchInteractOn();
            ChooseRandomStartingPlayer();
        }

        private void CreateCellViewsArray()
        {
            Cell[] allCellViews = UnityEngine.Object.FindObjectOfType<BoardReferences>().CellViewsOnBoard;

            //NOTE(JohnMir): Convert 1D to 2D array for easier access 
            m_BoardCells = new Cell[NUMBER_OF_CELLS_PER_LINE, NUMBER_OF_CELLS_PER_ROW];
            int index = 0;
            for (int i = 0; i < m_BoardCells.GetLength(0); i++)
            {
                for (int j = 0; j < m_BoardCells.GetLength(1); j++)
                {
                    m_BoardCells[i, j] = allCellViews[index];
                    m_BoardCells[i, j].Init(this, i, j);
                    index++;
                }
            }
        }

        private void ResetAllCelViews()
        {
            for (int i = 0; i < m_BoardCells.GetLength(0); i++)
            {

                for (int j = 0; j < m_BoardCells.GetLength(1); j++)
                {
                    m_BoardCells[i, j].ResetCellView();
                }
            }
        }

        private void ChooseRandomStartingPlayer()
        {
            m_CurrentPlayer = (Player)Random.Range(0, 2);
        }

        private void SetStartingGameplayState()
        {
            if(m_CurrentPlayer == Player.ENEMY)
            {
                if(m_AiLogic == null)
                {
                    SwitchToGameplayState(new PlayerChooseState(this));
                }
                else
                {
                    SwitchToGameplayState(new AiChooseState(this));
                }
            }
            if(m_CurrentPlayer == Player.PLAYER)
            {
                SwitchToGameplayState(new PlayerChooseState(this));
            }
        }

        public void SwitchInteractOn()
        {
            m_CurrentInteractSystem = m_InteractSystem;
        }
        public void SwitchInteractOff()
        {
            m_CurrentInteractSystem = m_NoInteractSystem;
        }

        public void InteractWithCell(CellPosition cellPosition)
        {
            int x = cellPosition.x;
            int y = cellPosition.y;

            if (m_BoardCells[x, y].CellData == Cell.CellStatus.UNOCCUPIED)
            {
                if (m_CurrentPlayer == Player.PLAYER)
                {
                    m_BoardCells[x, y].CellData = Cell.CellStatus.PLAYER;
                }
                else 
                {
                    m_BoardCells[x, y].CellData = Cell.CellStatus.ENEMY;
                }
            }
        }
        public void Tick()
        {
            m_CurrentInteractSystem.Tick();
        }

        public void SwitchToGameplayState(GameplayState gameplayState)
        {
            if(m_CurrentGameplayState != null)
            {
                m_CurrentGameplayState.OnLeaveState();
            }
            m_CurrentGameplayState = gameplayState;
            m_CurrentGameplayState.OnEnterState();
        }

        public void PickAiMove()
        {
            m_AiLogic.GetCellAiChoseBasedOnBoard(this.m_BoardCells);
        }
    }
}
