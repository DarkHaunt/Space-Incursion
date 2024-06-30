using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.Code.Game.UI
{
    public class GameDeathView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public UniTask Show(float duration)
        {
            return _canvasGroup.DOFade(1f, duration)
                .SetEase(Ease.Linear)
                .ToUniTask();
        }

        public UniTask Hide() =>
            _canvasGroup.DOFade(0f, 0f).ToUniTask();
    }
}