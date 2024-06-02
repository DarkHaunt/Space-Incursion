using Game.Code.Common.StateMachineBase.Interfaces;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;

namespace Game.Code.Game.Core.States
{
    public class GameBootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private readonly EnemyHandleService _enemyHandleService;
        private readonly NetworkSpawnService _spawnService;


        public GameBootstrapState(GameStateMachine stateMachine, NetworkSpawnService spawnService, EnemyHandleService enemyHandleService)
        {
            _stateMachine = stateMachine;

            _enemyHandleService = enemyHandleService;
            _spawnService = spawnService;
        }

        public async UniTask Enter()
        {
            await SetUpSpawning();
            
            await GoToLobbyState();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;

        private async UniTask SetUpSpawning()
        {
            if (_spawnService.IsHost)
            {
                var level = await _spawnService.SpawnLevel();
                _enemyHandleService.Init(level.ArenaArea);
            }
            
            await _spawnService.SpawnUIRoot();
        }

        private async UniTask GoToLobbyState() =>
            await _stateMachine.Enter<GameLobbyState>();
    }
}