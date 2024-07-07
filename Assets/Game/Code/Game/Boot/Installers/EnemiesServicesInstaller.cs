using Game.Code.Game.Entities.Enemies.Services;
using Game.Code.Game.Services;
using VContainer;
using VContainer.Unity;

namespace Game.Code.Game.Boot.Installers
{
    public class EnemiesServicesInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            RegisterEnemiesHandleService(builder);
            RegisterEnemiesPositionProvider(builder);
            RegisterEnemiesSpawnPossibilityProvider(builder);
        }
        
        private void RegisterEnemiesSpawnPossibilityProvider(IContainerBuilder builder)
        {
            builder.Register<EnemySpawnPossibilityProvider>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();
        }

        private void RegisterEnemiesHandleService(IContainerBuilder builder) =>
            builder.Register<EnemyHandleService>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();

        private void RegisterEnemiesPositionProvider(IContainerBuilder builder) =>
            builder.Register<EnemyPositionProvider>(Lifetime.Scoped);
    }
}