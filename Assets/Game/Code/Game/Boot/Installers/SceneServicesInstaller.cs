using System.Collections.Generic;
using Game.Code.Game.Scene;
using Game.Code.Game.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Code.Game.Boot.Installers
{
    public class SceneServicesInstaller : IInstaller
    {
        private readonly Camera _inputCamera;
        private readonly Transform _uIParent;
        private readonly CameraService _cameraService;
        private readonly List<Transform> _playerSpawnPoints;

        public SceneServicesInstaller(Camera inputCamera, Transform uIParent, CameraService cameraService, List<Transform> playerSpawnPoints)
        {
            _playerSpawnPoints = playerSpawnPoints;
            _cameraService = cameraService;
            _inputCamera = inputCamera;
            _uIParent = uIParent;
        }

        public void Install(IContainerBuilder builder)
        {
            RegisterPhysicCollisionService(builder);
            RegisterSceneDependenciesProvider(builder);
        }

        private void RegisterPhysicCollisionService(IContainerBuilder builder) =>
            builder.Register<PhysicCollisionService>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();


        private void RegisterSceneDependenciesProvider(IContainerBuilder builder) =>
            builder.Register<SceneDependenciesProvider>(Lifetime.Scoped)
                .WithParameter(_playerSpawnPoints)
                .WithParameter(_cameraService)
                .WithParameter(_inputCamera)
                .WithParameter(_uIParent);
    }
}