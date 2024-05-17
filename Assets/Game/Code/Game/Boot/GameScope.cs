using Game.Code.Game.Core;
using Game.Code.Game.Scene;
using Game.Code.Game.Services;
using VContainer.Unity;
using UnityEngine;
using VContainer;

namespace Game.Code.Game.Boot
{
    public class GameScope : LifetimeScope
    {
        [Header("--- Scene Dependencies ---")]
        [SerializeField] private Camera _inputCamera;
        [SerializeField] private Transform _uIParent; 
        

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterBootstrapper(builder);
            RegisterStateMachine(builder);

            RegisterInputService(builder);
            RegisterSceneDependenciesProvider(builder);
            
            RegisterNetworkFacade(builder);
            RegisterNetworkHostService(builder);
            RegisterPlayerColorProvider(builder);
        }

        private void RegisterPlayerColorProvider(IContainerBuilder builder) =>
            builder.Register<PlayerColorProvider>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();

        private void RegisterSceneDependenciesProvider(IContainerBuilder builder) =>
            builder.Register<SceneDependenciesProvider>(Lifetime.Scoped)
                .WithParameter(_inputCamera)
                .WithParameter(_uIParent);

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
                .AsSelf();
        }

        private void RegisterBootstrapper(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<GameBootstrapper>(Lifetime.Scoped);
    }
}