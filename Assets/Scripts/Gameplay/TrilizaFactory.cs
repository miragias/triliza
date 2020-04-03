using Gameplay;
using Gameplay.AI;
using UnityEngine;

public static class TrilizaFactory
{
    public static Triliza CreatePlayerVsPlayerGame()
    {
        Triliza triliza = new Triliza();
        GameManager.Instance.CurrentTrilizaGame = triliza;
        return triliza;
    }
    public static Triliza CreatePlayerVsEasyGame()
    {
        AISimple simpleAi = Resources.Load<AISimple>("SimpleAi");
        Triliza triliza = new Triliza(simpleAi);
        GameManager.Instance.CurrentTrilizaGame = triliza;
        return triliza;
    }

    public static Triliza CreatePlayerVsHardGame()
    {
        AIAdvanced hardAI = Resources.Load<AIAdvanced>("HardAi");
        Triliza triliza = new Triliza(hardAI);
        GameManager.Instance.CurrentTrilizaGame = triliza;
        return triliza;
    }
}
