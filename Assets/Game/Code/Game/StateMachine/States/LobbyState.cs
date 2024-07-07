using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using Game.Code.Game.Entities.Player.Services;
using Game.Code.Infrastructure.SceneManaging;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Game.Network;
using Fusion;
using Game.Code.Infrastructure.Network;
using UniRx;

namespace Game.Code.Game.StateMachine.States
{
    public class LobbyState : IState
    {
        private readonly CompositeDisposable _disposables = new();

        private readonly NetworkPlayerHandleService _networkPlayerHandleService;
        private readonly NetworkHostStateHandleService _hostStateHandleService;
        private readonly PlayerHandleService _playerHandleService;
        private readonly TransitionHandler _transitionHandler;
        private readonly GameStartService _gameStartService;
        private readonly GameStateMachine _stateMachine;

        public LobbyState(GameStateMachine stateMachine, PlayerHandleService playerHandleService, TransitionHandler transitionHandler,
            NetworkPlayerHandleService networkPlayerHandleService, NetworkHostStateHandleService hostStateHandleService,
            GameStartService gameStartService)
        {
            _stateMachine = stateMachine;

            _networkPlayerHandleService = networkPlayerHandleService;
            _hostStateHandleService = hostStateHandleService;
            _playerHandleService = playerHandleService;
            _transitionHandler = transitionHandler;
            _gameStartService = gameStartService;
        }

        public async UniTask Enter()
        {
            ObservePlayersIncome();
            ObserveStartGameView();

            await ProcessPlayersSpawn();

            await _transitionHandler.PlayFadeOutAnimation();
        }

        public UniTask Exit()
        {
            _disposables.Dispose();
            _gameStartService.HideView();
            
            NetworkSignalBus.OnGameStarted -= GoToGameStart;

            return UniTask.CompletedTask;
        }

        private UniTask ProcessPlayersSpawn()
        {
            var spawnTasks = _networkPlayerHandleService
                .GetAllPlayersToSpawn()
                .Select(SpawnPlayer);

            return UniTask.WhenAll(spawnTasks);
        }

        private void ObserveStartGameView() =>
            NetworkSignalBus.OnGameStarted += GoToGameStart;

        private void ObservePlayersIncome()
        {
            _networkPlayerHandleService.OnPlayerAddedToSpawnQueue
                .Subscribe(x => HandlePlayerIncome(x.Value))
                .AddTo(_disposables);
        }

        private async void HandlePlayerIncome(PlayerRef player)
        {
            if (_playerHandleService.IsPlayerAlreadyRegistered(player))
                return;

            await SpawnPlayer(player);
        }

        private async UniTask SpawnPlayer(PlayerRef player)
        {
            await _networkPlayerHandleService.SetUpPlayerData(player);

            if (_hostStateHandleService.IsHost)
                _gameStartService.UpdateStartCondition(_playerHandleService.AlivePlayersCount);
        }

        private async void GoToGameStart() =>
            await _stateMachine.Enter<GameState>();
    }
}