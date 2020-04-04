using UnityEngine;
using Core.StateMachine;
using Gameplay;
using UI.Menus;

public class GameManager : MonoBehaviour
{
    #region singleton
    static GameManager mInstance;
    public static GameManager Instance
    {
        get
        {
            return mInstance;
        }
    }
    #endregion

    //NOTE(JohnMir): Keeping these here just for easy singleton access and cause it's a simple game.
    public Triliza CurrentTrilizaGame;
    public BoardReferences BoardReferences;
    //

    public State CurrentState; //GameState

    //Menus
    public MainMenu MainMenu;
    public EndMenu GameMenu;


    private void Awake()
    {
        mInstance = this;
        SwitchToState(new MenuState());
    }

    private void Update()
    {
        CurrentState.Tick();
    }

    public void SetupGameMenu()
    {
    }

    public void SwitchToState(State state)
    {
        if(CurrentState != null)
        {
            CurrentState.OnLeaveState();
        }
        CurrentState = state;
        CurrentState.OnEnterState();
    }
}