using Game.Code.Game.Network;
using VContainer;
using VContainer.Unity;

namespace Game.Code.Game.Boot.Installers
{
    public class NetworkServicesInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            RegisterNetworkFacade(builder);
            RegisterNetworkPlayerHandleService(builder);
            RegisterNetworkHostStateHandleService(builder);
        }
        
        private void RegisterNetworkHostStateHandleService(IContainerBuilder builder) =>
            builder.Register<NetworkHostStateHandleService>(Lifetime.Scoped);

        private void RegisterNetworkPlayerHandleService(IContainerBuilder builder) =>
            builder.Register<NetworkPlayerHandleService>(Lifetime.Scoped);

        private void RegisterNetworkFacade(IContainerBuilder builder) =>
            builder.Register<NetworkFacade>(Lifetime.Scoped);
    }
}