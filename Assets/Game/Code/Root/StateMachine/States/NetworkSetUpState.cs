using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Infrastructure.SceneManaging;
using Game.Code.Game.Services;
using Cysharp.Threading.Tasks;
using Game.Code.Game;
using Fusion;

namespace Game.Code.Root.StateMachine.States
{
    public class NetworkSetUpState : IState
    {
        private readonly RootStateMachine _stateMachine;
        private readonly NetworkArgsProvider _networkArgsProvider;
        private readonly TransitionHandler _transitionHandler;
        private readonly NetworkSceneLoader _sceneLoader;
        private readonly NetworkRunner _networkRunner;
        
        public NetworkSetUpState(RootStateMachine stateMachine, NetworkArgsProvider networkArgsProvider, 
            NetworkServiceLocator networkServiceLocator, TransitionHandler transitionHandler, NetworkSceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _networkArgsProvider = networkArgsProvider;
            _transitionHandler = transitionHandler;
            _sceneLoader = sceneLoader;

            _networkRunner = networkServiceLocator.Runner;
        }
        
        public UniTask Enter()
        {
            _networkArgsProvider.OnGameArgsCreated += SetUpNetworkAndStartGame;

            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _networkArgsProvider.OnGameArgsCreated -= SetUpNetworkAndStartGame;
            
            return UniTask.CompletedTask;
        }

        private async void SetUpNetworkAndStartGame(StartGameArgs args)
        {
            _transitionHandler.FadeImmediate();
            _networkRunner.ProvideInput = true;

            await _networkRunner.StartGame(args);
            await _sceneLoader.LoadSceneNetwork(_networkRunner, Scenes.Game);

            await _stateMachine.Enter<GameState>();
        }
    }
}