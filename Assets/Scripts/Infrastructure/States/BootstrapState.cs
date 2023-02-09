using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services.Input;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private const string MainScene = "MainScene";
        
        private readonly GameStateMachine m_StateMachine;
        private readonly SceneLoader m_SceneLoader;
        private readonly AllServices m_Services;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            m_StateMachine = stateMachine;
            m_SceneLoader = sceneLoader;
            m_Services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            m_SceneLoader.Load(Initial, onLoaded:EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel() => m_StateMachine.Enter<LoadLevelState, string>(MainScene);

        private void RegisterServices()
        {
            m_Services.RegisterSingle<IInputService>(InputService());
            m_Services.RegisterSingle<IAssetProvider>(new AssetProvider());
            m_Services.RegisterSingle<IGameFactory>(
                new GameFactory(m_Services.Single<IAssetProvider>()));
        }

        private static IInputService InputService() => new MobileInputService();
    }
}