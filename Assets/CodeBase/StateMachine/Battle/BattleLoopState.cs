using UnityEngine;

namespace CodeBase.StateMachine.Battler
{
    public class BattleLoopState : IState
    {
        private readonly BattleStateMachine _stateMachine;

        public BattleLoopState(BattleStateMachine stateMAchine)
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