using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider m_Assets;

        public GameFactory(IAssetProvider assets)
        {
            m_Assets = assets;
        }

        public GameObject CreateBallManager() => m_Assets.Instantiate(AssetPath.BallManagerPath);
        public GameObject CreatePlayerCanvas() => m_Assets.Instantiate(AssetPath.PlayerCanvasPath);
        public GameObject CreatePlayer(GameObject at) => m_Assets.Instantiate(AssetPath.PlayerPath, at.transform.position);
        public GameObject CreateTrajectoryController() => m_Assets.Instantiate(AssetPath.TrajectoryPath);
        public GameObject CreateBall(Vector3 at) => m_Assets.Instantiate(AssetPath.BallPath, at);
    }
}