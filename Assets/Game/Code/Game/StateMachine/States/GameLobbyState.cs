using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Infrastructure.SceneManaging;
using Cysharp.Threading.Tasks;

namespace Game.Code.Game.Core.States
{
    public class GameLobbyState : IState
    {
        private readonly TransitionHandler _transitionHandler;

        public GameLobbyState(TransitionHandler transitionHandler)
        {
            _transitionHandler = transitionHandler;
        }
        
        public UniTask Enter() =>
            _transitionHandler.PlayFadeOutAnimation();

        public UniTask Exit() =>
            UniTask.CompletedTask;
    }
}