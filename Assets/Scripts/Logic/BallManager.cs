using System.Collections.Generic;
using Infrastructure.States;
using UnityEngine;

namespace Logic
{
    public class BallManager : MonoBehaviour
    {
        private List<Ball> m_Balls;
        private GameStateMachine m_StateMachine;

        public void Init(GameStateMachine stateMachine)
        {
            m_Balls = new List<Ball>();
            m_StateMachine = stateMachine;
        }

        public void ResetGame()
        {
            int ballAmount = 0;

            foreach (var ball in m_Balls)
            {
                if (ball.gameObject.activeInHierarchy)
                {
                    ballAmount++;
                }
            }
            
            if(ballAmount > 0) return;
            m_StateMachine.Enter<ResetGameState>();
        }

        public void AddBallToList(Ball ball)
        { 
            m_Balls.Add(ball);
        }
    }
}