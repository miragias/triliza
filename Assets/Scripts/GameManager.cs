using UnityEngine;
using Core.StateMachine;

public class GameManager : MonoBehaviour
{
    #region singleton
    static GameManager mInstance;
    public static GameManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject go = new GameObject();
                mInstance = go.AddComponent<GameManager>();
            }
            return mInstance;
        }
    }
    #endregion

    public State CurrentState;
    private void Awake()
    {
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