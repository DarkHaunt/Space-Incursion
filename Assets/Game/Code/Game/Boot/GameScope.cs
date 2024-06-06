using System.Collections.Generic;
using Game.Code.Game.Services;
using Game.Code.Game.Scene;
using Game.Code.Game.Core;
using VContainer.Unity;
using UnityEngine;
using VContainer;

namespace Game.Code.Game.Boot
{
    public class GameScope : LifetimeScope
    {
        [Header("--- Scene Dependencies ---")]
        [SerializeField] private Camera _inputCamera;
        [SerializeField] private CameraService _cameraService;

        [Header("--- Points ---")]
        [SerializeField] private Transform _uIParent;
        [SerializeField] private List<Transform> _playerSpawnPoints;
        

        protected override void Configure(IContainerBuilder builder)
        {
            // TODO: Make Installers for readability
            
            RegisterBootstrapper(builder);
            RegisterStateMachine(builder);

            RegisterInputService(builder);
            RegisterCameraService(builder);
            RegisterPhysicCollisionService(builder);
            RegisterSceneDependenciesProvider(builder);
            
            RegisterNetworkFacade(builder);
            RegisterNetworkSpawnService(builder);
            
            RegisterPlayerColorProvider(builder); 
            RegisterPlayerHandleService(builder);

            RegisterEnemiesHandleService(builder);
            RegisterEnemiesPositionProvider(builder);
            RegisterEnemiesSpawnPossibilityProvider(builder);
        }

        private void RegisterCameraService(IContainerBuilder builder)
        {
            builder.RegisterInstance(_cameraService);
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

        private void RegisterPlayerHandleService(IContainerBuilder builder) =>
            builder.Register<PlayerHandleService>(Lifetime.Scoped);

        private void RegisterPhysicCollisionService(IContainerBuilder builder) =>
            builder.Register<PhysicCollisionService>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();

        private void RegisterPlayerColorProvider(IContainerBuilder builder) =>
            builder.Register<PlayerColorProvider>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();

        private void RegisterSceneDependenciesProvider(IContainerBuilder builder) =>
            builder.Register<SceneDependenciesProvider>(Lifetime.Scoped)
                .WithParameter(_playerSpawnPoints)
                .WithParameter(_inputCamera)
                .WithParameter(_uIParent);

        private void RegisterNetworkFacade(IContainerBuilder builder) =>
            builder.Register<NetworkFacade>(Lifetime.Scoped);

        private void RegisterNetworkSpawnService(IContainerBuilder builder) =>
            builder.Register<NetworkSpawnService>(Lifetime.Scoped);

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