using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using Game.Code.Game.Entities.Enemies.Services;
using Game.Code.Game.Entities.Player.Services;
using Game.Code.Infrastructure.Network;
using Game.Code.Game.Services;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Network;
using Fusion;
using UniRx;
using CompositeDisposable = UniRx.CompositeDisposable;

namespace Game.Code.Game.StateMachine.States
{
    public class GameState : IState
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly GameStateMachine _stateMachine;

        private readonly NetworkHostStateHandleService _hostStateHandleService;
        private readonly PlayerHandleService _playerHandleService;
        private readonly EnemyHandleService _enemyHandleService;
        private readonly GameOverService _gameOverService;
        private readonly NetworkRunner _runner;

        private bool _isGameOver;

        public GameState(GameStateMachine stateMachine, NetworkHostStateHandleService hostStateHandleService, PlayerHandleService playerHandleService,
            GameOverService gameOverService, EnemyHandleService enemyHandleService, NetworkMonoServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;

            _hostStateHandleService = hostStateHandleService;
            _playerHandleService = playerHandleService;
            _enemyHandleService = enemyHandleService;
            _gameOverService = gameOverService;

            _runner = serviceLocator.Runner;
        }

        public UniTask Enter()
        {
            if (_hostStateHandleService.IsHost)
            {
                _gameOverService.OnGameOver += HandleGameOver;
                _enemyHandleService.StartSpawning();
            }

            _playerHandleService.OnPlayerKilled
                .Subscribe(x => HandlePlayerKilled(x.Value))
                .AddTo(_disposables);

            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _disposables.Dispose();

            if (_hostStateHandleService.IsHost)
            {
                _gameOverService.OnGameOver -= HandleGameOver;

                _enemyHandleService.StopSpawning();
                _enemyHandleService.KillAllExistingEnemies();
            }

            return UniTask.CompletedTask;
        }

        private async void HandleGameOver(GameResultsData results)
        {
            _isGameOver = true;
            await _stateMachine.Enter<GameOverState, GameResultsData>(results);
        }

        private async void HandlePlayerKilled(PlayerRef player)
        {
            if (_isGameOver || _runner.LocalPlayer != player)
                return;

            await _stateMachine.Enter<LoseState>();
        }
    }
}