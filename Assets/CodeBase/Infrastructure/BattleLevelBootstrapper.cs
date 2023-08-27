using CodeBase.Battle;
using CodeBase.Creatures;
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
            Time.timeScale = 2;

            GameObject playerInitialPoint = GameObject.FindWithTag(InitialpointTag);
            GameObject enenmyInitialPoint = GameObject.FindWithTag(EnenmyInitialPoint);
            GameObject battler = InstantiateExtensions.Instantiate(BattlerPath, new Vector3(0, 0, 0));
            GameObject playerTeam = InstantiateExtensions.Instantiate(PlayerTeamPath, at: playerInitialPoint.transform.position);
            GameObject enemyTeam = InstantiateExtensions.Instantiate(EnemyTeamPath, at: enenmyInitialPoint.transform.position);
            
            playerTeam.GetComponent<TeamGenerator>().Initialize();
            var playerT = playerTeam.GetComponent<Team>();
            var creaturesTeam = playerT.GetComponentsInChildren<IInitializable>();

            if (playerTeam.TryGetComponent(out AutoBattlerComponent component))
                component.InitializeComponent(battler.GetComponent<Battler>());

            foreach (var creature in creaturesTeam)
                creature.Initialize();

            playerT.Initialize();
            playerT.Id = "Player";

            
            enemyTeam.GetComponent<TeamGenerator>().Initialize();
            var enemyT = enemyTeam.GetComponent<Team>();
            var creaturesEnemyTeam = enemyT.GetComponentsInChildren<IInitializable>();
            
            if (enemyTeam.TryGetComponent(out AutoBattlerComponent enenmyComponent))
                enenmyComponent.InitializeComponent(battler.GetComponent<Battler>());

            foreach (var creature in creaturesEnemyTeam)
                creature.Initialize();
            
            enemyT.Initialize();
            enemyT.Id = "Enemy";
            
            battler.GetComponent<Battler>().Initialize();
        }
    }
}