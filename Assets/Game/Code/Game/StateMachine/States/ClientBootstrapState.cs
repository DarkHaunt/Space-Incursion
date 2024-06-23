using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Infrastructure.AssetManaging;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;

namespace Game.Code.Game.StateMachine.States
{
    public class ClientBootstrapState : IState
    {
        private readonly GameStaticDataProvider _gameStaticDataProvider;
        private readonly PhysicCollisionService _collisionService;
        private readonly GameStateMachine _stateMachine;
        private readonly CameraService _cameraService;
        private readonly GameFactory _gameFactory;
        private readonly UIService _uiService;

        public ClientBootstrapState(GameStaticDataProvider gameStaticDataProvider, PhysicCollisionService collisionService, 
            GameStateMachine stateMachine, CameraService cameraService, UIService uiService, GameFactory gameFactory)
        {
            _gameStaticDataProvider = gameStaticDataProvider;
            _collisionService = collisionService;
            _cameraService = cameraService;
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _uiService = uiService;
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
            
            var deathView = await _gameFactory.CreatePlayerDeathView();
            var resultsView = await _gameFactory.CreateGameResultsView();

            _uiService.Init(deathView, resultsView);
        }

        private UniTask GoToHostState() =>
            _stateMachine.Enter<HostBootstrapState>();
    }
}