using Cysharp.Threading.Tasks;
using Game.Code.Game.Entities.Enemies.Services;
using Game.Code.Game.Network;
using Game.Code.Game.Services;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;

namespace Game.Code.Game.StateMachine.States
{
    public class HostBootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private readonly NetworkHostStateHandleService _hostStateHandleService;
        private readonly EnemyHandleService _enemyHandleService;
        private readonly GameStartService _gameStartService;
        private readonly GameFactory _gameFactory;


        public HostBootstrapState(GameStateMachine stateMachine, NetworkHostStateHandleService hostStateHandleService, GameFactory gameFactory, 
            EnemyHandleService enemyHandleService, GameStartService gameStartService)
        {
            _stateMachine = stateMachine;

            _hostStateHandleService = hostStateHandleService;
            _enemyHandleService = enemyHandleService;
            _gameStartService = gameStartService;
            _gameFactory = gameFactory;
        }

        public async UniTask Enter()
        {
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
                var view = await _gameFactory.CreateGameStartView();

                _enemyHandleService.Init(level);
                _gameStartService.Init(view);
            }

            _hostStateHandleService.SetHostIsInitialized();
        }

        private async UniTask GoToLobbyState() =>
            await _stateMachine.Enter<LobbyState>();
    }
}