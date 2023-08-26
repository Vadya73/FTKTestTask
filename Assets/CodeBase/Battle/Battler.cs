using CodeBase.StateMachine.Battler;
using UnityEngine;

namespace CodeBase.Battle
{
    public class Battler : MonoBehaviour, IInitializable
    {
        [SerializeField] private Team[] _teams;

        private Team _teamMove;
        private BattleStateMachine _stateMachine;
        
        private const string Team = "Team"; 
        
        public void Initialize()
        {
            _stateMachine = new BattleStateMachine();
            
            var objects = GameObject.FindGameObjectsWithTag(Team);
            _teams = new Team[objects.Length];

            for (var i = 0; i < objects.Length; i++)
            {
                _teams[i] = objects[i].GetComponent<Team>();
            }

            foreach (var team in _teams)
            {
                if (team.Id == "Player")
                {
                    SetMove(team);
                }
                
                team.CanFight = false;
            }
        }

        private void SetMove(Team team)
        {
            _teamMove = team;
            team.CanFight = true;
        }

        private void ChangeTeamMove()
        {
            for (var i = 0; i < _teams.Length; i++)
            {
                var team = _teams[i];
                if (_teamMove == team)
                {
                    if (i++ <= _teams.Length)
                    {
                        SetMove(_teams[0]);
                    }
                    SetMove(_teams[i++]);
                }
            }
        }
        
        
    }
}