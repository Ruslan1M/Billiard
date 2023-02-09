using Infrastructure.Services;
using Services.Input;
using UnityEngine;

namespace Player
{
    public class PlayerInput
    {
        private readonly IInputService m_InputService;
        private readonly PushPlayerBall m_PushPlayerBall;

        public PlayerInput(PushPlayerBall pushPlayerBall, IInputService inputService)
        {
            m_PushPlayerBall = pushPlayerBall;
            m_InputService = inputService;
        }

        public void UpdateState(Vector3 t)
        {
            if (m_InputService.IsFireButton())
            {
                m_PushPlayerBall.SetLine(t);
            }

            if (m_InputService.IsFireButtonUp())
            {
                m_PushPlayerBall.Push();
            }
        }
    }
}