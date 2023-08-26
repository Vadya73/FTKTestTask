using CodeBase.Creatures;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Battler
{
    public class TeamGenerator : MonoBehaviour, IInitializable
    {
        [Range(1,3)] [SerializeField] private int _creaturesCount = 1;
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
            switch (_creaturesCount)
            {
                case 1:
                    InstantiateRandomCreature(transform);
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
            var instantObj = PrefabUtility.InstantiatePrefab(creature) as GameObject;
            if (instantObj != null)
            {
                instantObj.transform.position = pos.position;
                instantObj.transform.rotation = pos.rotation;
                instantObj.transform.SetParent(transform);
            }
        }

        private void ChooseRandomCreature(out GameObject creature)
        {
            var randomN = Random.Range(0, _creatureToGenerate.Length);
            creature = _creatureToGenerate[randomN];
        }
    }
}