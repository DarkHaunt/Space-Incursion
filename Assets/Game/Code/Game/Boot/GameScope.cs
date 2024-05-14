using Game.Code.Game.StaticData;
using Game.Code.Game.Services;
using Game.Code.Game.Core;
using VContainer.Unity;
using UnityEngine;
using VContainer;

namespace Game.Code.Game.Boot
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private Camera _inputCamera;
        

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterBootstrapper(builder);
            RegisterStateMachine(builder);
            
            RegisterInputService(builder);
            RegisterNetworkFacade(builder);
            RegisterNetworkHostService(builder);
        }
        
        private void RegisterNetworkFacade(IContainerBuilder builder) =>
            builder.Register<NetworkFacade>(Lifetime.Scoped);

        private void RegisterNetworkHostService(IContainerBuilder builder) =>
            builder.Register<NetworkHostService>(Lifetime.Scoped);

        private void RegisterStateMachine(IContainerBuilder builder) =>
            builder.Register<GameStateMachine>(Lifetime.Scoped);

        private void RegisterInputService(IContainerBuilder builder)
        {
            builder
                .Register<InputService>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf()
                .WithParameter(_inputCamera);
        }

        private void RegisterBootstrapper(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<GameBootstrapper>(Lifetime.Scoped);
    }
}