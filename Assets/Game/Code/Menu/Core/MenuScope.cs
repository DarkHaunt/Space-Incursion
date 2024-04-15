using Game.Code.Menu.StateMachine;
using Game.Code.Menu.UI;
using VContainer.Unity;
using UnityEngine;
using VContainer;

namespace Game.Code.Menu.Core
{
    public class MenuScope : LifetimeScope
    {
        [Header("--- Views ---")]
        [SerializeField] private MenuView _menuView;
        [SerializeField] private StartGameView _startGameView;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterBootstrapper(builder);
            
            RegisterViews(builder);
            RegisterStateMachine(builder);
        }

        private void RegisterBootstrapper(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<MenuBootstrapper>(Lifetime.Scoped);

        private void RegisterViews(IContainerBuilder builder)
        {
            builder.RegisterInstance(_menuView);
            builder.RegisterInstance(_startGameView);
        }

        private void RegisterStateMachine(IContainerBuilder builder) =>
            builder.Register<MenuStateMachine>(Lifetime.Scoped);
    }
}