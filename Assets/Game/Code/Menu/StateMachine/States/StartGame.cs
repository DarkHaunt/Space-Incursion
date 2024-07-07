using Game.Code.Root.StateMachine.States;
using Game.Code.Root.StateMachine;
using Cysharp.Threading.Tasks;
using Game.Code.Game;
using Game.Code.Infrastructure.Network;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using Game.Code.Menu.MVP;

namespace Game.Code.Menu.StateMachine.States
{
    public class StartGame : IState
    {
        private readonly RootStateMachine _rootStateMachine;

        private readonly NetworkPlayerDataProvider _networkPlayerDataProvider;
        private readonly MenuModel _model;

        public StartGame(RootStateMachine rootStateMachine, NetworkPlayerDataProvider networkPlayerDataProvider, MenuModel model)
        {
            _rootStateMachine = rootStateMachine;

            _networkPlayerDataProvider = networkPlayerDataProvider;
            _model = model;
        }

        public async UniTask Enter()
        {
            _networkPlayerDataProvider.SetPlayerData
            (
                roomName: _model.RoomName,
                nickName: _model.PlayerName
            );
            
            await _rootStateMachine.Enter<FusionNetworkBootstrapState>();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;
    }
}