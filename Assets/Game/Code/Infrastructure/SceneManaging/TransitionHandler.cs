using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using VContainer.Unity;

namespace Game.Code.Infrastructure.SceneManaging
{
    public class TransitionHandler : MonoBehaviour, IInitializable
    {
        private const float FadeInTime = 0.7f;
        private const float FadeOutTime = 0.35f;

        [SerializeField] private CanvasGroup _canvas;

        private CancellationTokenSource _cancellationToken;


        public void Initialize()
        {
            UnfadeImmediate();
            UpdateCancellationSource();
        }

        public void FadeImmediate()
        {
            SetInteractable(true);
            _canvas.DOFade(1f, 0f);
        }

        public void UnfadeImmediate()
        {
            SetInteractable(false);
            _canvas.DOFade(0f, 1f);
        }

        public async UniTask PlayFadeInAnimation()
        {
            SetInteractable(false);

            await PlayAnimationClip(_canvas.DOFade(1f, FadeInTime));
        }

        public async UniTask PlayFadeOutAnimation()
        {
            SetInteractable(true);

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

        private void SetInteractable(bool interactable)
        {
            _canvas.blocksRaycasts = interactable;
            _canvas.interactable = interactable;
        }

        private void UpdateCancellationSource()
        {
            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }
    }
}