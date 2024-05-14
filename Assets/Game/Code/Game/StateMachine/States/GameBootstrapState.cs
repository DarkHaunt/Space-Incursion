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

        private readonly NetworkStartArgsProvider _startArgsProvider;
        private readonly TransitionHandler _transitionHandler;
        private readonly NetworkHostService _hostService;
        private readonly NetworkFacade _networkFacade;
        private readonly NetworkRunner _networkRunner;


        public GameBootstrapState(GameStateMachine stateMachine, TransitionHandler transitionHandler, NetworkHostService hostService, 
            NetworkFacade networkFacade, NetworkMonoServiceLocator serviceLocator, NetworkStartArgsProvider startArgsProvider)
        {
            _stateMachine = stateMachine;
            
            _transitionHandler = transitionHandler;
            _startArgsProvider = startArgsProvider;
            _networkFacade = networkFacade;
            _hostService = hostService;

            _networkRunner = serviceLocator.Runner;
        }

        public async UniTask Enter()
        {
            await StartGame();
            
            if (_hostService.IsHost)
                await _hostService.SpawnLevel();
            
            await _transitionHandler.PlayFadeOutAnimation();

            await GoToLobbyState();
        }

        private async UniTask StartGame()
        {
            _networkRunner.AddCallbacks(_networkFacade);
            _networkRunner.ProvideInput = true;

            await _networkRunner.StartGame(_startArgsProvider.GameArgs);
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }

        private async UniTask GoToLobbyState() =>
            await _stateMachine.Enter<GameLobbyState>();
    }
}