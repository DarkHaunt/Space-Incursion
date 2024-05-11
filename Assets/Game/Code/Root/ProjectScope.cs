using Game.Code.Infrastructure.SceneManaging;
using Code.Infrastructure.AssetManaging;
using Code.Infrastructure.UpdateRunner;
using Game.Code.Common.StateMachineBase;
using Game.Code.Common.CoroutineRunner;
using Game.Code.Root.StateMachine;
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
        [SerializeField] private NetworkServiceLocator _networkServices;


        protected override void Configure(IContainerBuilder builder)
        {
            RegisterBootstrapper(builder);
            RegisterUpdateRunner(builder);

            RegisterStateFactory(builder);
            RegisterRootStateMachine(builder);

            RegisterNetworkServices(builder);
            RegisterNetworkSceneLoader(builder);
            RegisterNetworkArgsProvider(builder);

            RegisterAssetProvider(builder);
            RegisterCoroutineRunner(builder);
            RegisterSceneLoaderSystem(builder);
        }

        private void RegisterUpdateRunner(IContainerBuilder builder)
        {
            builder
                .Register<UpdateRunner>(Lifetime.Singleton)
                .As<ITickSource, ITickable>();
        }

        private void RegisterNetworkSceneLoader(IContainerBuilder builder) =>
            builder.Register<NetworkSceneLoader>(Lifetime.Singleton);

        private void RegisterNetworkArgsProvider(IContainerBuilder builder) =>
            builder.Register<NetworkArgsProvider>(Lifetime.Singleton);

        private void RegisterAssetProvider(IContainerBuilder builder) =>
            builder.Register<AssetProvider>(Lifetime.Singleton);

        private void RegisterBootstrapper(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<ProjectBootstrapper>();

        private void RegisterStateFactory(IContainerBuilder builder) =>
            builder.Register<StateFactory>(Lifetime.Transient);

        private void RegisterRootStateMachine(IContainerBuilder builder) =>
            builder.Register<RootStateMachine>(Lifetime.Singleton);

        private void RegisterNetworkServices(IContainerBuilder builder)
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
                .DontDestroyOnLoad();
        }
    }
}