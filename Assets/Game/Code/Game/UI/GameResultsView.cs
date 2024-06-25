using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using DG.Tweening;
using UnityEngine;

namespace Game.Code.Game.UI
{
    public class GameResultsView : MonoBehaviour
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

        public void FillWithResults(GameResultsData results)
        {
            foreach (var player in results.PlayersWithScore)
                Debug.Log($"<color=white>{player.Key.Nickname.ToString()} - {player.Value}</color>");
        }
    }
}