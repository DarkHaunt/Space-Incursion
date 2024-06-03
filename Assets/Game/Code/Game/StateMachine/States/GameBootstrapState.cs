using Game.Code.Common.StateMachineBase.Interfaces;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;

namespace Game.Code.Game.Core.States
{
    public class GameBootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private readonly PhysicCollisionService _collisionService;
        private readonly EnemyHandleService _enemyHandleService;
        private readonly NetworkSpawnService _spawnService;
        private readonly CameraService _cameraService;


        public GameBootstrapState(GameStateMachine stateMachine, NetworkSpawnService spawnService, CameraService cameraService,
            PhysicCollisionService collisionService, EnemyHandleService enemyHandleService)
        {
            _stateMachine = stateMachine;

            _enemyHandleService = enemyHandleService;
            _collisionService = collisionService;
            _cameraService = cameraService;
            _spawnService = spawnService;
        }

        public async UniTask Enter()
        {
            _collisionService.Enable();
            
            await SetUpLevelDependentServices();
            
            await GoToLobbyState();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;

        private async UniTask SetUpLevelDependentServices()
        {
            if (_spawnService.IsHost)
            {
                var level = await _spawnService.SpawnLevel();
                
                _enemyHandleService.Init(level);
                _cameraService.Init(level);
            }
            
            await _spawnService.SpawnUIRoot();
        }

        private async UniTask GoToLobbyState() =>
            await _stateMachine.Enter<GameLobbyState>();
    }
}