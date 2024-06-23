using Cysharp.Threading.Tasks;
using Game.Code.Game.StaticData.Indents;
using Game.Code.Infrastructure.AssetManaging;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using TMPro;

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