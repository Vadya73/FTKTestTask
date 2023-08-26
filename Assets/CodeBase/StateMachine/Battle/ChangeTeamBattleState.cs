using UnityEngine;

namespace CodeBase.StateMachine.Battler
{
    public class ChangeTeamBattleState : IState
    {
        private readonly BattleStateMachine _stateMachine;

        public ChangeTeamBattleState(BattleStateMachine stateMAchine)
        {
            _stateMachine = stateMAchine;
        }
        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}