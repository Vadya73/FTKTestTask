namespace CodeBase.StateMachine.Battler
{
    public class BattleEnterState : IState
    {
        private readonly BattleStateMachine _stateMachine;

        public BattleEnterState(BattleStateMachine stateMAchine)
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