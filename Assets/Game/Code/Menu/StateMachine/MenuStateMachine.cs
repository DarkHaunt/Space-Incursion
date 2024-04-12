using Game.Code.Common.StateMachineBase;
using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Menu.StateMachine.States;

namespace Game.Code.Menu.StateMachine
{
    public class MenuStateMachine : BaseStateMachine
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
