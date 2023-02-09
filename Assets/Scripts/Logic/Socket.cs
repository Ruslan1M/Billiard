using System;
using UnityEngine;

namespace Logic
{
    public class Socket : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<Ball>(out var ball))
            {
                ball.BallManager.ResetGame();
                ball.gameObject.SetActive(false);
            }

            if (col.TryGetComponent<Player.Player>(out var player))
            {
                player.ResetGame();
            }
        }
    }
}