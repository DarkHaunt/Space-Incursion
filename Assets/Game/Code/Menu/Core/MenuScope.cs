using Game.Code.Menu.MVP;
using Game.Code.Menu.StateMachine;
using VContainer.Unity;
using UnityEngine;
using VContainer;

namespace Game.Code.Menu.Core
{
    public class MenuScope : LifetimeScope
    {
        [SerializeField] private MenuView _menuView;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterBootstrapper(builder);
            
            RegisterMVPComponents(builder);
            RegisterStateMachine(builder);
        }

        private void RegisterBootstrapper(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<MenuBootstrapper>(Lifetime.Scoped);

        private void RegisterMVPComponents(IContainerBuilder builder)
        {
            builder.Register<MenuPresenter>(Lifetime.Scoped)
                .AsImplementedInterfaces();
            
            builder.Register<MenuModel>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.RegisterInstance(_menuView);
        }

        private void RegisterStateMachine(IContainerBuilder builder) =>
            builder.Register<MenuStateMachine>(Lifetime.Scoped); 
    }
}