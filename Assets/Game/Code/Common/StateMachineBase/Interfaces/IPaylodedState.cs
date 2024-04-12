using Cysharp.Threading.Tasks;

namespace Game.Code.Common.StateMachineBase.Interfaces
{
    public interface IPaylodedState<in TPayload> : IExitableState
    {
        UniTask Enter(TPayload payload);
    }
}