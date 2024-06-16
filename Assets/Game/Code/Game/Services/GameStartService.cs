using Game.Code.Game.StaticData.Indents;
using Game.Code.Game.UI;
using System;
using UniRx;

namespace Game.Code.Game.Services
{
    public class GameStartService
    {
        private GameStartView _view;
        
        public IObservable<Unit> OnGameStartButtonClick { get; private set; }


        public void Init(GameStartView view)
        {
            _view = view;
            _view.EnableStartButton(false);

            OnGameStartButtonClick = _view.StartButton.OnClickAsObservable();
        }

        public void UpdateStartCondition(int playerCount)
        {
            _view.SetPlayersCount(playerCount, NetworkIndents.MaxPlayersCount);
            
            CheckForGameStartAbility(playerCount);
        }

        private void CheckForGameStartAbility(int playerCount) =>
            _view.EnableStartButton(playerCount >= NetworkIndents.MinPlayersCount);
    }
}