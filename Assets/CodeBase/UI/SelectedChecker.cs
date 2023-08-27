using CodeBase.Battle;
using CodeBase.BuffSystem;
using CodeBase.Creatures;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.UI
{
    //It's not a SelectedChecker It's some kind of monster now
    public class SelectedChecker : MonoBehaviour, IInitializable
    {
        private const string PlayerId = "Player";
        private Creature _selectedHero;
        private Creature _selectedEnemy;
        private Battler _battler;
        
        public void Initialize()
        {
            _battler = GameObject.FindWithTag("Battler").GetComponent<Battler>();
        }
        
        public void TryAttack()
        {
            Attack(_selectedHero, _selectedEnemy);
        }
        
        public void TryBuff()
        {
            if (_selectedHero.CanBuff)
            {
                _selectedHero.GetComponent<Buffs>().Buff();
                _selectedHero.CanBuff = false;
            }
        }
        
        private void Attack(Creature attacker, Creature target)
        {
            if (attacker != null && target != null)
            {
                Debug.Log(attacker.name + " attacks " + target.name);
                attacker.PrepateToAttack(target);
            }
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

        public void EndMove()
        {
            if (_battler.TeamMove.Id == PlayerId)
            {
                _battler.ChangeTeamMove();
            }
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}