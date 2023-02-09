using UnityEngine;

namespace Player
{
    public class PushPlayerBall : MonoBehaviour
    {
        private const string Wall = "Wall";
    
        private LineRenderer m_DirectionalLine;
        private LineRenderer m_TrajectoryLine;
        private SpriteRenderer m_HitCircle;
        private Rigidbody2D m_Rigidbody2D;
        private PlayerCanvas m_PlayerCanvas;
        private Camera m_Camera;
    
        private Vector2 m_FingerPosition = new Vector2();
        private Vector2 m_Direction = new Vector2();
        
        private const float MaxDirectionValue = 10f;

        public void Init(LineRenderer directionalLine, LineRenderer trajectoryLine, SpriteRenderer hitCircle, Rigidbody2D rb2D, PlayerCanvas playerCanvas)
        {
            m_PlayerCanvas = playerCanvas;
            m_DirectionalLine = directionalLine;
            m_TrajectoryLine = trajectoryLine;
            m_HitCircle = hitCircle;
            m_Rigidbody2D = rb2D;
        
            m_Camera = Camera.main;
        }

        public void Push()
        {
            m_DirectionalLine.enabled = false;
            m_HitCircle.enabled = false;
            m_TrajectoryLine.enabled = false;
        
            m_Rigidbody2D.AddForce(-m_Direction * 2.5f, ForceMode2D.Impulse);
        }

        public void SetLine(Vector3 t)
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = m_Camera!.ScreenToWorldPoint(screenPosition);
        
            m_FingerPosition = worldPosition;
            m_Direction = m_FingerPosition - new Vector2(t.x, t.y);
            m_PlayerCanvas.DirectionImage.fillAmount = Mathf.Abs(m_Direction.x) / MaxDirectionValue;

            RaycastHit2D hit = Physics2D.Raycast(t, -m_Direction.normalized);
        
            var position = hit.collider.transform.position;
            Vector2 hitTrajectory = (new Vector2(position.x, position.y) - hit.point).normalized;
            Vector2 bounceTrajectory = Vector2.Perpendicular(hitTrajectory);

            Vector3 startPosition = new Vector3(t.x - m_Direction.normalized.x * 0.5f, t.y - m_Direction.normalized.y * 0.5f, -1);
            Vector3 endPosition = new Vector3(hit.point.x + m_Direction.normalized.x * 0.15f, hit.point.y + m_Direction.normalized.y * 0.15f, -1);
            Vector3 trajectoryPosition = new Vector3(position.x + hitTrajectory.x * 0.5f, position.y + hitTrajectory.y * 0.5f, -1);
        
            if ((-hitTrajectory.x > 0 && -hitTrajectory.y > 0) || (-hitTrajectory.x < 0 && -hitTrajectory.y < 0))
            {
                bounceTrajectory = -bounceTrajectory;
            }
        
            m_DirectionalLine.enabled = true;
            m_HitCircle.enabled = true;

            m_DirectionalLine.SetPosition(0, startPosition);
            m_DirectionalLine.SetPosition(1, endPosition);

            if (hit.collider.TryGetComponent<Ball>(out var ball))
            {
                m_TrajectoryLine.enabled = true;
                m_TrajectoryLine.SetPosition(0, new Vector3(hit.point.x, hit.point.y, -1));
                m_TrajectoryLine.SetPosition(1, trajectoryPosition);
            }
            else
            {
                m_TrajectoryLine.enabled = false;
            }
        
            if (hit.collider.CompareTag(Wall))
            {

                bounceTrajectory = Vector2.Reflect(endPosition.normalized, hit.collider.transform.up);

            }
        
            Vector3 bouncePosition = new Vector3(endPosition.x + bounceTrajectory.x * 0.5f, endPosition.y + bounceTrajectory.y * 0.5f, -1);
            m_DirectionalLine.SetPosition(2, bouncePosition);
            m_HitCircle.transform.position = endPosition;
        }
    }
}
