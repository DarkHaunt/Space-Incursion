using Cysharp.Threading.Tasks;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using Game.Code.Menu.MVP;

namespace Game.Code.Menu.StateMachine.States
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
            _view.StartButton.onClick.AddListener(SetStartGameState);
            _view.ExitButton.onClick.AddListener(SetExitGameState);
            
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _view.StartButton.onClick.RemoveListener(SetStartGameState);
            _view.ExitButton.onClick.RemoveListener(SetExitGameState);
            
            return UniTask.CompletedTask;
        }
        
        private void SetStartGameState()
            => _stateMachine.Enter<StartGame>().Forget();

        private void SetExitGameState()
            => _stateMachine.Enter<ExitGame>().Forget();
    }
}