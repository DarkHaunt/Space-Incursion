using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;

namespace Game.Code.Game.StateMachine.States
{
    public class GameOverState : IPaylodedState<GameResultsData>
    {
        private readonly CameraService _cameraService;
        private readonly UIService _uiService;

        public GameOverState(UIService uiService, CameraService cameraService)
        {
            _uiService = uiService;
            _cameraService = cameraService;
        }

        public UniTask Enter(GameResultsData payload)
        {
            _uiService.ShowGameOverPanel(payload);
            _cameraService.CancelFollow();

            return UniTask.CompletedTask;
        }

        public UniTask Exit() => 
            UniTask.CompletedTask;
    }
}