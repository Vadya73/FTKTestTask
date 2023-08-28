using System;
using System.Linq;
using CodeBase.Creatures;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class CreatureUI : MonoBehaviour, IInitializable
    {
        [SerializeField] private Creature[] _players;
        [SerializeField] private Creature[] _enemies;
        [SerializeField] private Transform[] _playersTransform;
        [SerializeField] private Transform[] _enemiesTransform;
        [SerializeField] private GameObject _healthTextPrefab;
        private TextMeshProUGUI[] _healthPlayersTexts;
        private TextMeshProUGUI[] _healthEnemiesTexts;

        private const string PlayerTag = "Player";
        private const string EnenmyTag = "Enemy";

        public void Initialize()
        {
            _players = GameObject.FindGameObjectsWithTag(PlayerTag)
                .Select(go => go.GetComponent<Creature>())
                .ToArray();
            
            _enemies = GameObject.FindGameObjectsWithTag(EnenmyTag)
                .Select(go => go.GetComponent<Creature>())
                .ToArray();
            
            _healthPlayersTexts = new TextMeshProUGUI[_players.Length];
            _healthEnemiesTexts = new TextMeshProUGUI[_enemies.Length];
            
            for (int i = 0; i < _players.Length; i++)
            {
                GameObject healthTextObject = Instantiate(_healthTextPrefab, _playersTransform[i]);
                _healthPlayersTexts[i] = healthTextObject.GetComponent<TextMeshProUGUI>();
            }            
            
            for (int i = 0; i < _enemies.Length; i++)
            {
                GameObject healthTextObject = Instantiate(_healthTextPrefab, _enemiesTransform[i]);
                _healthEnemiesTexts[i] = healthTextObject.GetComponent<TextMeshProUGUI>();
            }

            UpdateHealthUI();
            
            foreach (var creature in _players)
            {
                creature.OnHealthChanged += UpdateHealthUI;
            }            
            foreach (var creature in _enemies)
            {
                creature.OnHealthChanged += UpdateHealthUI;
            }
        }

        private void UpdateHealthUI()
        {
            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i].CurrentHealth <= 0)
                {
                    if (_healthPlayersTexts[i] != null)
                    {
                        Destroy(_healthPlayersTexts[i].gameObject);
                        _healthPlayersTexts[i] = null;
                    }
                    continue;
                }

                if (_healthPlayersTexts[i] != null)
                {
                    _healthPlayersTexts[i].text = $"Health: {_players[i].CurrentHealth}";
                }
            }

            for (int i = 0; i < _enemies.Length; i++)
            {
                if (_enemies[i].CurrentHealth <= 0)
                {
                    if (_healthEnemiesTexts[i] != null)
                    {
                        Destroy(_healthEnemiesTexts[i].gameObject);
                        _healthEnemiesTexts[i] = null;
                    }
                    continue;
                }

                if (_healthEnemiesTexts[i] != null)
                {
                    _healthEnemiesTexts[i].text = $"Health: {_enemies[i].CurrentHealth}";
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var creature in _players)
            {
                creature.OnHealthChanged -= UpdateHealthUI;
            }
            foreach (var creature in _enemies)
            {
                creature.OnHealthChanged -= UpdateHealthUI;
            }
        }
    }
}