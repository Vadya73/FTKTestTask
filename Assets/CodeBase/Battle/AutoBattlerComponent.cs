using System.Collections;
using System.Collections.Generic;
using CodeBase.Creatures;
using UnityEngine;

namespace CodeBase.Battle
{
    public class AutoBattlerComponent : MonoBehaviour,IInitializableComponents <Battler>
    {
        private Battler _battler;
        private List<Creature> _creatures;
        private List<Creature> _enemies;
        
        private const string Player = "Player";
        private const float WaitSeconds = 4;

        public void InitializeComponent(Battler battler)
        {
            _battler = battler.GetComponent<Battler>();

            _creatures = new List<Creature>(GetComponentsInChildren<Creature>());
            var players = GameObject.FindGameObjectsWithTag(Player);

            _enemies = new List<Creature>(players.Length);
            
            for (int i = 0; i < players.Length; i++)
                _enemies.Add(players[i].GetComponent<Creature>());
        }
        
        public void Battle()
        {
            StartCoroutine(PerformBattle());
        }

        private IEnumerator PerformBattle()
        {
            foreach (var creature in _creatures)
            {
                var enemy = FindTheWeakestEnemy();
                
                if (enemy != null && enemy.CurrentHealth > 0)
                {
                    creature.PrepateToAttack(enemy);
                    yield return new WaitForSeconds(WaitSeconds);
                }
            }
    
            _battler.ChangeTeamMove();
        }
        
        private Creature FindTheWeakestEnemy()
        {
            Creature weakestEnemy = null;
    
            foreach (var enemy in _enemies)
            {
                if (enemy != null && enemy.CurrentHealth > 0 && (weakestEnemy == null || weakestEnemy.CurrentHealth > enemy.CurrentHealth))
                {
                    weakestEnemy = enemy;
                }
            }
    
            return weakestEnemy;
        }


    }
}