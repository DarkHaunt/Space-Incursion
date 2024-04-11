using Code.Infrastructure.StateMachineBase;
using Cysharp.Threading.Tasks;
using Game.Scripts.Menu.UI;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class MainMenu : IState
    {
        private readonly MenuStateMachine _stateMachine;
        private readonly MenuView _view;
        

        public MainMenu(MenuStateMachine stateMachine, MenuView view)
        {
            _stateMachine = stateMachine;
            _view = view;
        }
        
        
        public UniTask Enter()
        {
            _view.Enable(true);
            _view.StartButton.onClick.AddListener(SetStartGameState);
            _view.ExitButton.onClick.AddListener(SetExitGameState);
            
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _view.Enable(false);
            _view.StartButton.onClick.RemoveListener(SetStartGameState);
            _view.ExitButton.onClick.RemoveListener(SetExitGameState);
            
            return UniTask.CompletedTask;
        }
        
        private void SetStartGameState()
            => _stateMachine.Enter<StartGame>();

        private void SetExitGameState()
            => _stateMachine.Enter<ExitGame>();
        
        public class Factory
        {
            private readonly MenuView _view;

            public Factory(MenuView view) =>
                _view = view;

            public MainMenu CreateState(MenuStateMachine stateMachine) =>
                new (stateMachine, _view);
        }
    }
}