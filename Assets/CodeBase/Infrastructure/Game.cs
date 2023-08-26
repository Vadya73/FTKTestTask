using CodeBase.Logic;
using CodeBase.StateMachine.Game;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMAchine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMAchine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}