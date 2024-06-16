using Game.Code.Game.Services;
using VContainer;
using VContainer.Unity;

namespace Game.Code.Game.Boot.Installers
{
    public class PlayerServicesInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            RegisterInputService(builder);
            RegisterCameraService(builder);
            RegisterPlayerColorProvider(builder);
            RegisterPlayerHandleService(builder);
        }

        private void RegisterCameraService(IContainerBuilder builder)
        {
            builder.Register<CameraService>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();
        }

        private void RegisterPlayerHandleService(IContainerBuilder builder) =>
            builder.Register<PlayerHandleService>(Lifetime.Scoped);

        private void RegisterPlayerColorProvider(IContainerBuilder builder) =>
            builder.Register<PlayerColorProvider>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();

        private void RegisterInputService(IContainerBuilder builder)
        {
            builder
                .Register<InputService>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}