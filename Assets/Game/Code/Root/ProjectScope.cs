using Game.Code.Infrastructure.SceneManaging;
using Code.Infrastructure.AssetManaging;
using Code.Infrastructure.UpdateRunner;
using Game.Code.Common.StateMachineBase;
using Game.Code.Common.CoroutineRunner;
using Game.Code.Root.StateMachine;
using Game.Code.Game.StaticData;
using Game.Code.Game.Services;
using VContainer.Unity;
using Game.Code.Game;
using UnityEngine;
using VContainer;

namespace Game.Code.Root
{
    public class ProjectScope : LifetimeScope
    {
        [Header("--- Services ---")]
        [SerializeField] private TransitionHandler _transitionHandler;
        [SerializeField] private CoroutineRunner _coroutineRunner;

        [Header("--- Network ---")]
        [SerializeField] private NetworkMonoServiceLocator _networkServices;


        protected override void Configure(IContainerBuilder builder)
        {
            RegisterStateFactory(builder);
            RegisterCoroutineRunner(builder);
            
            RegisterBootstrapper(builder);
            RegisterRootStateMachine(builder);
            
            RegisterStaticDataProvider(builder);
            RegisterNetworkArgsProvider(builder);
            RegisterNetworkServiceLocator(builder);

            RegisterUpdateRunner(builder);
            RegisterAssetProvider(builder);
            RegisterSceneLoaderSystem(builder);
        }

        private void RegisterNetworkArgsProvider(IContainerBuilder builder) =>
            builder.Register<NetworkPlayerDataProvider>(Lifetime.Singleton);

        private void RegisterBootstrapper(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<ProjectBootstrapper>();

        private void RegisterStaticDataProvider(IContainerBuilder builder) =>
            builder.Register<GameStaticDataProvider>(Lifetime.Singleton);

        private void RegisterUpdateRunner(IContainerBuilder builder)
        {
            builder
                .Register<UpdateRunner>(Lifetime.Singleton)
                .As<ITickSource, ITickable>();
        }

        private void RegisterAssetProvider(IContainerBuilder builder) =>
            builder.Register<AssetProvider>(Lifetime.Singleton);

        private void RegisterStateFactory(IContainerBuilder builder) =>
            builder.Register<StateFactory>(Lifetime.Transient);

        private void RegisterRootStateMachine(IContainerBuilder builder) =>
            builder.Register<RootStateMachine>(Lifetime.Singleton);

        private void RegisterNetworkServiceLocator(IContainerBuilder builder)
        {
            builder
                .RegisterComponentInNewPrefab(_networkServices, Lifetime.Singleton)
                .DontDestroyOnLoad();
        }

        private void RegisterCoroutineRunner(IContainerBuilder builder)
        {
            builder
                .RegisterComponentInNewPrefab(_coroutineRunner, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<ICoroutineRunner>();
        }

        private void RegisterSceneLoaderSystem(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);

            builder
                .RegisterComponentInNewPrefab(_transitionHandler, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}