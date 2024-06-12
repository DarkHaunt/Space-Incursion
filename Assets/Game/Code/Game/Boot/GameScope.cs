using Game.Code.Game.Boot.Installers;
using System.Collections.Generic;
using Game.Code.Game.Services;
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
        [SerializeField] private Transform _uIParent;
        [SerializeField] private CameraService _cameraService;
        [SerializeField] private List<Transform> _playerSpawnPoints;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterBootstrapper(builder);
            RegisterStateMachine(builder);

            new SceneServicesInstaller(_inputCamera, _uIParent, _cameraService, _playerSpawnPoints)
                .Install(builder);

            new NetworkServicesInstaller()
                .Install(builder);

            new PlayerServicesInstaller()
                .Install(builder);
            
            new EnemiesServicesInstaller()
                .Install(builder);
        }

        private void RegisterStateMachine(IContainerBuilder builder) =>
            builder.Register<GameStateMachine>(Lifetime.Scoped);

        private void RegisterBootstrapper(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<GameBootstrapper>(Lifetime.Scoped);
    }
}