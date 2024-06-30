using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Game.UI;

namespace Game.Code.Game.StateMachine.States
{
    public class GameOverState : IPaylodedState<GameResultsData>
    {
        private readonly CameraService _cameraService;
        private readonly GameFactory _gameFactory;
        private readonly UIService _uiService;

        public GameOverState(UIService uiService, CameraService cameraService, GameFactory gameFactory)
        {
            _cameraService = cameraService;
            _gameFactory = gameFactory;
            _uiService = uiService;
        }

        public async UniTask Enter(GameResultsData payload)
        {
            var resultViews = await CreateResultViews(payload);

            _uiService.ShowGameOverPanel(resultViews);
            _cameraService.CancelFollow();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;

        private async UniTask<IEnumerable<PlayerResultsView>> CreateResultViews(GameResultsData gameResultsData)
        {
            var views = new List<PlayerResultsView>(gameResultsData.PlayersWithScore.Count);

            foreach (var playerWithScore in gameResultsData.PlayersWithScore)
            {
                var view = await _gameFactory.CreatePlayerResultsView(playerWithScore);
                views.Add(view);
            }

            return views;
        }
    }
}