using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;
using UnityEngine;

namespace Game.Code.Infrastructure.SceneManaging
{
    public class TransitionHandler : MonoBehaviour
    {
        private const float FadeInTime = 0.7f;
        private const float FadeOutTime = 0.35f;

        [SerializeField] private CanvasGroup _canvas;
        
        private CancellationTokenSource _cancellationToken;


        private void Awake() =>
            UpdateCancellationSource();

        public void FadeImmediate() =>
            _canvas.DOFade(1f, 0f);

        public async UniTask PlayFadeInAnimation()
        {
            _canvas.interactable = true;
            
            await PlayAnimationClip(_canvas.DOFade(1f, FadeInTime));
        }

        public async UniTask PlayFadeOutAnimation()
        {
            _canvas.interactable = false;
            
            await PlayAnimationClip(_canvas.DOFade(0f, FadeOutTime));
        }

        public void CancelTransition()
            => _cancellationToken?.Cancel();

        private async UniTask PlayAnimationClip(Tween animationTween)
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                UpdateCancellationSource();
                
                await UniTask.FromCanceled(_cancellationToken.Token);
            }

            await animationTween.AsyncWaitForCompletion();
        }

        private void UpdateCancellationSource()
        {
            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }
    }
}