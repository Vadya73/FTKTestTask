using CodeBase.Battler;
using CodeBase.Infrastructure;
using CodeBase.Logic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.StateMachine.Game
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        private const string InitialpointTag = "PlayerInitialPoint";
        private const string EnenmyInitialPoint = "EnemyInitialPoint";
        private const string PlayerTeamPath = "Player/PlayerTeam";
        private const string EnemyTeamPath = "Enemies/EnemyTeam";
        private const string HudPath = "Hud/Hud";

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => _curtain.Hide();

        private void OnLoaded()
        {
            GameObject playerInitialPoint = GameObject.FindWithTag(InitialpointTag);
            GameObject enenmyInitialPoint = GameObject.FindWithTag(EnenmyInitialPoint);
            GameObject playerTeam = Instantiate(PlayerTeamPath, at: playerInitialPoint.transform.position);
            GameObject enemyTeam = Instantiate(EnemyTeamPath, at: enenmyInitialPoint.transform.position);
            
            playerTeam.GetComponent<TeamGenerator>().Initialize();
            enemyTeam.GetComponent<TeamGenerator>().Initialize();

            Instantiate(HudPath);
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }        
        
        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }


    }
}