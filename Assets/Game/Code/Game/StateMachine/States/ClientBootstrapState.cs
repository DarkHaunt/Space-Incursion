using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Game.StaticData;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;

namespace Game.Code.Game.Core.States
{
    public class ClientBootstrapState : IState
    {
        private readonly GameStaticDataProvider _gameStaticDataProvider;
        private readonly PhysicCollisionService _collisionService;
        private readonly GameStateMachine _stateMachine;
        private readonly CameraService _cameraService;
        private readonly GameFactory _gameFactory;

        public ClientBootstrapState(GameStaticDataProvider gameStaticDataProvider, PhysicCollisionService collisionService, 
            GameStateMachine stateMachine, CameraService cameraService, GameFactory gameFactory)
        {
            _gameStaticDataProvider = gameStaticDataProvider;
            _collisionService = collisionService;
            _cameraService = cameraService;
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
        }

        public async UniTask Enter()
        {
            await SetUpClientSideServices();
            await GoToHostState();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;

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

        private UniTask GoToHostState() =>
            _stateMachine.Enter<HostBootstrapState>();
    }
}