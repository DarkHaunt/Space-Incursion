using Cysharp.Threading.Tasks;
using Game.Code.Game.Entities.Enemies.Services;
using Game.Code.Game.Entities.Player.Services;
using Game.Code.Game.Network;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using UniRx;
using CompositeDisposable = UniRx.CompositeDisposable;

namespace Game.Code.Game.StateMachine.States
{
    public class GameState : IState
    {
        private readonly CompositeDisposable _disposables = new();
        
        private readonly NetworkHostStateHandleService _hostStateHandleService;
        private readonly PlayerHandleService _playerHandleService;
        private readonly EnemyHandleService _enemyHandleService;

        public GameState(NetworkHostStateHandleService hostStateHandleService, PlayerHandleService playerHandleService, EnemyHandleService enemyHandleService)
        {
            _hostStateHandleService = hostStateHandleService;
            _playerHandleService = playerHandleService;
            _enemyHandleService = enemyHandleService;
        }

        public UniTask Enter()
        {
            if (_hostStateHandleService.IsHost)
            {
                _playerHandleService.OnPlayerKilled
                    .Subscribe(_ => HandlePlayerDead())
                    .AddTo(_disposables);
                
                _enemyHandleService.StartSpawning();
            }

            return UniTask.CompletedTask;
        }

        private void HandlePlayerDead()
        {
            
        }

        public UniTask Exit()
        {
            _disposables.Dispose();
            
            if (_hostStateHandleService.IsHost)
            {
                _enemyHandleService.StopSpawning();
                _enemyHandleService.KillAllExistingEnemies();
            }

            return UniTask.CompletedTask;
        }
    }
}