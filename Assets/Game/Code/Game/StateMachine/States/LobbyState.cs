using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Infrastructure.SceneManaging;
using Game.Code.Game.Services;
using Cysharp.Threading.Tasks;
using Fusion;
using UniRx;

namespace Game.Code.Game.Core.States
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
            
            GoToGameStart();
        }

        public UniTask Exit()
        {
            _disposables.Dispose();
            _gameStartService.HideView();

            return UniTask.CompletedTask;
        }

        private UniTask ProcessPlayersSpawn()
        {
            var spawnTasks = _networkPlayerHandleService
                .GetAllPlayersToSpawn()
                .Select(SpawnPlayer);

            return UniTask.WhenAll(spawnTasks);
        }

        private void ObserveStartGameView()
        {
            if (!_hostStateHandleService.IsHost)
                return;

            _gameStartService.OnGameStartButtonClick
                .Subscribe(_ => GoToGameStart())
                .AddTo(_disposables);
        }

        private void ObservePlayersIncome()
        {
            _networkPlayerHandleService.OnPlayerAdded
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
                _gameStartService.UpdateStartCondition(_playerHandleService.PlayersCount);
        }

        private async void GoToGameStart() =>
            await _stateMachine.Enter<GameState>();
    }
}