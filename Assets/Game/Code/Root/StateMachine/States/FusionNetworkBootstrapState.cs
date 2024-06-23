using Game.Code.Infrastructure.SceneManaging;
using Cysharp.Threading.Tasks;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;

namespace Game.Code.Root.StateMachine.States
{
    public class FusionNetworkBootstrapState : IState
    {
        private readonly RootStateMachine _stateMachine;

        private readonly TransitionHandler _transitionHandler;
        private readonly SceneLoader _sceneLoader;

        public FusionNetworkBootstrapState(RootStateMachine stateMachine, TransitionHandler transitionHandler, SceneLoader sceneLoader)
        {
            _transitionHandler = transitionHandler;

            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }


        public async UniTask Enter()
        {
            await _transitionHandler.PlayFadeInAnimation();

            await _sceneLoader.Load(Scenes.Game);
            await _stateMachine.Enter<GameState>();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;
    }
}