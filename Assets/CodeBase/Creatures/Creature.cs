using System;
using System.Collections;
using CodeBase.Battle;
using CodeBase.Interfaces;
using UnityEngine;

namespace CodeBase.Creatures
{
    public class Creature : MonoBehaviour, IInitializable, IDamagable
    {
        [SerializeField] protected int _maxHealth;
        [SerializeField] protected int _currentHealth;
        [Range(0, 100)] [SerializeField] protected float _armor;
        [SerializeField] protected int _baseDamage;
        [SerializeField] protected int _currentDamage;
        [SerializeField] protected int _vampirismPercent;
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected GameObject _selectedEffect;

        protected int _armorDestruction = 0;
        protected int _vampiricDebuffEnemy = 0;
        protected bool _canAttack = true;
        protected bool _canBuff = true;

        protected Creature _target = null;
        protected Mover _mover;
        protected Team _currentTeam;
        protected MeshRenderer _meshRenderer;

        public event Action OnAttackComplete;
        public event Action OnHealthChanged;

        public int CurrentHealth => _currentHealth;

        public bool CanBuff
        {
            get => _canBuff;
            set => _canBuff = value;
        }

        public int CurrentDamage
        {
            get => _currentDamage;
            set => _currentDamage = value;
        }

        public int VampirismPercent
        {
            get => _vampirismPercent;
            set => _vampirismPercent = Mathf.Clamp(value, 0, 100);
        }

        public int VampiricDebuffEnemy
        {
            get => _vampiricDebuffEnemy;
            set => _vampiricDebuffEnemy = value;
        }

        public int ArmorDestruction
        {
            get => _armorDestruction;
            set => _armorDestruction = value;
        }

        public float Armor
        {
            get => _armor;
            set => _armor = Mathf.Clamp(value, 0, 100);
        }

        public bool CanAttack
        {
            get => _canAttack;
            set => _canAttack = value;
        }

        public float MoveSpeed => _moveSpeed;

        public void Initialize()
        {
            _currentTeam = GetComponentInParent<Team>();
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            if (TryGetComponent<Mover>(out Mover mover))
            {
                _mover = mover;
                _mover.OnMoveComplete += Attack;
            }

            _currentHealth = _maxHealth;
            _currentDamage = _baseDamage;
        }

        public void ApplyDamage(int value)
        {
            if (value >= _currentHealth)
                Death();

            var damageMultiplier = 1f - (_armor / 100f);
            var actualDamage = Mathf.RoundToInt(value * damageMultiplier);
            var originalColor = _meshRenderer.material.color;

            _currentHealth -= actualDamage;
            OnHealthChanged?.Invoke();

            _meshRenderer.material.color = Color.red;
            StartCoroutine(RestoreColor(originalColor, 0.5f));

            Debug.Log($"Apply Damage, Current health: {_currentHealth}, Name: {gameObject.name}");
        }


        public void Deselect()
        {
            if (_selectedEffect != null)
                _selectedEffect.SetActive(false);
        }

        public void Select()
        {
            if (_selectedEffect != null)
                _selectedEffect.SetActive(true);
        }

        public void PrepateToAttack(Creature target)
        {
            if (_canAttack && _currentTeam.CanFight && target != null)
            {
                _target = target;
                _mover?.MoveToTarget(target);
            }
            else
            {
                _mover?.MoveToDefaultPosition();
            }
        }

        public void Attack()
        {
            if (_target == null) return;

            DealDamageToTarget();
            ApplyVampirism();

            ResetAttackState();
        }

        private void DealDamageToTarget()
        {
            int targetHealthBeforeAttack = _target.CurrentHealth;
            _target.ApplyDamage(_currentDamage);

            int damageDealt = targetHealthBeforeAttack - _target.CurrentHealth;
            int vampirismAmount = Mathf.RoundToInt(damageDealt * (_vampirismPercent / 100f));

            _currentHealth += vampirismAmount;
        }

        private void ApplyVampirism()
        {
            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
        }

        private void ResetAttackState()
        {
            _canAttack = false;
            _target = null;
            _mover?.MoveToDefaultPosition();
            OnAttackComplete?.Invoke();
        }

        protected void Death()
        {
            _currentHealth = 0;
            Debug.Log($"Death {gameObject.name}");
            GetComponentInParent<Team>().RemoveCreatureFromTeam(this);
            Destroy(gameObject);
        }

        private IEnumerator RestoreColor(Color color, float time)
        {
            yield return new WaitForSeconds(time);
            _meshRenderer.material.color = color;
        }

        private void OnDestroy()
        {
            if (_mover != null)
                _mover.OnMoveComplete -= Attack;
        }
    }
}