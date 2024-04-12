using Game.Code.Menu.StateMachine;
using Game.Code.Menu.StateMachine.States;
using Game.Code.Menu.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Code.Menu.Core
{
    public class MenuInstaller : LifetimeScope
    {
        [Header("--- Views ---")]
        [SerializeField] private MenuView _menuView;
        [SerializeField] private StartGameView _startGameView;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterBootstrapper(builder);
            RegisterViews(builder);
            
            RegisterStateMachine(builder);
            
            RegisterMenuState(builder);
            RegisterStartGameState(builder);
            RegisterLoadGameState(builder);
            RegisterExitGameState(builder);
        }

        private void RegisterBootstrapper(IContainerBuilder builder)
        {
            builder
                .Register<MenuBootstrapper>(Lifetime.Scoped)
                .AsImplementedInterfaces();
        }

        private void RegisterViews(IContainerBuilder builder)
        {
            builder.RegisterInstance(_menuView);
            builder.RegisterInstance(_startGameView);
        }

        private void RegisterStateMachine(IContainerBuilder builder) =>
            builder.Register<MenuStateMachine>(Lifetime.Scoped);

        private void RegisterMenuState(IContainerBuilder builder)
        {
            builder.Register<MainMenu.Factory>(Lifetime.Scoped);
            builder.RegisterFactory<MenuStateMachine, MainMenu>(container =>
                container.Resolve<MainMenu.Factory>().CreateState, Lifetime.Scoped);
        }

        private void RegisterStartGameState(IContainerBuilder builder)
        {
            builder.Register<StartGame.Factory>(Lifetime.Scoped);
            builder.RegisterFactory<MenuStateMachine, StartGame>(container =>
                container.Resolve<StartGame.Factory>().CreateState, Lifetime.Scoped);
        }

        private void RegisterLoadGameState(IContainerBuilder builder)
        {
            builder.Register<LoadGame.Factory>(Lifetime.Scoped);
            builder.RegisterFactory<LoadGame>(container =>
                container.Resolve<LoadGame.Factory>().CreateState, Lifetime.Scoped);
        }

        private void RegisterExitGameState(IContainerBuilder builder)
        {
            builder.Register<ExitGame.Factory>(Lifetime.Scoped);
            builder.RegisterFactory<ExitGame>(container =>
                container.Resolve<ExitGame.Factory>().CreateState, Lifetime.Scoped);
        }
    }
}