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
            GameBootsTrapper existingTrapper = FindObjectOfType<GameBootsTrapper>();
            if (existingTrapper != null && existingTrapper != this)
            {
                Destroy(existingTrapper.gameObject);
            }

            LoadingCurtain existingCurtain = FindObjectOfType<LoadingCurtain>();
            if (existingCurtain != null && existingCurtain != Curtain)
            {
                Destroy(existingCurtain.gameObject);
            }

            if (Curtain != null)
            {
                Curtain.gameObject.SetActive(true);
            }

            _game = new Game(this, Curtain);
            _game.StateMAchine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}