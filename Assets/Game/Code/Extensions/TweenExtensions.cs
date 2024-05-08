using System;
using System.Collections;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Extensions
{
    public static class TweenExtensions
    {
        public static void KillIfValid(this Tween tween, bool complete = false)
        {
            if (tween.IsValid())
                tween.Kill(complete);
        }

        public static void PlayIfValid(this Tween tween)
        {
            if (tween.IsValid())
                tween.Play();
        }

        public static void PauseIfValid(this Tween tween)
        {
            if (tween.IsValid() && tween.IsPlaying())
                tween.Pause();
        }

        public static bool IsValid(this Tween tween)
            => tween?.IsActive() ?? false;

        public static Tweener DOVertexPosition(this LineRenderer lineRenderer, int index, Vector3 targetPosition, float duration)
        {
            var currentPosition = lineRenderer.GetPosition(index);

            return DOTween.To(() => currentPosition, x => lineRenderer.SetPosition(index, x), targetPosition, duration);
        }

        public static Tween DOMoveWithSpeed(this Transform transform, Vector3 position, float speed)
        {
            var time = Vector3.Distance(position, transform.position) / speed;

            return transform.DOMove(position, time);
        }

        public static Tween DOMoveWithSpeed(this Rigidbody2D rigidbody, Vector3 position, float speed)
        {
            var time = Vector3.Distance(position, rigidbody.position) / speed;

            return rigidbody.DOMove(position, time);
        }

        public static Tween DOMoveXWithSpeed(this Transform transform, float x, float speed)
        {
            var time = Mathf.Abs(x - transform.position.x) / speed;

            return transform.DOMoveX(x, time);
        }

        public static Tween DOMoveXWithSpeed(this Rigidbody2D rigidbody, float x, float speed)
        {
            var time = Mathf.Abs(x - rigidbody.position.x) / speed;

            return rigidbody.DOMoveX(x, time);
        }

        public static Tween DOMoveYWithSpeed(this Transform transform, float y, float speed)
        {
            var time = Mathf.Abs(y - transform.position.y) / speed;

            return transform.DOMoveY(y, time);
        }

        public static Tween DOMoveYWithSpeed(this Rigidbody2D rigidbody, float y, float speed)
        {
            var time = Mathf.Abs(y - rigidbody.position.y) / speed;

            return rigidbody.DOMoveY(y, time);
        }

        /// <summary>
        /// Wait for tween completion, but on yield cancellation gets stopped
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IEnumerator WaitForCompleteCancellable(this Tween t)
        {
            while (t.IsActive() && !t.IsComplete())
                yield return null;
        }

        public static IEnumerator WaitForCompleteCancellable(this Tween t, float percent)
        {
            while (t.IsActive() && !t.IsComplete() & t.ElapsedPercentage() < percent)
                yield return null;
        }

        public static Tween DOJumpUpOnPosition(this Rigidbody2D target, Vector2 endValue, float duration, bool snapping = false)
        {
            const float offsetY = -1;
            var s = DOTween.Sequence();

            Tween xTween = DOTween.To(() => target.position, x => target.position = x, new Vector2(endValue.x, target.position.y), duration)
                .SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear);

            Tween yTween = DOTween.To(() => target.position, x => target.position = x, new Vector2(target.position.x, endValue.y), duration)
                .SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad);

            s.Append(xTween).Join(yTween)
                .SetTarget(target).SetEase(DOTween.defaultEaseType);

            yTween.OnUpdate(() =>
            {
                Vector3 pos = target.position;
                pos.y += DOVirtual.EasedValue(0, offsetY, yTween.ElapsedPercentage(), Ease.OutQuad);
                target.MovePosition(pos);
            });

            return s;
        }

        public static Tween DOJumpOnPositionWithSpeed(this Rigidbody2D target, Vector2 endValue, float speed, bool snapping = false)
        {
            var duration = Vector3.Distance(target.position, endValue) / speed;

            return target.DOJumpUpOnPosition(endValue, duration, snapping);
        }

        public static Tween DoColorWithoutA(this Image image, Color color, float duration)
        {
            return DOTween.To(
                () => image.color,
                x =>
                {
                    x.a = image.color.a;
                    image.color = x;
                },
                color,
                duration
            ).Play();
        }
    }
}