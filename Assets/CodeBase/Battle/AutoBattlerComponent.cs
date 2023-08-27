﻿using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Creatures;
using UnityEngine;

namespace CodeBase.Battle
{
    public class AutoBattlerComponent : MonoBehaviour, IInitializableComponents<Battler>
    {
        private Battler _battler;
        private List<Creature> _creatures;
        private List<Creature> _enemies;

        private const string Player = "Player";
        private const float WaitSeconds = 4;
        private bool _isPerformingBattle = false;

        public void InitializeComponent(Battler battler)
        {
            _battler = battler.GetComponent<Battler>();

            _creatures = new List<Creature>(GetComponentsInChildren<Creature>());
            var players = GameObject.FindGameObjectsWithTag(Player);

            _enemies = new List<Creature>(players.Length);

            for (int i = 0; i < players.Length; i++)
                _enemies.Add(players[i].GetComponent<Creature>());

            foreach (var creature in _creatures)
            {
                creature.OnAttackComplete += HandleAttackComplete;
            }
        }

        public void Battle()
        {
            StartCoroutine(PerformBattle());
        }

        private IEnumerator PerformBattle()
        {
            int attackedCreatureCount = 0;

            foreach (var creature in _creatures)
            {
                var enemy = FindTheWeakestEnemy();

                if (enemy != null && enemy.CurrentHealth > 0)
                {
                    creature.OnAttackComplete += () => 
                    {
                        attackedCreatureCount++;

                        if (attackedCreatureCount == _creatures.Count)
                            _battler.ChangeTeamMove();
                    };

                    creature.PrepateToAttack(enemy);
                    yield return new WaitUntil(() => !creature.CanAttack);
                }
            }
        }

        private readonly object _performingBattleLock = new object();

        private void HandleAttackComplete()
        {
            lock (_performingBattleLock)
            {
                if (!_isPerformingBattle)
                    return;

                foreach (var creature in _creatures)
                {
                    if (!creature.CanAttack)
                        continue;

                    Creature targetEnemy = UnityEngine.Random.value < 0.5f ? TakeRandomEnemy() : FindTheWeakestEnemy(); //why don't work!!!??. уже час ночи я хочу спать
                    if (targetEnemy != null)
                    {
                        creature.PrepateToAttack(targetEnemy);
                    }
                }
            }
        }

        
        private Creature FindTheWeakestEnemy()
        {
            Creature weakestEnemy = null;
            foreach (var enemy in _enemies)
            {
                if (enemy != null && enemy.CurrentHealth > 0)
                {
                    weakestEnemy = enemy;
                    break;
                }
            }

            foreach (var enemy in _enemies)
            {
                if (enemy != null && enemy.CurrentHealth > 0 && weakestEnemy.CurrentHealth > enemy.CurrentHealth)
                {
                    weakestEnemy = enemy;
                }
            }
            return weakestEnemy;
        }

        private Creature TakeRandomEnemy()
        {
            List<Creature> validEnemies = _enemies.FindAll(enemy => enemy != null && enemy.CurrentHealth > 0);

            if (validEnemies.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validEnemies.Count);
                return validEnemies[randomIndex];
            }

            return null;
        }

        private void OnDestroy()
        {
            foreach (var creature in _creatures)
            {
                creature.OnAttackComplete -= HandleAttackComplete;
            }
        }
    }
}