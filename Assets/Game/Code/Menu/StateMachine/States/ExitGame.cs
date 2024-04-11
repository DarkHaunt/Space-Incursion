using Code.Infrastructure.StateMachineBase;
using Cysharp.Threading.Tasks;

namespace Game.Scripts.Menu.StateMachine.States
{
    public class ExitGame : IState
    {
        public UniTask Enter()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

            return UniTask.CompletedTask;
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;

        public class Factory
        {
            public ExitGame CreateState() =>
                new();
        }
    }
}