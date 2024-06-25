using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using Game.Code.Infrastructure.StateMachineBase.Interfaces;

namespace Game.Code.Game.StateMachine.States
{
    public class LoseState : IState
    {
        private readonly UIService _uiService;
        private readonly CameraService _cameraService;

        public LoseState(UIService uiService, CameraService cameraService)
        {
            _uiService = uiService;
            _cameraService = cameraService;
        }
        
        public UniTask Enter()
        {
            _uiService.ShowDeathScreen();
            _cameraService.CancelFollow();

            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _uiService.HideDeathScreen();
            
            return UniTask.CompletedTask;
        }
    }
}