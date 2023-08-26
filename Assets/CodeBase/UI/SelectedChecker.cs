using CodeBase.Creatures;
using UnityEngine;

namespace CodeBase.UI
{
    public class SelectedChecker : MonoBehaviour
    {
        private Creature _selectedHero;
        private Creature _selectedEnemy;
        

        public void TryAttack()
        {
            Attack(_selectedHero, _selectedEnemy);
        }
        
        public void TryBuff()
        {
            _selectedHero.Buff();
        }
        
        private void Attack(Creature attacker, Creature target)
        {
            Debug.Log(attacker.name + " attacks " + target.name);
            _selectedHero.MoveToTarget(target);
        }

        public void SelectHero(Creature hero)
        {
            _selectedHero = hero;
            _selectedHero.Select();
        }

        public void SelectEnemy(Creature enemy)
        {
            _selectedEnemy = enemy;
            _selectedEnemy.Select();
        }
    }
}