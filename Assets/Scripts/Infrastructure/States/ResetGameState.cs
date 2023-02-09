using UnityEngine;

namespace Infrastructure.States
{
    public class ResetGameState : IState
    {
        private const string Initial = "Initial";
        private readonly GameBootstrapper m_GameBootstrapper;
        private readonly SceneLoader m_SceneLoader;

        public ResetGameState(GameStateMachine stateMachine,GameBootstrapper gameBootstrapper, SceneLoader sceneLoader)
        {
            m_GameBootstrapper = gameBootstrapper;
            m_SceneLoader = sceneLoader;
        }

        public void Enter()
        {
            m_SceneLoader.Load(Initial, OnLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            Object.Destroy(m_GameBootstrapper.gameObject);
        }
    }
}