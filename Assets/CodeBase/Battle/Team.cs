using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Creatures;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Battle
{
    public class Team : MonoBehaviour, IInitializable
    {
        [SerializeField] private string _id;
        [SerializeField] private List<Creature> _creatures = new();
        [SerializeField] private bool _canFight;
        

        public string Id { get => _id; set => _id = value; }

        public bool CanFight
        {
            get => _canFight;
            set
            {
                _canFight = value;
                foreach (var creature in _creatures)
                    creature.CanAttack = true;
            }
        }

        public void Initialize()
        {
            _creatures.Clear();
            _creatures.AddRange(GetComponentsInChildren<Creature>());
        }

        public void CheckTeamAlive()
        {
            if (_creatures.Count <= 0)
            {
                Debug.Log("End Game");
                SceneManager.LoadScene(0);
            }
        }

        public void RemoveCreatureFromTeam(Creature creature)
        {
            if (creature != null && _creatures.Contains(creature))
            {
                _creatures.Remove(creature);
            }
            CheckTeamAlive();
        }
        
        public void AddCreatureToTeam(Creature creature)
        {
            if (creature != null)
            {
                _creatures.Add(creature);
            }
        }
    }
}