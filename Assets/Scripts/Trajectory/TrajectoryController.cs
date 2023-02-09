using UnityEngine;

namespace Trajectory
{
    public class TrajectoryController : MonoBehaviour
    {
        [SerializeField] private LineRenderer direction;
        [SerializeField] private LineRenderer trajectory;
        [SerializeField] private SpriteRenderer hitPoint;

        public LineRenderer Direction => direction;
        public LineRenderer Trajectory => trajectory;
        public SpriteRenderer HitPoint => hitPoint;
    }
}