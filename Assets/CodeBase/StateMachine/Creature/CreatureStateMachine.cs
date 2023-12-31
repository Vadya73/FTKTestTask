﻿using System;
using System.Collections.Generic;

namespace CodeBase.StateMachine.Creature
{
    public class CreatureStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IExitableState _activeState;
        public CreatureStateMachine()
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(CreatureIdleState)] = new CreatureIdleState(),
                [typeof(CreatureMoveState)] = new CreatureMoveState(),
                [typeof(CreatureMeleeAttackState)] = new CreatureMeleeAttackState(),
                [typeof(CreatureRangeAttackState)] = new CreatureRangeAttackState(),
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