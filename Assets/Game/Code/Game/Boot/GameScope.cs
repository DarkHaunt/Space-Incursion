using Game.Code.Game.Services;
using VContainer.Unity;
using UnityEngine;
using VContainer;

namespace Game.Code.Game.Boot
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private NetworkTickService _tickService;
        [SerializeField] private Camera _inputCamera;
        

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterBootstrapper(builder);

            RegisterTickService(builder);
            RegisterGameFactory(builder);
            RegisterInputService(builder);
            RegisterNetworkService(builder);
        }

        private void RegisterTickService(IContainerBuilder builder)
        {
            builder.RegisterInstance(_tickService);
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