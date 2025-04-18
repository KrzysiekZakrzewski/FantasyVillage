using System;
using DG.Tweening;
using UnityEngine;
using ViewSystem.Utils;

namespace ViewSystem.Implementation.ViewPresentations
{
    [Serializable]
    public class DotweenViewPresentation : BaseViewPresentation
    {
        [SerializeField]
        private float tweenDuration = 0.33f;
        [SerializeField]
        private Ease ease = Ease.OutCubic;
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        private Sequence sequence;

        public override event Action<IAmViewPresentation> OnShowPresentationComplete;
        public override event Action<IAmViewPresentation> OnHidePresentationComplete;

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

        public override void ForceHidePresentationComplete()
        {
            sequence?.Kill();
            OnHidePresentationComplete?.Invoke(this);
            Debug.Log("T");
        }

        protected virtual Sequence GetShowSequence(IAmViewStackItem previousViewStackItem)
        {
            return DotweenViewAnimationUtil.FadeIn(canvasGroup, ease, tweenDuration);
        }
        
        protected virtual Sequence GetHideSequence(IAmViewStackItem nextViewStackItem)
        {
            return DotweenViewAnimationUtil.FadeOut(canvasGroup, ease, tweenDuration);
        }

        private void PrepareSequence()
        {
            sequence?.Kill();
        }
    }
}
