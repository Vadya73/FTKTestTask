using CodeBase.Logic;
using CodeBase.StateMachine.Game;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootsTrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain Curtain;
        
        private Game _game;
        private void Awake()
        {
            _game = new Game(this, Curtain);
            _game.StateMAchine.Enter<BootstrapState>();
            
            
            DontDestroyOnLoad(this);
        }
    }
}