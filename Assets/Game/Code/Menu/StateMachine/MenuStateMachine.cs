using Game.Scripts.Menu.StateMachine.States;
using Code.Infrastructure.StateMachineBase;

namespace Game.Scripts.Menu.StateMachine
{
    public class MenuStateMachine : Code.Infrastructure.StateMachineBase.StateMachine
    {
        private IState _state;

        public MenuStateMachine(StartGame.Factory joinRoomFactory, MainMenu.Factory mainMenuFactory, 
            LoadGame.Factory loadBootFactory, ExitGame.Factory exitGameFactory)
        {
            RegisterState(mainMenuFactory.CreateState(this));
            RegisterState(joinRoomFactory.CreateState(this));
            RegisterState(loadBootFactory.CreateState());
            RegisterState(exitGameFactory.CreateState());
        }
    }
}
