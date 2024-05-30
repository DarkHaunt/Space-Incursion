using System.Threading.Tasks;
using Game.Code.Common.StateMachineBase.Interfaces;
using Cysharp.Threading.Tasks;
using Fusion;
using Game.Code.Game.Services;
using Game.Code.Infrastructure.SceneManaging;

namespace Game.Code.Game.Core.States
{
    public class GameBootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private readonly NetworkPlayerDataProvider _playerDataProvider;
        private readonly TransitionHandler _transitionHandler;
        private readonly NetworkSpawnService _spawnService;
        private readonly NetworkFacade _networkFacade;
        private readonly NetworkRunner _networkRunner;


        public GameBootstrapState(GameStateMachine stateMachine, TransitionHandler transitionHandler, NetworkSpawnService spawnService,
            NetworkFacade networkFacade, NetworkMonoServiceLocator serviceLocator, NetworkPlayerDataProvider playerDataProvider)
        {
            _stateMachine = stateMachine;

            _transitionHandler = transitionHandler;
            _playerDataProvider = playerDataProvider;
            _networkFacade = networkFacade;
            _spawnService = spawnService;

            _networkRunner = serviceLocator.Runner;
        }

        public async UniTask Enter()
        {
            await StartGame();
            await SetUpHostSide();

            await _transitionHandler.PlayFadeOutAnimation();

            await GoToLobbyState();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }

        private async UniTask SetUpHostSide()
        {
            await _spawnService.TryToSpawnUIRoot();
            await _spawnService.TryToSpawnLevel();
        }

        private async UniTask StartGame()
        {
            _networkRunner.AddCallbacks(_networkFacade);
            _networkRunner.ProvideInput = true;

            await _networkRunner.StartGame(_playerDataProvider.PlayerData.GameArgs);
        }

        private async UniTask GoToLobbyState() =>
            await _stateMachine.Enter<GameLobbyState>();
    }
}