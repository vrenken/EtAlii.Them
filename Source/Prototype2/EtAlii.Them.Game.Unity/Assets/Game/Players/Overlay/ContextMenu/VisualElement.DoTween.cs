namespace Game.Players
{
    using DG.Tweening;
    using DG.Tweening.Core;
    using DG.Tweening.Plugins.Options;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class VisualElementDoTweenExtensions
    {
        /// <summary>Tweens a VisualElement's scale to the given value.
        /// Also stores the VisualElement as the tween's target so it can be used for filtered operations</summary>
        /// <param name="target"></param>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOScale(this VisualElement target, Vector3 endValue, float duration)
        {
            var t = DOTween.To(() => target.style.scale.value.value,
                x => target.style.scale = new StyleScale(new Scale(x)), endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens a VisualElement's scale uniformly to the given value.
        /// Also stores the VisualElement as the tween's target so it can be used for filtered operations</summary>
        /// <param name="target"></param>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOScale(this VisualElement target, float endValue, float duration)
        {
            Vector3 endValueV3 = new Vector3(endValue, endValue, endValue);
            var t = DOTween.To(() => target.style.scale.value.value,
                x => target.style.scale = new StyleScale(new Scale(x)), endValueV3, duration);
            t.SetTarget(target);
            return t;
        }
        
        /// <summary>Tweens a VisualElement's rotation uniformly to the given value.
        /// Also stores the VisualElement as the tween's target so it can be used for filtered operations</summary>
        /// <param name="target"></param>
        /// <param name="endValue">The end value to reach</param><param name="duration">The duration of the tween</param>
        public static TweenerCore<float, float, FloatOptions> DoRotate(this VisualElement target, float endValue, float duration)
        {
            var t = DOTween.To(() => target.style.rotate.value.angle.ToDegrees(),
                x => target.style.rotate = new StyleRotate(new Rotate(new Angle(x, AngleUnit.Degree))), endValue, duration);
            t.SetTarget(target);
            return t;
        }
    }
}

    