using Game.Code.Menu.StateMachine.States;
using Game.Code.Common.StateMachineBase;
using Game.Code.Menu.StateMachine;
using Cysharp.Threading.Tasks;
using System.Threading;
using VContainer;
using VContainer.Unity;

namespace Game.Code.Menu.Core
{
    public class MenuBootstrapper : IAsyncStartable
    {
        private readonly MenuStateMachine _stateMachine;
        private readonly StateFactory _stateFactory;

        public MenuBootstrapper(MenuStateMachine stateMachine, StateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;
        }

        public async void Initialize()
            => await _stateMachine.Enter<MainMenu>();

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            SetUpStateMachine();

            await _stateMachine.Enter<MainMenu>();
        }

        private void SetUpStateMachine()
        {
            _stateMachine.RegisterState(_stateFactory.Create<MainMenu>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<LoadGame>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<StartGame>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<ExitGame>(Lifetime.Scoped));
        }
    }
}