using Cysharp.Threading.Tasks;
using Game.Code.Common.StateMachineBase.Interfaces;

namespace Game.Code.Root.StateMachine.States
{
    public class GameState : IState
    {
        public UniTask Enter() =>
            UniTask.CompletedTask;

        public UniTask Exit() =>
            UniTask.CompletedTask;
    }
}