using CodeBase.Creatures;
using TMPro.Examples;
using UnityEngine;

namespace CodeBase.Battle
{
    public class Team : MonoBehaviour, IInitializable
    {
        [SerializeField] private string _id;
        [SerializeField] private Creature[] _creatures;
        

        
        private bool _canFight;
        
        public string Id { get => _id; set => _id = value; }
        public bool CanFight { get => _canFight; set => _canFight = value; }

        private Creature _activeCreature;
        
        public void Initialize()
        {
            _creatures = GetComponentsInChildren<Creature>();
        }
    }
}