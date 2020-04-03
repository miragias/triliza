using UnityEngine;
using Core.StateMachine;
using TMPro;

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