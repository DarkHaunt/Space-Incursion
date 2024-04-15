using Code.Infrastructure.AssetManaging;
using Game.Code.Infrastructure.SceneManaging;
using Game.Code.Common.StateMachineBase;
using Game.Code.Common.CoroutineRunner;
using Game.Code.Boot.StateMachine;
using VContainer.Unity;
using UnityEngine;
using VContainer;

namespace Game.Code.Boot
{
    public class ProjectInstaller : LifetimeScope
    {
        [SerializeField] private SceneTransitionHandler _transitionHandler;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterBootstrapper(builder);
            
            RegisterStateFactory(builder);
            RegisterRootStateMachine(builder);
            
            RegisterAssetProvider(builder);
            RegisterCoroutineRunner(builder);
            RegisterSceneLoaderSystem(builder);
        }

        private void RegisterAssetProvider(IContainerBuilder builder) =>
            builder.Register<AssetProvider>(Lifetime.Singleton);

        private void RegisterBootstrapper(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<ProjectBootstrapper>();

        private void RegisterStateFactory(IContainerBuilder builder) =>
            builder.Register<StateFactory>(Lifetime.Singleton);

        private void RegisterRootStateMachine(IContainerBuilder builder) =>
            builder.Register<RootStateMachine>(Lifetime.Singleton);

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