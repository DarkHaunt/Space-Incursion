using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Services;
using DG.Tweening;
using UnityEngine;

namespace Game.Code.Game.UI
{
    public class GameResultsView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _resultsParentObject;
        

        public UniTask Show(float duration)
        {
            return _canvasGroup.DOFade(1f, duration)
                .SetEase(Ease.Linear)
                .ToUniTask();
        }

        public UniTask Hide() =>
            _canvasGroup
                .DOFade(0f, 0f)
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