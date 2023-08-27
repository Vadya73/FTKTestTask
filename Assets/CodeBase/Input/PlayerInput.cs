using CodeBase.Creatures;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Input
{
    public class PlayerInput : MonoBehaviour, IInitializableComponents<SelectedChecker>
    {
        [SerializeField] private SelectedChecker _uiChecker;
        private Creature _oldSelectHero;
        private Creature _oldSelectEnemy;

        private const string Player = "Player";
        private const string Enemy = "Enemy";
        
        public void InitializeComponent(SelectedChecker component)
        {
            _uiChecker = component;
            component.GetComponentInChildren<CreatureUI>().Initialize();
        }
        
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.TryGetComponent(out Creature rayedCreature) && hit.collider.CompareTag(Player))
                {
                    // ReSharper disable once Unity.NoNullPropagation
                    _oldSelectHero?.Deselect();
                    
                    _uiChecker.SelectHero(rayedCreature);
                    _oldSelectHero = rayedCreature;
                }
                
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.TryGetComponent(out Creature rayed) && hit.collider.CompareTag(Enemy))
                {
                    // ReSharper disable once Unity.NoNullPropagation
                    _oldSelectEnemy?.Deselect();
                    
                    _uiChecker.SelectEnemy(rayed);
                    _oldSelectEnemy = rayed;
                }
            }
        }
    }
}