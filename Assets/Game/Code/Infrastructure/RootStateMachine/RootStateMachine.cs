using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Code.Common.StateMachineBase;
using Game.Code.Infrastructure.RootStateMachine.States;
using VContainer.Unity;

namespace Game.Code.Infrastructure.RootStateMachine
{
    public class RootStateMachine : BaseStateMachine, IAsyncStartable
    {
        public RootStateMachine()
        {
            
        }
        
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await Enter<BootstrapState>();
        }
    }
}