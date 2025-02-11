using DG.Tweening;
using System;
using UnityEngine;
using ViewSystem.Utils;
using static ViewSystem.Utils.DotweenViewAnimationUtil;

namespace ViewSystem.Implementation.ViewPresentations
{
    public class DotweenSlideViewPresentation : BaseViewPresentation
    {
        [SerializeField]
        private float tweenDuration = 0.33f;

        [SerializeField]
        private Ease ease = Ease.OutCubic;

        [SerializeField]
        private RectTransform rectTransform;

        private Sequence sequence;
        private Direction direction = Direction.Left;

        public override event Action<IAmViewPresentation> OnShowPresentationComplete;
        public override event Action<IAmViewPresentation> OnHidePresentationComplete;

        protected virtual Sequence GetShowSequence(IAmViewStackItem previousViewStackItem)
        {
            return DotweenViewAnimationUtil.SlideIn(rectTransform, direction, ease, tweenDuration);
        }

        protected virtual Sequence GetHideSequence(IAmViewStackItem nextViewStackItem)
        {
            return DotweenViewAnimationUtil.SlideOut(rectTransform, direction, ease, tweenDuration);
        }

        public override void ForceHidePresentationComplete()
        {
            sequence?.Kill();
            OnHidePresentationComplete?.Invoke(this);
        }

        public override void PlayHidePresentation(IAmViewStackItem nextViewStackItem)
        {
            PrepareSequence();
            sequence = GetHideSequence(nextViewStackItem);
            sequence.onComplete += () =>
            {
                OnHidePresentationComplete?.Invoke(this);
            };
            sequence.SetUpdate(true);
        }

        public override void PlayShowPresentation(IAmViewStackItem previousViewStackItem)
        {
            PrepareSequence();
            sequence = GetShowSequence(previousViewStackItem);
            sequence.onComplete += () =>
            {
                OnShowPresentationComplete?.Invoke(this);
            };
            sequence.SetUpdate(true);
        }

        private void PrepareSequence()
        {
            sequence?.Kill();
        }

        public void SetDirection(Direction direction) => this.direction = direction;
    }
}