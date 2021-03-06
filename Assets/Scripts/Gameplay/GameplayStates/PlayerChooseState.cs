﻿namespace Gameplay.GameplayStates
{
    public class PlayerChooseState : GameplayState
    {
        private readonly Triliza m_Triliza;

        public PlayerChooseState(Triliza triliza)
        {
            m_Triliza = triliza;
        }

        public override void OnEnterState()
        {
            m_Triliza.SwitchInteractOn();
        }

        public override void OnLeaveState()
        {
            m_Triliza.SwitchInteractOff();
        }

        public override void Update()
        {
        }
    }
}
