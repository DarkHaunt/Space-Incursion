using Game.Code.Infrastructure.SceneManaging;
using Game.Code.Game.StaticData.Indents;
using Cysharp.Threading.Tasks;
using Game.Code.Game.StaticData;
using Game.Code.Infrastructure.AssetManaging;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;

namespace Game.Code.Root.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStaticDataProvider _staticDataProvider;
        private readonly AssetProvider _assetProvider;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(AssetProvider assetProvider, GameStaticDataProvider staticDataProvider, SceneLoader sceneLoader)
        {
            _staticDataProvider = staticDataProvider;
            _assetProvider = assetProvider;
            _sceneLoader = sceneLoader;
        }

        public async UniTask Enter()
        {
            await _staticDataProvider.PrewarmData();

            await PrewarmAssets();
            await GoToMenuScene();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;

        private async UniTask GoToMenuScene() =>
            await _sceneLoader.Load(Scenes.Menu);

        private async UniTask PrewarmAssets()
        {
            await _assetProvider.InitializeAsync();
            await _assetProvider.WarmupAssetsByLabel(AddressableIndents.GlobalAssetsLabel);
        }
    }
}