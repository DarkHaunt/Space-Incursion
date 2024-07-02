using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using Game.Code.Infrastructure.AssetManaging;
using Game.Code.Game.StaticData.Indents;
using Cysharp.Threading.Tasks;

namespace Game.Code.Root.StateMachine.States
{
    public class GameState : IState
    {
        private readonly AssetProvider _assetProvider;

        public GameState(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask Enter() =>
            await _assetProvider.WarmupAssetsByLabel(AddressableIndents.GameplayAssetsLabel);

        public async UniTask Exit() =>
            await _assetProvider.ReleaseAssetsByLabel(AddressableIndents.GameplayAssetsLabel);
    }
}