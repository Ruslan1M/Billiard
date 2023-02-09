using Configs;
using Infrastructure.Services;
using Infrastructure.States;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(GameBootstrapper gameBootstrapper,ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, BallConfig ballConfig)
        {
            StateMachine = new GameStateMachine(gameBootstrapper,new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container, ballConfig);
        }
        
    }
}