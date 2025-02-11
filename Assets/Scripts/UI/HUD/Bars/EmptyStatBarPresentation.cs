using System;

namespace BlueRacconGames.UI.Bars.Presentation
{
    public class EmptyStatBarPresentation : BaseStatBarPresentation
    {
        public override event Action<IStatBarPresentation> OnShowPresentationComplete;
        public override event Action<IStatBarPresentation, IStatBar> OnUpdatePresentationComplete;
        public override event Action<IStatBarPresentation> OnHidePresentationComplete;
        public override event Action<IStatBarPresentation> OnLaunchComplete;

        public override void Launch(IStatBar statBar)
        {

        }

        public override void PlayShowPresentation(IStatBar statBar)
        {
            OnShowPresentationComplete?.Invoke(this);
        }

        public override void PlayUpdatePresentation(IStatBar statBar)
        {
            OnUpdatePresentationComplete?.Invoke(this, statBar);
        }

        public override void PlayHidePresentation(IStatBar statBar)
        {
            OnHidePresentationComplete?.Invoke(this);
        }

        public override void ForceHidePresentationComplete()
        {
            OnHidePresentationComplete?.Invoke(this);
        }

        public override void ForceUpdate(IStatBar statBar)
        {
            OnUpdatePresentationComplete?.Invoke(this, statBar);
        }

        public override void ResetPresentation(IStatBar statBar)
        {

        }
    }
}
