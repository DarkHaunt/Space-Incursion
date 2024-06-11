using Game.Code.Common.StateMachineBase.Interfaces;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Scene;
using Game.Code.Game.Services;
using UnityEngine;

namespace Game.Code.Game.Core.States
{
    public class GameBootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private readonly SceneDependenciesProvider _sceneDependenciesProvider;
        private readonly PhysicCollisionService _collisionService;
        private readonly EnemyHandleService _enemyHandleService;
        private readonly NetworkSpawnService _spawnService;


        public GameBootstrapState(GameStateMachine stateMachine, NetworkSpawnService spawnService,
            PhysicCollisionService collisionService, EnemyHandleService enemyHandleService, SceneDependenciesProvider sceneDependenciesProvider)
        {
            _stateMachine = stateMachine;

            _enemyHandleService = enemyHandleService;
            _sceneDependenciesProvider = sceneDependenciesProvider;
            _collisionService = collisionService;
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
               // var cameraService = await _spawnService.SpawnCameraService();

                _enemyHandleService.Init(level);

                await UniTask.Delay(2000);
                
                _sceneDependenciesProvider.CameraService.SetLevelBorders(level);
            }

            _spawnService.SetHostIsInitialized();
            Debug.Log($"<color=white>Spawn Host</color>");

            await _spawnService.SpawnUIRoot();
        }

        private async UniTask GoToLobbyState() =>
            await _stateMachine.Enter<GameLobbyState>();
    }
}