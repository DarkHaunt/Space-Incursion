using Game.Code.Common.StateMachineBase.Interfaces;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Scene;
using Game.Code.Game.Services;

namespace Game.Code.Game.Core.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private readonly SceneDependenciesProvider _sceneDependenciesProvider;
        private readonly NetworkHostStateHandleService _hostStateHandleService;
        private readonly PhysicCollisionService _collisionService;
        private readonly EnemyHandleService _enemyHandleService;
        private readonly GameFactory _gameFactory;


        public BootstrapState(GameStateMachine stateMachine, PhysicCollisionService collisionService, NetworkHostStateHandleService hostStateHandleService, 
            GameFactory gameFactory, SceneDependenciesProvider sceneDependenciesProvider, EnemyHandleService enemyHandleService)
        {
            _stateMachine = stateMachine;

            _sceneDependenciesProvider = sceneDependenciesProvider;
            _hostStateHandleService = hostStateHandleService;
            _enemyHandleService = enemyHandleService;
            _collisionService = collisionService;
            _gameFactory = gameFactory;
        }

        public async UniTask Enter()
        {
            await SetUpHostSideServices();
            await SetUpClientSideServices();

            await GoToLobbyState();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;

        private async UniTask SetUpHostSideServices()
        {
            await _hostStateHandleService.WaitUntilHostInitialized();

            if (_hostStateHandleService.IsHost)
            {
                var level = await _gameFactory.CreateLevel();
                _enemyHandleService.Init(level);
                
                //_sceneDependenciesProvider.CameraService.SetLevelBorders(level);
            }

            _hostStateHandleService.SetHostIsInitialized();
        }

        private async UniTask SetUpClientSideServices()
        {
            _collisionService.Enable();
            
            await _gameFactory.CreateUIRoot(_sceneDependenciesProvider.UIRoot);
        }

        private async UniTask GoToLobbyState() =>
            await _stateMachine.Enter<LobbyState>();
    }
}