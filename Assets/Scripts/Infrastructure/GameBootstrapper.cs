using Configs;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain LoadingCurtain;
        public BallConfig ballConfig;
        
        private Game _game;

        private void Awake()
        {
            CreateNewGameState();
            DontDestroyOnLoad(this);
        }

        private void CreateNewGameState()
        {
            _game = new Game(this,this, LoadingCurtain, ballConfig);
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}