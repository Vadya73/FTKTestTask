using CodeBase.StateMachine.Battler;
using UnityEngine;

namespace CodeBase.Battle
{
    public class Battler : MonoBehaviour, IInitializable
    {
        [SerializeField] private Team[] _teams;

        [SerializeField] private Team _teamMove;
        private BattleStateMachine _stateMachine;
        
        private const string Team = "Team";

        public Team TeamMove => _teamMove;
        
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
                    team.CanFight = true;
                    return;
                }
                
                team.CanFight = false;
            }
        }

        private void SetMove(Team team)
        {
            _teamMove = team;
            _teamMove.CanFight = true;
            
            if (team.TryGetComponent(out AutoBattlerComponent component))
            {
                component.Battle();
            }
        }

        public void ChangeTeamMove()
        {
            for (var i = 0; i < _teams.Length; i++)
            {
                if (_teamMove == _teams[i])
                {
                    if (i == _teams.Length - 1)
                    {
                        SetMove(_teams[0]);
                    }
                    else
                    {
                        SetMove(_teams[i + 1]);
                        Debug.Log($"Ход команды: {_teams[i + 1].gameObject.name}");
                    }
                    return;
                }
            }
        }
    }
}