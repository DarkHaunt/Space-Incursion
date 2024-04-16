using Game.Code.Common.StateMachineBase.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Code.Menu.StateMachine.States
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
    }
}