using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Infrastructure.SceneManaging;
using Game.Code.Game.StaticData.Indents;
using Code.Infrastructure.AssetManaging;
using Cysharp.Threading.Tasks;

namespace Game.Code.Root.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly AssetProvider _assetProvider;

        private readonly RootStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(RootStateMachine stateMachine, AssetProvider assetProvider, SceneLoader sceneLoader)
        {
            _assetProvider = assetProvider;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public async UniTask Enter()
        {
            await PrewarmAssets();
            await GoToMenuScene();

            await _stateMachine.Enter<NetworkSetUpState>();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;

        private async UniTask GoToMenuScene() =>
            await _sceneLoader.Load(Scenes.Menu);

        private async UniTask PrewarmAssets()
        {
            await _assetProvider.InitializeAsync();

            await UniTask.WhenAll
            (
                _assetProvider.WarmupAssetsByLabel(AddressableIndents.GameplayAssetsLabel),
                _assetProvider.WarmupAssetsByLabel(AddressableIndents.GlobalAssetsLabel)
            );
        }
    }
}