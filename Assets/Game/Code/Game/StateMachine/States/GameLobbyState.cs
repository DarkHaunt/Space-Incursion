using Cysharp.Threading.Tasks;
using Game.Code.Common.StateMachineBase.Interfaces;

namespace Game.Code.Game.Core.States
{
    public class GameLobbyState : IState
    {
        public UniTask Enter() =>
            UniTask.CompletedTask;

        public UniTask Exit() =>
            UniTask.CompletedTask;
    }
}