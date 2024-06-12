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
        private readonly CompositeDisposable _disposables = new ();

        private readonly NetworkPlayerHandleService _networkPlayerHandleService;
        private readonly PlayerHandleService _playerHandleService;
        private readonly TransitionHandler _transitionHandler;
        private readonly GameStateMachine _stateMachine;

        public LobbyState(GameStateMachine stateMachine, PlayerHandleService playerHandleService, NetworkPlayerHandleService networkPlayerHandleService, TransitionHandler transitionHandler)
        {
            _stateMachine = stateMachine;
            
            _networkPlayerHandleService = networkPlayerHandleService;
            _playerHandleService = playerHandleService;
            _transitionHandler = transitionHandler;
        }
        
        public async UniTask Enter()
        {
            ObservePlayersIncome();
            
            //await ProcessPlayerSpawn();
            
            await _transitionHandler.PlayFadeOutAnimation();

            //await _stateMachine.Enter<GameState>();
        }

        private UniTask ProcessPlayerSpawn()
        {
            var spawnTasks = _networkPlayerHandleService.PlayersToSpawn.Select(SpawnPlayer);
            return UniTask.WhenAll(spawnTasks);
        }

        private void ObservePlayersIncome()
        {
            _networkPlayerHandleService.PlayersToSpawn
                .ToObservable()
                .Subscribe(HandlePlayerIncome)
                .AddTo(_disposables);
        }

        private void HandlePlayerIncome(PlayerRef player)
        {
            if (_playerHandleService.IsPlayerAlreadyRegistered(player))
                return;

            SpawnPlayer(player).Forget();
        }

        private  async UniTask SpawnPlayer(PlayerRef player) =>
            await _networkPlayerHandleService.SetUpPlayerData(player);

        public UniTask Exit()
        {
            _disposables.Dispose();
            
            return UniTask.CompletedTask;
        }
    }
}