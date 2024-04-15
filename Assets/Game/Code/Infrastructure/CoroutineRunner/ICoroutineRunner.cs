using System.Collections;

namespace Game.Code.Common.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        void RunCoroutine(IEnumerator coroutine);
        void StopRunningCoroutine(IEnumerator coroutine);
    }
}