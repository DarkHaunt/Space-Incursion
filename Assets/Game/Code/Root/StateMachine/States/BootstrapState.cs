using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Infrastructure.SceneManaging;
using Game.Code.Game.StaticData.Indents;
using Code.Infrastructure.AssetManaging;
using Cysharp.Threading.Tasks;

namespace Game.Code.Root.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly TransitionHandler _transitionHandler;
        private readonly RootStateMachine _stateMachine;
        private readonly AssetProvider _assetProvider;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(RootStateMachine stateMachine, AssetProvider assetProvider, TransitionHandler transitionHandler, SceneLoader sceneLoader)
        {
            _transitionHandler = transitionHandler;
            _assetProvider = assetProvider;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public async UniTask Enter()
        {
            _transitionHandler.FadeImmediate();
            
            await PrewarmAssets();
            await GoToGameScene();

            await _stateMachine.Enter<GameState>();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;

        private UniTask GoToGameScene() =>
            _sceneLoader.Load(Scenes.Game);

        private async UniTask PrewarmAssets()
        {
            await _assetProvider.InitializeAsync();
            
            await _assetProvider.WarmupAssetsByLabel(AddressableLabels.ProjectileLabel);
            await _assetProvider.WarmupAssetsByLabel(AddressableLabels.PlayerLabel);
            await _assetProvider.WarmupAssetsByLabel(AddressableLabels.EnemyLabel);
        }
    }
}