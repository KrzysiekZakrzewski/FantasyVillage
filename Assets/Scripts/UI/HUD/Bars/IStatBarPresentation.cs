using System;

namespace BlueRacconGames.UI.Bars.Presentation
{
    public interface IStatBarPresentation
    {
        event Action<IStatBarPresentation> OnShowPresentationComplete;
        event Action<IStatBarPresentation, IStatBar> OnUpdatePresentationComplete;
        event Action<IStatBarPresentation> OnHidePresentationComplete;
        event Action<IStatBarPresentation> OnLaunchComplete;

        void Launch(IStatBar statBar);
        void PlayShowPresentation(IStatBar statBar);
        void PlayUpdatePresentation(IStatBar statBar);
        void PlayHidePresentation(IStatBar statBar);
        void ForceHidePresentationComplete();
        void ForceUpdate(IStatBar statBar);
        void ResetPresentation(IStatBar statBar);
    }
}