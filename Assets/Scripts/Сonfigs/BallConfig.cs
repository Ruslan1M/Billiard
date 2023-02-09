using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "BallConfig", menuName = "Configs/BallConfig", order = 0)]
    public class BallConfig : ScriptableObject
    {
        public List<Vector3> BallSpawnPoints;
        public List<Sprite> BallSprites;
    }
}