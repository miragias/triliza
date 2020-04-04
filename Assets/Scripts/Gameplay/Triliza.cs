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
    public enum PlayerType { PLAYER, ENEMY };
    public enum GameType { PVP , EASY , HARD};

    public class Triliza
    {
        private const int NUMBER_OF_CELLS_PER_LINE = 3;
        private const int NUMBER_OF_CELLS_PER_ROW = 3;

        private readonly IAILogic m_AiLogic;
        public bool IsAIGame => m_AiLogic != null;

        private readonly CheckWinLogic m_CheckWinLogic;
        private Cell[,] m_BoardCells;

        private InteractSystem m_InteractSystem;
        private NoInteractSystem m_NoInteractSystem;

        private IInteractSystem m_CurrentInteractSystem;
        private GameplayState m_CurrentGameplayState;

        private PlayerType m_CurrentPlayer;
        private readonly GameType m_GameType;
        public bool GameHasEnded = false;

        //For PVP
        public Triliza()
        {
            m_CheckWinLogic = new CheckWinLogic(3, 3, 2 , new PlayerType[]{PlayerType.ENEMY , PlayerType.PLAYER});
            m_AiLogic = null;
            m_GameType = GameType.PVP;
            GameHasEnded = false;

            SetupRestOfStuff();
        }

        //For VS COM
        public Triliza(IAILogic logic)
        {
            m_CheckWinLogic = new CheckWinLogic(3, 3, 2 , new PlayerType[] { PlayerType.ENEMY, PlayerType.PLAYER });
            m_AiLogic = logic;
            m_GameType = logic.GetGameType;
            GameHasEnded = false;

            SetupRestOfStuff();
        }

        //For easy recreation
        public Triliza(Triliza triliza)
        {
            m_CheckWinLogic = new CheckWinLogic(3, 3, 2 , new PlayerType[] { PlayerType.ENEMY, PlayerType.PLAYER });
            m_AiLogic = triliza.m_AiLogic;
            m_GameType = triliza.m_GameType;
            GameHasEnded = false;

            SetupRestOfStuff();
        }

        private void SetupRestOfStuff()
        {
            CreateCellViewsArray();
            ResetAllCelViews();
            SetupInteractSystems();
            SwitchInteractOn();
            ChooseRandomStartingPlayer();
            SetStartingGameplayState();
            SetupUIStuff();
        }

        private void CreateCellViewsArray()
        {
            Cell[] allCellViews = GameManager.Instance.BoardReferences.GetComponent<BoardReferences>().CellViewsOnBoard;

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

        private void SetupInteractSystems()
        {
            m_InteractSystem = Resources.Load<InteractSystem>("InteractSystem");
            m_InteractSystem.SetupSystem(Camera.main);
            m_NoInteractSystem = Resources.Load<NoInteractSystem>("NoInteractSystem");
        }

        private void ChooseRandomStartingPlayer()
        {
            m_CurrentPlayer = (PlayerType)Random.Range(0, 2);
        }

        private void SetStartingGameplayState()
        {
            if(m_CurrentPlayer == PlayerType.ENEMY)
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
            if(m_CurrentPlayer == PlayerType.PLAYER)
            {
                SwitchToGameplayState(new PlayerChooseState(this));
            }
        }

        private void SetupUIStuff()
        {
            GameManager.Instance.GameMenu.SetupGameMenu(m_GameType);
            GameManager.Instance.BoardReferences.DisableAllImages();

            GameManager.Instance.GameMenu.InfoText.text = "";
            if(IsAIGame)
            {
                GameManager.Instance.GameMenu.PvPText.text = "";
            }
            else
            {
                SetCurrentPlayerUIText();
            }
        }

        private void SetCurrentPlayerUIText()
        {
            if (IsAIGame) return;
            string playerChoosingString = m_CurrentPlayer == PlayerType.PLAYER ? "X" : "O";
            GameManager.Instance.GameMenu.PvPText.text = "Currently choosing :" + playerChoosingString;
        }

        public void SwitchInteractOn()
        {
            m_CurrentInteractSystem = m_InteractSystem;
        }
        public void SwitchInteractOff()
        {
            m_CurrentInteractSystem = m_NoInteractSystem;
        }

        public void InteractWithCell(CellPosition cellPosition, bool playerSelecting = true)
        {
            int x = cellPosition.x;
            int y = cellPosition.y;

            if (m_BoardCells[x, y].CellData == CellStatus.UNOCCUPIED)
            {
                if (m_CurrentPlayer == PlayerType.PLAYER)
                {
                    m_BoardCells[x, y].CellData = CellStatus.PLAYER;
                    m_BoardCells[x, y].CellSelectedByPlayer(PlayerType.PLAYER);
                }
                else 
                {
                    m_BoardCells[x, y].CellData = CellStatus.ENEMY;
                    m_BoardCells[x, y].CellSelectedByPlayer(PlayerType.ENEMY);
                }

                bool playerWon = m_CheckWinLogic.CheckPlayerWon(m_CurrentPlayer , cellPosition);
                if(playerWon)
                {
                    SwitchToGameplayState(new FinishedGameStateWithWinner(m_AiLogic == null, m_CurrentPlayer , this));
                    GameHasEnded = true;
                    return;
                }
                bool checkTie = CheckTie();
                if (checkTie)
                {
                    GameHasEnded = true;
                    SwitchToGameplayState(new FinishedGameStateWithTie(this));
                    return;
                }
                SwitchPlayer();
                if(playerSelecting)
                {
                    GoToNextStateAfterInteract();
                }
            }
        }
        private bool CheckTie()
        {
            for (int i = 0; i < m_BoardCells.GetLength(0); i++)
            {
                for (int j = 0; j < m_BoardCells.GetLength(1); j++)
                {
                    if(m_BoardCells[i,j].CellData == CellStatus.UNOCCUPIED)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void SwitchPlayer()
        {
            int currentPlayer = (int)m_CurrentPlayer;
            currentPlayer++;
            m_CurrentPlayer = (PlayerType)((currentPlayer) % 2);
            SetCurrentPlayerUIText();
        }

        private void GoToNextStateAfterInteract()
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
            CellPosition cellPositionChosen = m_AiLogic.GetCellAiChoseBasedOnBoard(this.m_BoardCells);
            InteractWithCell(cellPositionChosen , false);
        }
    }
}
