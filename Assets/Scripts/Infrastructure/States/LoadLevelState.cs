using Configs;
using Infrastructure.Factory;
using Logic;
using Player;
using Services.Input;
using Trajectory;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPoint = "InitialPoint";
        private const string GameField = "GameField";
        
        private readonly GameStateMachine m_StateMachine;
        private readonly GameBootstrapper m_GameBootstrapper;
        private readonly SceneLoader m_SceneLoader;
        private readonly LoadingCurtain m_LoadingCurtain;
        private readonly IGameFactory m_GameFactory;
        private readonly BallConfig m_BallConfig;

        public LoadLevelState(GameStateMachine stateMachine, GameBootstrapper gameBootstrapper, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, BallConfig ballConfig)
        {
            m_StateMachine = stateMachine;
            m_GameBootstrapper = gameBootstrapper;
            m_SceneLoader = sceneLoader;
            m_LoadingCurtain = loadingCurtain;
            m_GameFactory = gameFactory;
            m_BallConfig = ballConfig;
        }

        public void Enter(string sceneName)
        {
            m_LoadingCurtain.Show();
            m_SceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            m_LoadingCurtain.Hide();
        }
        
        private void OnLoaded()
        {
            Spawn();

            SetupCamera();
            
            m_StateMachine.Enter<GameLoopState>();
        }

        private void Spawn()
        {
            var trajectory = m_GameFactory.CreateTrajectoryController().GetComponent<TrajectoryController>();
            var player = m_GameFactory.CreatePlayer(GameObject.FindWithTag(InitialPoint)).GetComponent<Player.Player>();
            var playerCanvas = m_GameFactory.CreatePlayerCanvas().GetComponent<PlayerCanvas>();
            
            var newPlayerState = new PlayerState(m_StateMachine);
            player.Init(newPlayerState);

            player.PushPlayerBall.Init(trajectory.Direction, trajectory.Trajectory, trajectory.HitPoint,
                player.GetComponent<Rigidbody2D>(), playerCanvas);

            SpawnBalls();   
        }

        private void SpawnBalls()
        {
            var ballManager = m_GameFactory.CreateBallManager().GetComponent<BallManager>();
            ballManager.Init(m_StateMachine);
            
            for(var i = 0; i < m_BallConfig.BallSpawnPoints.Count; i++)
            {
                var ball = m_GameFactory.CreateBall(m_BallConfig.BallSpawnPoints[i]).GetComponent<Ball>();
                ball.Init(ballManager);
                ball.GetComponent<SpriteRenderer>().sprite = m_BallConfig.BallSprites[i];
                ballManager.AddBallToList(ball);
            }
        }

        private void SetupCamera()
        {
            var field = GameObject.FindWithTag(GameField).GetComponent<SpriteRenderer>();
            Camera.main!.orthographicSize = field.bounds.size.x * Screen.height / Screen.width * 0.5f;
        }
    }
}