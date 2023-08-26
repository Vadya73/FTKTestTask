using CodeBase.Creatures;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Battle
{
    public class TeamGenerator : MonoBehaviour, IInitializable
    {
        [Range(1,3)] [SerializeField] private int _creaturesCount = 1;
        [SerializeField] private bool _randomCount = false;
        [SerializeField] private GameObject[] _creatureToGenerate;
        
        [SerializeField] private Transform[] _twoCreaturesPos;
        [SerializeField] private Transform[] _threeCreaturesPos;

        public void Initialize()
        {
            GenerateTeam();
            Debug.Log("TeamsInit");
        }

        private void GenerateTeam()
        {
            if (_randomCount)
            {
                _creaturesCount = Random.Range(1, 4);
                Debug.Log(_creaturesCount);
            }
            
            switch (_creaturesCount)
            {
                case 1:
                    InstantiateRandomCreature(_threeCreaturesPos[0]);
                    break;
                case 2:
                    for (int i = 0; i < 2; i++)
                        InstantiateRandomCreature(_twoCreaturesPos[i]);
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                        InstantiateRandomCreature(_threeCreaturesPos[i]);
                    break;
            }
        }

        private void InstantiateRandomCreature(Transform pos)
        {
            ChooseRandomCreature(out var creature);
            var instObj = Instantiate(creature, pos.position, pos.rotation);
            instObj.transform.SetParent(transform);
        }

        private void ChooseRandomCreature(out GameObject creature)
        {
            var randomN = Random.Range(0, _creatureToGenerate.Length);
            creature = _creatureToGenerate[randomN];
        }
    }
}