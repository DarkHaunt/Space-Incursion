using Game.Code.Common.StateMachineBase.Interfaces;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Game.StaticData;

namespace Game.Code.Game.Core.States
{
    public class GameBootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private readonly NetworkHostStateHandleService _hostStateHandleService;
        private readonly GameStaticDataProvider _gameStaticDataProvider;
        private readonly PhysicCollisionService _collisionService;
        private readonly EnemyHandleService _enemyHandleService;
        private readonly CameraService _cameraService;
        private readonly GameFactory _gameFactory;


        public GameBootstrapState(GameStateMachine stateMachine, PhysicCollisionService collisionService, GameStaticDataProvider gameStaticDataProvider,
            NetworkHostStateHandleService hostStateHandleService, GameFactory gameFactory, EnemyHandleService enemyHandleService, CameraService cameraService)
        {
            _stateMachine = stateMachine;

            _hostStateHandleService = hostStateHandleService;
            _gameStaticDataProvider = gameStaticDataProvider;
            _enemyHandleService = enemyHandleService;
            _collisionService = collisionService;
            _cameraService = cameraService;
            _gameFactory = gameFactory;
        }

        public async UniTask Enter()
        {
            await SetUpClientSideServices();
            await SetUpHostSideServices();

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
            }

            _hostStateHandleService.SetHostIsInitialized();
        }

        private async UniTask SetUpClientSideServices()
        {
            _collisionService.Enable();
            _cameraService.SetLevelBorders
            (
                _gameStaticDataProvider.GameConfig.CameraLeftBottomBound,
                _gameStaticDataProvider.GameConfig.CameraRightTopBound
            );

            await _gameFactory.CreateUIRoot();
        }

        private async UniTask GoToLobbyState() =>
            await _stateMachine.Enter<LobbyState>();
    }
}