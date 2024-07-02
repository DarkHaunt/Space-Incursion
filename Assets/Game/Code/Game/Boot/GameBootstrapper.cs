using Game.Code.Game.StateMachine.States;
using Cysharp.Threading.Tasks;
using VContainer.Unity;
using System.Threading;
using System;
using Fusion;
using Game.Code.Game.Network;
using Game.Code.Game.StateMachine;
using Game.Code.Infrastructure.Network;
using Game.Code.Infrastructure.StateMachineBase;
using VContainer;

namespace Game.Code.Game.Boot
{
    public class GameBootstrapper : IAsyncStartable, IDisposable
    {
        private readonly GameStateMachine _stateMachine;
        private readonly StateFactory _stateFactory;
        
        private readonly NetworkFacade _networkFacade;
        private readonly NetworkRunner _networkRunner;


        public GameBootstrapper(NetworkMonoServiceLocator networkServiceLocator, GameStateMachine stateMachine, NetworkFacade networkFacade, 
            StateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;

            _networkFacade = networkFacade;
            _networkRunner = networkServiceLocator.Runner;
        }


        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _networkRunner.AddCallbacks(_networkFacade);

            _stateMachine.RegisterState(_stateFactory.Create<NetworkBootstrapState>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<ClientBootstrapState>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<HostBootstrapState>(Lifetime.Scoped));
            
            _stateMachine.RegisterState(_stateFactory.Create<LobbyState>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<GameState>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<LoseState>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<GameOverState>(Lifetime.Scoped));
            
            _stateMachine.RegisterState(_stateFactory.Create<ShutdownState>(Lifetime.Scoped));

            await _stateMachine.Enter<NetworkBootstrapState>();
        }

        public void Dispose() =>
            _networkRunner.RemoveCallbacks(_networkFacade);
    }
}