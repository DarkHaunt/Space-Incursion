using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.Game.UI
{
    public class GameResultsView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _resultsParentObject;
        [field: SerializeField] public Button ExitButton { get; private set; }

        public UniTask Show(float duration)
        {
            return _canvasGroup.DOFade(1f, duration)
                .SetEase(Ease.Linear)
                .ToUniTask();
        }

        public UniTask Hide() =>
            _canvasGroup.DOFade(0f, 0f)
                .ToUniTask();

        public void FillWithResults(IEnumerable<PlayerResultsView> views)
        {
            var orderedTransforms = views
                .OrderBy(x => x.Score)
                .Select(x => x.transform);
            
            foreach (var t in orderedTransforms)
                t.SetParent(_resultsParentObject);
        }
    }
}