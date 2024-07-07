using Game.Code.Game.StaticData.Indents;
using Game.Code.Infrastructure.Network;
using Game.Code.Game.UI;
using Fusion;

namespace Game.Code.Game.Services
{
    public class GameStartService
    {
        private NetworkSignalBus _signal;
        private NetworkRunner _runner;
        private GameStartView _view;


        public void Init(GameStartView view, NetworkRunner runner)
        {
            _runner = runner;
            _view = view;
            
            _view.EnableStartButton(false);
            _view.StartButton.onClick.AddListener(RPC_ClickedButton);
        }

        public void UpdateStartCondition(int playerCount)
        {
            _view.SetPlayersCount(playerCount, NetworkIndents.MaxPlayersCount);
            
            CheckForGameStartAbility(playerCount);
        }

        public void HideView()
        {
            if (_view != null) 
                _view.Hide();
        }

        private void RPC_ClickedButton() =>
            NetworkSignalBus.RPC_GameStarted(_runner);

        private void CheckForGameStartAbility(int playerCount) =>
            _view.EnableStartButton(playerCount >= NetworkIndents.MinPlayersCount);
    }
}