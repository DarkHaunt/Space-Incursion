using Game.Code.Game.Services;
using Game.Code.Game.StaticData;
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

            RegisterGameFactory(builder);
            RegisterInputService(builder);
            
            RegisterNetworkService(builder);
            
            RegisterStaticDataProvider(builder);
        }

        private void RegisterStaticDataProvider(IContainerBuilder builder)
        {
            builder.Register<GameStaticDataProvider>(Lifetime.Scoped);
        }

        private void RegisterInputService(IContainerBuilder builder)
        {
            builder
                .Register<InputService>(Lifetime.Scoped)
                .WithParameter(_inputCamera);
        }

        private void RegisterGameFactory(IContainerBuilder builder) =>
            builder.Register<GameFactory>(Lifetime.Scoped);

        private void RegisterNetworkService(IContainerBuilder builder) =>
            builder.Register<NetworkService>(Lifetime.Scoped);

        private void RegisterBootstrapper(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<GameBootstrapper>(Lifetime.Scoped);
    }
}