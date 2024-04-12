using Cysharp.Threading.Tasks;
using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Menu.UI;

namespace Game.Code.Menu.StateMachine.States
{
    public class StartGame : IState
    {
        private readonly MenuStateMachine _stateMachine;
        private readonly StartGameView _view;

        public StartGame(MenuStateMachine stateMachine, StartGameView view)
        {
            _stateMachine = stateMachine;
            _view = view;
        }

        public UniTask Enter()
        {
            _view.Enable(true);
            _view.StartButton.onClick.AddListener(SetLoadGame);
            _view.CancelButton.onClick.AddListener(SetMainMenuState);

            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _view.Enable(false);
            _view.StartButton.onClick.RemoveListener(SetLoadGame);
            _view.CancelButton.onClick.RemoveListener(SetMainMenuState);
            
            return UniTask.CompletedTask;
        }

        private void SetLoadGame()
            => _stateMachine.Enter<LoadGame>().Forget();

        private void SetMainMenuState()
            => _stateMachine.Enter<MainMenu>().Forget();

        public class Factory
        {
            private readonly StartGameView _view;

            public Factory(StartGameView view) =>
                _view = view;

            public StartGame CreateState(MenuStateMachine stateMachine) =>
                new (stateMachine, _view);
        }
    }
}