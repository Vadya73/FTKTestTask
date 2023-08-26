using CodeBase.Battle;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class BattleLevelBootstrapper : MonoBehaviour, IInitializable
    {
        private const string BattlerPath = "Battler/Battler";
        private const string InitialpointTag = "PlayerInitialPoint";
        private const string EnenmyInitialPoint = "EnemyInitialPoint";
        private const string PlayerTeamPath = "Player/PlayerTeam";
        private const string EnemyTeamPath = "Enemies/EnemyTeam";

        public void Initialize()
        {
            GameObject playerInitialPoint = GameObject.FindWithTag(InitialpointTag);
            GameObject enenmyInitialPoint = GameObject.FindWithTag(EnenmyInitialPoint);
            GameObject battler = InstantiateExtensions.Instantiate(BattlerPath, new Vector3(0, 0, 0));
            GameObject playerTeam = InstantiateExtensions.Instantiate(PlayerTeamPath, at: playerInitialPoint.transform.position);
            GameObject enemyTeam = InstantiateExtensions.Instantiate(EnemyTeamPath, at: enenmyInitialPoint.transform.position);
            
            playerTeam.GetComponent<TeamGenerator>().Initialize();
            var playerT = playerTeam.GetComponent<Team>();
            playerT.Initialize();
            playerT.Id = "Player";
            
            enemyTeam.GetComponent<TeamGenerator>().Initialize();
            var enemyT = enemyTeam.GetComponent<Team>();
            enemyT.Initialize();
            enemyT.Id = "Enemy";
            
            battler.GetComponent<Battler>().Initialize();
        }
    }
}