using CodeBase.Infrastructure;

namespace CodeBase.StateMachine.Game
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        
        private const string Initial = "Initial";

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>("Level");

        private void RegisterServices()
        {
            
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }
}