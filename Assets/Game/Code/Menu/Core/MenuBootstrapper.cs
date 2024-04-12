using Game.Code.Menu.StateMachine;
using Game.Code.Menu.StateMachine.States;
using VContainer.Unity;

namespace Game.Code.Menu.Core
{
    public class MenuBootstrapper : IInitializable
    {
        private readonly MenuStateMachine _stateMachine;

        public MenuBootstrapper(MenuStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public async void Initialize()
            => await _stateMachine.Enter<MainMenu>();
    }
}