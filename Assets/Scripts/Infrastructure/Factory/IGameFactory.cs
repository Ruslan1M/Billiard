using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject at);

        GameObject CreateTrajectoryController();

        GameObject CreateBall(Vector3 at);

        GameObject CreateBallManager();

        GameObject CreatePlayerCanvas();
    }
}