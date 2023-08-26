using System.Collections;
using UnityEditor.Compilation;
using UnityEngine;

namespace CodeBase.Creatures
{
    public class Creature : MonoBehaviour, IInitializable
    {
        [SerializeField] protected int _maxHealth;
        [SerializeField] protected int _currentHealth;
        [SerializeField] protected int _armor;
        [SerializeField] protected int _baseDamage;
        [SerializeField] protected int _vampirismValue;
        [SerializeField] protected float _moveSpeed;
        [SerializeField] private Creature[] _enemies;

        protected Vector3 _defaultPosition;
        protected bool _isMoving;

        protected Animator _animator;
        
        private static readonly int AttackAnimationKey = Animator.StringToHash("attack");


        public int CurrentHealth => _currentHealth;
        public int Armor => _armor;

        protected void Attack()
        {
            _animator.SetTrigger(AttackAnimationKey);
            MoveToDefaultPositiom();
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
            Creature target = _enemies[0];
            var lowHealth = target.CurrentHealth;

            foreach (var enemys in _enemies)
            {
                if (enemys.CurrentHealth < lowHealth)
                {
                    lowHealth = enemys.CurrentHealth;
                    target = enemys;
                }
            }
            enemy = target;
        }

        public void MoveToTarget()
        {
            var pointNearTarget = new Vector3(0,0,1);
            CheckAllEnemies(out var enemyToMove);
            var pointToMove = enemyToMove.GetComponent<Transform>().position + pointNearTarget;
            
            StartCoroutine(Move(pointToMove));
            Attack();
        }

        protected IEnumerator Move(Vector3 positionToMove)
        {
            _isMoving = true;
            while (Vector3.Distance(transform.position, positionToMove) > 0.09f)
            {
                transform.position = Vector3.MoveTowards(transform.position, positionToMove, _moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = positionToMove;
            _isMoving = false;
        }
        
        public void MoveToDefaultPositiom()
        {
            StartCoroutine(Move(_defaultPosition));
        }

        public virtual void Initialize()
        {
            
        }
    }
}