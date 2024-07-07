using System.Collections;

namespace Game.Code.Infrastructure.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        void RunCoroutine(IEnumerator coroutine);
        void StopRunningCoroutine(IEnumerator coroutine);
    }
}