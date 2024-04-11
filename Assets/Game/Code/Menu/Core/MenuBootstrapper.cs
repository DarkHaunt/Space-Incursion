using Game.Scripts.Menu.StateMachine.States;
using Game.Scripts.Menu.StateMachine;
using VContainer.Unity;

namespace Game.Scripts.Menu.Core
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