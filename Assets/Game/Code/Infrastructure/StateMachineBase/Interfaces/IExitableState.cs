using Cysharp.Threading.Tasks;

namespace Game.Code.Common.StateMachineBase.Interfaces
{
    public interface IExitableState
    {
        UniTask Exit();
    }
}