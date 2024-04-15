using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Code.Common.StateMachineBase;
using Game.Code.Root.StateMachine;
using Game.Code.Root.StateMachine.States;
using VContainer;
using VContainer.Unity;

namespace Game.Code.Root
{
    public class ProjectBootstrapper : IAsyncStartable
    {
        private readonly RootStateMachine _stateMachine;
        private readonly StateFactory _stateFactory;

        public ProjectBootstrapper(RootStateMachine stateMachine, StateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            SetUpStateMachine();
            
            await _stateMachine.Enter<BootstrapState>();
        }

        private void SetUpStateMachine()
        {
            _stateMachine.RegisterState(_stateFactory.Create<BootstrapState>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<GameState>(Lifetime.Scoped));
        }
    }
}