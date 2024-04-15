using Code.Infrastructure.AssetManaging;
using Cysharp.Threading.Tasks;
using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Game.StaticData.Indents;

namespace Game.Code.Root.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly RootStateMachine _stateMachine;
        private readonly AssetProvider _assetProvider;

        public BootstrapState(RootStateMachine stateMachine, AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            await PrewarmAssets();

            await _stateMachine.Enter<GameState>();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;

        private async UniTask PrewarmAssets()
        {
            await _assetProvider.InitializeAsync();
            
            await _assetProvider.WarmupAssetsByLabel(AddressableLabels.PlayerLabel);
            await _assetProvider.WarmupAssetsByLabel(AddressableLabels.EnemyLabel);
        }
    }
}