using Code.Infrastructure.AssetManaging;
using Cysharp.Threading.Tasks;
using Game.Code.Common.StateMachineBase.Interfaces;

namespace Game.Code.Infrastructure.RootStateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly AssetProvider _assetProvider;

        public BootstrapState(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask Exit()
        {
            await _assetProvider.InitializeAsync();
            
            
        }

        public UniTask Enter()
        {
            throw new System.NotImplementedException();
        }
    }
}