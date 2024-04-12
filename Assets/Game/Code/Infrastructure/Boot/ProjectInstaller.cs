using Game.Code.Common.CoroutineRunner;
using Game.Code.Infrastructure.SceneManaging;
using VContainer.Unity;
using UnityEngine;
using VContainer;

namespace Game.Code.Infrastructure.Boot
{
    public class ProjectInstaller : LifetimeScope
    {
        [SerializeField] private SceneTransitionHandler _transitionHandler;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCoroutineRunner(builder);
            RegisterSceneLoaderSystem(builder);
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
            builder
                .Register<SceneLoader>(Lifetime.Singleton);

            builder
                .RegisterComponentInNewPrefab(_transitionHandler, Lifetime.Singleton)
                .DontDestroyOnLoad();
        }
    }
}