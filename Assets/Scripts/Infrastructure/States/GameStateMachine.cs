using System;
using System.Collections.Generic;
using Configs;
using Infrastructure.Factory;
using Infrastructure.Services;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(GameBootstrapper gameBootstrapper, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain, AllServices services,
            BallConfig ballConfig)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, gameBootstrapper,sceneLoader, loadingCurtain, services.Single<IGameFactory>(), ballConfig),
                [typeof(GameLoopState)] = new GameLoopState(this),
                [typeof(ResetGameState)] = new ResetGameState(this,gameBootstrapper, sceneLoader),
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }
        
        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayloadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }
        
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }
        
        private TState GetState<TState>() where TState : class, IExitableState => _states[typeof(TState)] as TState;

    }
}