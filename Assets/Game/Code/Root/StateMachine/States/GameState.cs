using Code.Infrastructure.AssetManaging;
using Cysharp.Threading.Tasks;
using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Game.StaticData.Indents;
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
        
        public async UniTask Enter()
        {
            await _assetProvider.WarmupAssetsByLabel(AddressableIndents.GameplayAssetsLabel);
        }

        public async UniTask Exit() =>
            await _assetProvider.ReleaseAssetsByLabel(AddressableIndents.GameplayAssetsLabel);
    }
}