using Infrastructure.States;

namespace Player
{
    public class PlayerState
    {
        private readonly GameStateMachine m_StateMachine;

        public PlayerState(GameStateMachine stateMachine)
        {
            m_StateMachine = stateMachine;
        }

        public void ResetGameState()
        {
           m_StateMachine.Enter<ResetGameState>();
        }
    }
}