using Game.Code.Common.StateMachineBase.Interfaces;
using VContainer;

namespace Game.Code.Common.StateMachineBase
{
    public class StateFactory
    {
        private readonly IObjectResolver _objectResolver;

        public StateFactory(IObjectResolver objectResolver) => 
            _objectResolver = objectResolver;

        public TState Create<TState>(Lifetime lifetime) where TState : IExitableState
        {
            var registrationBuilder = new RegistrationBuilder(typeof(TState), lifetime);
            return (TState)_objectResolver.Resolve(registrationBuilder.Build());
        }
    }
}