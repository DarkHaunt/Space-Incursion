using Cysharp.Threading.Tasks;

namespace Game.Code.Infrastructure.StateMachineBase.Interfaces
{
    public interface IExitableState
    {
        UniTask Exit();
    }
}