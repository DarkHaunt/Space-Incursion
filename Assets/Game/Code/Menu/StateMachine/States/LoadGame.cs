using Cysharp.Threading.Tasks;
using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Infrastructure.SceneManaging;

namespace Game.Code.Menu.StateMachine.States
{
    public class LoadGame : IState
    {
        private readonly SceneLoader _sceneLoader;

        public LoadGame(SceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;

        public async UniTask Enter()
        {
            await _sceneLoader.Load(Scenes.Game);
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;
        
        public class Factory
        {
            private readonly SceneLoader _sceneLoader;

            public Factory(SceneLoader sceneLoader) =>
                _sceneLoader = sceneLoader;

            public LoadGame CreateState() =>
                new (_sceneLoader);
        }
    }
}