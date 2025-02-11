using DG.Tweening;
using UnityEngine;

namespace ViewSystem.Utils
{
    public static class DotweenViewAnimationUtil
    {
        public const Ease DEFAULT_EASE = Ease.OutCubic;
        public const float DEFAULT_DURATION = .53f;

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        public static Sequence SlideIn(RectTransform rectTransform, Direction dir, Ease ease = DEFAULT_EASE,
            float duration = DEFAULT_DURATION)
        {
            Sequence sequence = DOTween.Sequence();

            Vector2 inPos;
            switch (dir)
            {
                case Direction.Left:
                    inPos = new Vector2(rectTransform.rect.width, 0);
                    break;
                case Direction.Right:
                    inPos = new Vector2(-rectTransform.rect.width, 0);
                    break;
                case Direction.Up:
                    inPos = new Vector2(0, -rectTransform.rect.height);
                    break;
                case Direction.Down:
                    inPos = new Vector2(0, rectTransform.rect.height);
                    break;
                default:
                    inPos = Vector2.zero;
                    break;
            }

            rectTransform.anchoredPosition = inPos;
            Tween anchorMoveTween =
                rectTransform.DOAnchorPos(Vector2.zero, duration).SetEase(ease);
            sequence.Insert(0, anchorMoveTween);

            return sequence;
        }

        public static Sequence SlideOut(RectTransform rectTransform, Direction dir, Ease ease = DEFAULT_EASE,
            float duration = DEFAULT_DURATION)
        {
            Sequence sequence = DOTween.Sequence();
            rectTransform.anchoredPosition = Vector2.zero;
            Vector2 outPos;
            switch (dir)
            {
                case Direction.Left:
                    outPos = new Vector2(-rectTransform.rect.width, 0);
                    break;
                case Direction.Right:
                    outPos = new Vector2(rectTransform.rect.width, 0);
                    break;
                case Direction.Up:
                    outPos = new Vector2(0, rectTransform.rect.height);
                    break;
                case Direction.Down:
                    outPos = new Vector2(0, -rectTransform.rect.height);
                    break;
                default:
                    outPos = Vector2.zero;
                    break;
            }

            Tween anchorMoveTween =
                rectTransform.DOAnchorPos(outPos, duration).SetEase(ease);
            sequence.Insert(0, anchorMoveTween);

            return sequence;
        }

        public static Sequence FadeIn(CanvasGroup group, Ease ease = DEFAULT_EASE, float duration = DEFAULT_DURATION)
        {
            Sequence sequence = DOTween.Sequence();
            group.alpha = 0;
            Tween fadeTween = group.DOFade(1, duration).SetEase(ease);
            sequence.Insert(0, fadeTween);
            return sequence;
        }

        public static Sequence FadeOut(CanvasGroup group, Ease ease = DEFAULT_EASE, float duration = DEFAULT_DURATION)
        {
            Sequence sequence = DOTween.Sequence();
            group.alpha = 1;
            Tween fadeTween = group.DOFade(0, duration).SetEase(ease);
            sequence.Insert(0, fadeTween);
            return sequence;
        }
    }
}