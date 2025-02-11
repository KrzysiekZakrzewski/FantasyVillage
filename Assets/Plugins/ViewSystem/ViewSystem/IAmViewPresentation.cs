using System;

namespace ViewSystem
{
    public interface IAmViewPresentation
    {
        event Action<IAmViewPresentation> OnShowPresentationComplete;
        event Action<IAmViewPresentation> OnHidePresentationComplete;
        void PlayShowPresentation(IAmViewStackItem previousViewStackItem);
        void PlayHidePresentation(IAmViewStackItem nextViewStackItem);
        void ForceHidePresentationComplete();
    }
}
