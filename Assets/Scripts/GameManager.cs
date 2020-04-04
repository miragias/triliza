using UnityEngine;
using Core.StateMachine;
using TMPro;
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

    public TextMeshProUGUI InfoText;
    public State CurrentState;
    public Triliza CurrentTrilizaGame;
    public GameObject Board;

    public MainMenu MainMenu;
    public EndMenu EndMenu;


    private void Awake()
    {
        mInstance = this;
        SwitchToState(new MenuState());
    }

    private void Update()
    {
        CurrentState.Tick();
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