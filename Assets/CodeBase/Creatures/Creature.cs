using System.Collections;
using CodeBase.StateMachine.Creature;
using UnityEngine;

namespace CodeBase.Creatures
{
    public class Creature : MonoBehaviour, IInitializable
    {
        [SerializeField] protected int _maxHealth;
        [SerializeField] protected int _currentHealth;
        [Range(0,100)] [SerializeField] protected int _armor;
        [SerializeField] protected int _baseDamage;
        [SerializeField] protected int _currentDamage;
        [SerializeField] protected int _vampirismPercent;
        [SerializeField] protected float _moveSpeed;

        [SerializeField] protected GameObject _selectedEffect;

        protected Vector3 _defaultPosition;
        protected bool _isMoving;
        protected bool _canAttack;

        
        protected CreatureStateMachine _stateMachine;

        public int CurrentHealth => _currentHealth;
        public int Armor => _armor;
        public bool CanFight
        {
            get => _canAttack;
            set => _canAttack = value;
        }

        public virtual void Initialize()
        {
            _stateMachine = new CreatureStateMachine();
            
            _currentHealth = _maxHealth;
            _defaultPosition = transform.position;
        }
        
        public void Buff()
        {
            Debug.Log($"Buff {gameObject.name}");
        }

        protected void TakeDamage(int value)
        {
            if (value >= _currentHealth)
            {
                Death();
            }
        }

        protected void Death()
        {
            _currentHealth = 0;
            Debug.Log("Death");
        }

        protected void CheckAllEnemies(out Creature enemy)
        {
            var enemiesObj = GameObject.FindGameObjectsWithTag("Enemy");
            Creature[] enemies = new Creature[enemiesObj.Length];
            for (int i = 0; i < enemiesObj.Length; i++)
            {
                enemies[i] = enemiesObj[i].GetComponent<Creature>();
            }
            
            Creature target = enemies[0];
            var lowHealth = target.CurrentHealth;
        
            foreach (var enemys in enemies)
            {
                if (enemys.CurrentHealth < lowHealth)
                {
                    lowHealth = enemys.CurrentHealth;
                    target = enemys;
                }
            }
            enemy = target;
        }

        public void MoveToTarget(Creature target)
        {
            var pointToMove = target.transform.position + new Vector3(0,0,-1);
            
            StartCoroutine(Move(pointToMove));
            // TODO Сделать ожидание окончания передвижения, атаковать, вернуться
            Attack();
            
            MoveToDefaultPositiom();
        }
        
        public void MoveToDefaultPositiom()
        {
            StartCoroutine(Move(_defaultPosition));
        }
        protected IEnumerator Move(Vector3 positionToMove)
        {
            _isMoving = true;
            while (Vector3.Distance(transform.position, positionToMove) > 0.09f)
            {
                Debug.Log("Move!");
                transform.position = Vector3.MoveTowards(transform.position, positionToMove, _moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = positionToMove;
            _isMoving = false;
        }

        private void Attack()
        {
            Debug.Log("Punch");
            StartCoroutine(Move(_defaultPosition));
        }

        public void Deselect()
        {
            _selectedEffect.SetActive(false);
        }

        public void Select()
        {
            _selectedEffect.SetActive(true);
        }
    }
}