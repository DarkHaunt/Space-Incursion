using Code.Infrastructure.StateMachineBase;
using Cysharp.Threading.Tasks;
using Code.Infrastructure;

namespace Game.Scripts.Menu.StateMachine.States
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