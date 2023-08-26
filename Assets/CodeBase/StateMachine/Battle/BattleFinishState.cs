namespace CodeBase.StateMachine.Battler
{
    public class BattleFinishState : IState
    {
        private readonly BattleStateMachine _stateMachine;

        public BattleFinishState(BattleStateMachine stateMAchine)
        {
            _stateMachine = stateMAchine;
        }
        public void Exit()
        {
        }

        public void Enter()
        {
        }

        public void Update()
        {
        }
    }
}