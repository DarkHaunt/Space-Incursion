using Game.Code.Common.StateMachineBase.Interfaces;
using Cysharp.Threading.Tasks;
using Game.Code.Game;
using Game.Code.Menu.View;

namespace Game.Code.Menu.StateMachine.States
{
    public class StartGame : IState
    {
        private readonly NetworkArgsProvider _networkArgsProvider;
        private readonly MenuModel _model;

        public StartGame(MenuModel model, NetworkArgsProvider networkArgsProvider)
        {
            _networkArgsProvider = networkArgsProvider;
            _model = model;
        }

        public UniTask Enter()
        {
            _networkArgsProvider.CreateNewGameArgs(_model.RoomName);
            return UniTask.CompletedTask;
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;
    }
}