using Game.Code.Infrastructure.StateMachineBase.Interfaces;
using Game.Code.Game.Entities.Enemies.Services;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Game.Network;
using Game.Code.Game.UI;
using UniRx;

namespace Game.Code.Game.StateMachine.States
{
    public class GameOverState : IPaylodedState<GameResultsData>
    {
        private readonly CompositeDisposable _disposables = new();

        private readonly NetworkHostStateHandleService _hostStateHandleService;
        private readonly EnemyHandleService _enemyHandleService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly CameraService _cameraService;
        private readonly GameFactory _gameFactory;
        private readonly UIService _uiService;

        public GameOverState(GameStateMachine gameStateMachine, UIService uiService, CameraService cameraService, GameFactory gameFactory, 
            EnemyHandleService enemyHandleService, NetworkHostStateHandleService hostStateHandleService)
        {
            _hostStateHandleService = hostStateHandleService;
            _enemyHandleService = enemyHandleService;
            _gameStateMachine = gameStateMachine;
            _cameraService = cameraService;
            _gameFactory = gameFactory;
            _uiService = uiService;
        }

        public async UniTask Enter(GameResultsData payload)
        {
            _cameraService.CancelFollow();
            
            if (_hostStateHandleService.IsHost)
            {
                _enemyHandleService.StopSpawning();
                _enemyHandleService.KillAllExistingEnemies();
            }
            
            var resultViews = await CreateResultViews(payload);

            _uiService.ShowGameOverPanel(resultViews);
            _uiService.OnExitButtonClick
                .Subscribe(_ => GoToMenuScene())
                .AddTo(_disposables);
        }

        public UniTask Exit()
        {
            _disposables.Dispose();
            
            return UniTask.CompletedTask;
        }

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

        private void GoToMenuScene() =>
            _gameStateMachine.Enter<ShutdownState>().Forget();
    }
}