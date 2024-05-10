using Game.Code.Common.StateMachineBase.Interfaces;
using Cysharp.Threading.Tasks;
using Fusion;
using Game.Code.Game;
using Game.Code.Game.Services;
using Game.Code.Game.StaticData.Indents;
using Game.Code.Menu.View;

namespace Game.Code.Menu.StateMachine.States
{
    public class StartGame : IState
    {
        private readonly NetworkServiceLocator _networkServiceLocator;
        private readonly NetworkSceneLoader _sceneLoader;
        private readonly MenuStateMachine _stateMachine;
        private readonly MenuModel _model;

        public StartGame(MenuStateMachine stateMachine, MenuModel model, 
            NetworkServiceLocator networkServiceLocator, NetworkSceneLoader sceneLoader)
        {
            _networkServiceLocator = networkServiceLocator;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _model = model;
        }

        public UniTask Enter()
        {
            
            
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }

        private StartGameArgs GetGameStartGameArgs()
        {
            return new StartGameArgs
            {
                GameMode = GameMode.AutoHostOrClient,
                PlayerCount = GameIndents.PlayerCount,
                SessionName = _model.RoomName,
            };
        }
        
        private void SetMainMenuState()
        {
            _stateMachine.Enter<MainMenu>().Forget();
        }
    }
}