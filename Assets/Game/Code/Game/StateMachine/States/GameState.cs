using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using Game.Code.Game.Entities.Enemies.Services;
using Game.Code.Game.Entities.Player.Services;
using Game.Code.Infrastructure.Network;
using Game.Code.Game.Services;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Network;
using Fusion;
using UniRx;
using UnityEngine;
using CompositeDisposable = UniRx.CompositeDisposable;

namespace Game.Code.Game.StateMachine.States
{
    public class GameState : IState
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly GameStateMachine _stateMachine;
        private readonly NetworkRunner _runner;

        private readonly NetworkHostStateHandleService _hostStateHandleService;
        private readonly PlayerHandleService _playerHandleService;
        private readonly EnemyHandleService _enemyHandleService;
        private readonly GameOverService _gameOverService;
        private readonly CameraService _cameraService;
        private readonly UIService _uiService;

        private bool _isGameOver;

        public GameState(GameStateMachine stateMachine, NetworkHostStateHandleService hostStateHandleService, PlayerHandleService playerHandleService,
            GameOverService gameOverService, EnemyHandleService enemyHandleService, NetworkMonoServiceLocator serviceLocator, UIService uiService,
            CameraService cameraService)
        {
            _stateMachine = stateMachine;

            _hostStateHandleService = hostStateHandleService;
            _playerHandleService = playerHandleService;
            _enemyHandleService = enemyHandleService;
            _gameOverService = gameOverService;
            _cameraService = cameraService;
            _uiService = uiService;

            _runner = serviceLocator.Runner;
        }

        public UniTask Enter()
        {
            if (_hostStateHandleService.IsHost)
            {
                _enemyHandleService.StartSpawning();
            }

            _gameOverService.OnGameOver += HandleGameOver;
            _playerHandleService.OnPlayerKilled
                .Subscribe(x => HandlePlayerKilled(x.Value))
                .AddTo(_disposables);

            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _disposables.Dispose();
            _uiService.HideDeathScreen();
            _gameOverService.OnGameOver -= HandleGameOver;

            return UniTask.CompletedTask;
        }

        private async void HandleGameOver(GameResultsData results)
        {
            _isGameOver = true;
            await _stateMachine.Enter<GameOverState, GameResultsData>(results);
        }

        private void HandlePlayerKilled(PlayerRef player)
        {
            if (_isGameOver || _runner.LocalPlayer != player)
                return;

            _uiService.OnExitButtonClick
                .Subscribe(_ => GoToMenuScene())
                .AddTo(_disposables);

            _uiService.ShowDeathScreen();
            _cameraService.CancelFollow();
        }

        private async void GoToMenuScene() =>
            await _stateMachine.Enter<ShutdownState>();
    }
}