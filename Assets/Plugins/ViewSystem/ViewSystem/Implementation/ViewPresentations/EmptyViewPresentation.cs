using System;

namespace ViewSystem.Implementation.ViewPresentations
{
    public class EmptyViewPresentation : BaseViewPresentation
    {
        public override event Action<IAmViewPresentation> OnShowPresentationComplete;
        public override event Action<IAmViewPresentation> OnHidePresentationComplete;
        public override void PlayShowPresentation(IAmViewStackItem previousViewStackItem)
        {
            OnShowPresentationComplete?.Invoke(this);
        }

        public override void PlayHidePresentation(IAmViewStackItem nextViewStackItem)
        {
            OnHidePresentationComplete?.Invoke(this);
        }

        public override void ForceHidePresentationComplete()
        {
            OnHidePresentationComplete?.Invoke(this);
        }
    }
}
