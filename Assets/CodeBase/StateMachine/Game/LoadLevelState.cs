using CodeBase.Infrastructure;
using CodeBase.Input;
using CodeBase.Logic;
using CodeBase.UI;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.StateMachine.Game
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        
        private const string HudPath = "Hud/Hud";
        private const string Levelbootstrapper = "LevelBootstrapper";

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
            var hud = InstantiateExtensions.Instantiate(HudPath).GetComponent<SelectedChecker>();
            GameObject levelBootstrapper = GameObject.FindWithTag(Levelbootstrapper);
            levelBootstrapper.GetComponent<IInitializable>().Initialize();
            levelBootstrapper.GetComponent<PlayerInput>().InitializeComponent(hud);
            hud.GetComponent<SelectedChecker>().Initialize();
            
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}