using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using UnityEngine;
using UniRx;

namespace Game.Code.Game.StateMachine.States
{
    public class LoseState : IState
    {
        private readonly CompositeDisposable _disposables = new();

        private readonly GameStateMachine _gameStateMachine;
        private readonly CameraService _cameraService;
        private readonly UIService _uiService;

        
        public LoseState(GameStateMachine gameStateMachine, UIService uiService, CameraService cameraService)
        {
            _gameStateMachine = gameStateMachine;
            _cameraService = cameraService;
            _uiService = uiService;
        }
        
        
        public UniTask Enter()
        {
            _uiService.OnExitButtonClick
                .Subscribe(_ => GoToMenuScene())
                .AddTo(_disposables);

            _uiService.ShowDeathScreen();
            _cameraService.CancelFollow();

            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _uiService.HideDeathScreen();
            _disposables.Dispose();
            
            return UniTask.CompletedTask;
        }
        
        private void GoToMenuScene() =>
            _gameStateMachine.Enter<ShutdownState>().Forget();
    }
}