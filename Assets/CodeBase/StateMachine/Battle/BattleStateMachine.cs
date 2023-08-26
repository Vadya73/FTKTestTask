using System;
using System.Collections.Generic;

namespace CodeBase.StateMachine.Battler
{
    public class BattleStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IExitableState _activeState;

        public BattleStateMachine()
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BattleEnterState)] = new BattleEnterState(this),
                [typeof(BattleLoopState)] = new BattleLoopState(this),
                [typeof(ChangeTeamBattleState)] = new ChangeTeamBattleState(this),
                [typeof(BattleFinishState)] = new BattleFinishState(this),
            };
        }
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }
        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}