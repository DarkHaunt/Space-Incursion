using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using Game.Code.Infrastructure.SceneManaging;
using Game.Code.Infrastructure.Network;
using Cysharp.Threading.Tasks;
using Fusion;

namespace Game.Code.Game.StateMachine.States
{
    public class ShutdownState : IState
    {
        private readonly NetworkRunner _networkRunner;
        private readonly SceneLoader _sceneLoader;

        public ShutdownState(NetworkMonoServiceLocator serviceLocator, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _networkRunner = serviceLocator.Runner;
        }
        
        public UniTask Enter()
        {
            _networkRunner.Shutdown();

            return _sceneLoader.Load(Scenes.Menu);
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;
    }
}