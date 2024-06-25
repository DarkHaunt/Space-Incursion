using Game.Code.Game.Entities.Player.Services;
using Game.Code.Game.Services;
using Game.Code.Game.Input;
using VContainer.Unity;
using VContainer;

namespace Game.Code.Game.Boot.Installers
{
    public class PlayerServicesInstaller : IInstaller
    {
        private readonly UIService _uiService;

        public PlayerServicesInstaller(UIService uiService)
        {
            _uiService = uiService;
        }

        public void Install(IContainerBuilder builder)
        {
            RegisterInputService(builder);
            RegisterCameraService(builder);

            RegisterUIService(builder);
            RegisterGameOverService(builder);
            RegisterGameStartService(builder); 
            
            RegisterPlayerColorProvider(builder);
            RegisterPlayerHandleService(builder);
        }

        private void RegisterGameOverService(IContainerBuilder builder) =>
            builder.Register<GameOverService>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();

        private void RegisterUIService(IContainerBuilder builder) =>
            builder.RegisterInstance(_uiService);

        private void RegisterGameStartService(IContainerBuilder builder) =>
            builder.Register<GameStartService>(Lifetime.Scoped);

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