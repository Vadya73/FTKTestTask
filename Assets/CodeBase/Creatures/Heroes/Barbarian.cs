using System;
using System.Collections.Generic;
using CodeBase.StateMachine;
using CodeBase.StateMachine.Creature;
using UnityEngine;

namespace CodeBase.Creatures.Heroes
{
    public class Barbarian : Creature
    {
        
        private Dictionary<Type, IState> _states;
        private IState _currentState;


        public override void Initialize()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = _maxHealth;
            _defaultPosition = transform.position;
            
            InitialStates();
            SetIdleState();
        }
        
        private void Update()
        {
            _currentState?.Update();
        }

        private void InitialStates()
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(CreatureIdleState)] = new CreatureIdleState(),
                [typeof(CreatureMoveState)] = new CreatureMoveState(),
                [typeof(CreatureMeleeAttackState)] = new CreatureMeleeAttackState()
            };
        }

        private void SetState(IState newState)
        {
            if (_currentState != null)
                _currentState.Exit();

            _currentState = newState;
            _currentState.Enter();
        }

        private IState GetState<T>() where T : IState
        {
            var type = typeof(T);
            return _states[type];
        }

        private void SetStateByDefault()
        {
            var stateByDefault = GetState<CreatureIdleState>();
            SetState(stateByDefault);
        }
        
        public void SetIdleState()
        {
            var state = GetState<CreatureIdleState>();
            SetState(state);
        }

        public void SetMoveState()
        {
            var state = GetState<CreatureMoveState>();
            SetState(state);
        }

        public void SetAttackState()
        { 
            var state = GetState<CreatureMeleeAttackState>();
            SetState(state);
        }
    }
}