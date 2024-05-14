using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Root.StateMachine.States;
using Game.Code.Root.StateMachine;
using Cysharp.Threading.Tasks;
using Game.Code.Menu.View;
using Game.Code.Game;

namespace Game.Code.Menu.StateMachine.States
{
    public class StartGame : IState
    {
        private readonly RootStateMachine _rootStateMachine;
        
        private readonly NetworkStartArgsProvider _networkStartArgsProvider;
        private readonly MenuModel _model;

        public StartGame(RootStateMachine rootStateMachine, NetworkStartArgsProvider networkStartArgsProvider, MenuModel model)
        {
            _rootStateMachine = rootStateMachine;
            
            _networkStartArgsProvider = networkStartArgsProvider;
            _model = model;
        }

        public async UniTask Enter()
        {
            _networkStartArgsProvider.SetStartArgs(_model.RoomName);
            await _rootStateMachine.Enter<NetworkSetUpState>();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;
    }
}