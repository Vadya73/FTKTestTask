using CodeBase.Creatures;
using UnityEngine;

namespace CodeBase.BuffSystem
{
    public abstract class Buffs : MonoBehaviour, IInitializable
    {
        protected Creature _creature;
        
        protected string _name = "Buff";
        protected bool _active = true;
        protected int _durationRounds = 1;
        protected float value;

        public abstract void Buff();
        public virtual void Initialize()
        {
            _creature = GetComponent<Creature>();
        }
    }
}