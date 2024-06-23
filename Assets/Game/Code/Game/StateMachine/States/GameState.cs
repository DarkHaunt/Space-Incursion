using Cysharp.Threading.Tasks;
using Game.Code.Game.Entities.Enemies.Services;
using Game.Code.Game.Network;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;

namespace Game.Code.Game.StateMachine.States
{
    public class GameState : IState
    {
        private readonly NetworkHostStateHandleService _hostStateHandleService;
        private readonly EnemyHandleService _enemyHandleService;

        public GameState(NetworkHostStateHandleService hostStateHandleService, EnemyHandleService enemyHandleService)
        {
            _hostStateHandleService = hostStateHandleService;
            _enemyHandleService = enemyHandleService;
        }

        public UniTask Enter()
        {
            if (_hostStateHandleService.IsHost)
                _enemyHandleService.StartSpawning();

            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            if (_hostStateHandleService.IsHost)
            {
                _enemyHandleService.StopSpawning();
                _enemyHandleService.KillAllExistingEnemies();
            }

            return UniTask.CompletedTask;
        }
    }
}