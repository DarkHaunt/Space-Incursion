using Cysharp.Threading.Tasks;
using Fusion;
using Game.Code.Game.Network;
using Game.Code.Infrastructure.Network;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;

namespace Game.Code.Game.StateMachine.States
{
    public class NetworkBootstrapState : IState
    {
        private readonly NetworkPlayerDataProvider _playerDataProvider;
        private readonly GameStateMachine _stateMachine;
        private readonly NetworkRunner _networkRunner;
        private readonly NetworkFacade _networkFacade;

        public NetworkBootstrapState(GameStateMachine stateMachine, NetworkFacade networkFacade, NetworkMonoServiceLocator serviceLocator, 
            NetworkPlayerDataProvider playerDataProvider)
        {
            _playerDataProvider = playerDataProvider;
            _networkFacade = networkFacade;
            _stateMachine = stateMachine;
            
            _networkRunner = serviceLocator.Runner;
        }
        
        public async UniTask Enter()
        {
            await StartGame();
            await GoToClientState();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;

        private async UniTask StartGame()
        {
            _networkRunner.AddCallbacks(_networkFacade);
            _networkRunner.ProvideInput = true;

            await _networkRunner.StartGame(_playerDataProvider.PlayerData.GameArgs);
        }

        private UniTask GoToClientState() =>
            _stateMachine.Enter<ClientBootstrapState>();
    }
}