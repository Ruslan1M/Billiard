using System;
using Infrastructure.Services;
using Services.Input;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private PushPlayerBall _pushPlayerBall;
        private PlayerState _playerState;
        
        public PushPlayerBall PushPlayerBall => _pushPlayerBall;
        
        public void Init(PlayerState playerState)
        {
            _pushPlayerBall = GetComponent<PushPlayerBall>();
            _playerInput = new PlayerInput(_pushPlayerBall, AllServices.Container.Single<IInputService>());
            _playerState = playerState;
        }

        public void ResetGame()
        {
            _playerState.ResetGameState();
        }

        private void Update()
        {
            _playerInput.UpdateState(transform.position);
        }
    }
}
