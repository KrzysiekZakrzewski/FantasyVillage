using System;
using UnityEngine;

namespace BlueRacconGames.UI.Bars.Presentation
{
    [System.Serializable]
    public abstract class BaseStatBarPresentation : MonoBehaviour, IStatBarPresentation
    {
        public abstract event Action<IStatBarPresentation> OnShowPresentationComplete;
        public abstract event Action<IStatBarPresentation, IStatBar> OnUpdatePresentationComplete;
        public abstract event Action<IStatBarPresentation> OnHidePresentationComplete;
        public abstract event Action<IStatBarPresentation> OnLaunchComplete;

        public abstract void Launch(IStatBar statBar);

        public abstract void PlayShowPresentation(IStatBar statBar);

        public abstract void PlayUpdatePresentation(IStatBar statBar);

        public abstract void PlayHidePresentation(IStatBar statBar);

        public abstract void ForceHidePresentationComplete();
        public abstract void ForceUpdate(IStatBar statBar);
        public abstract void ResetPresentation(IStatBar statBar);
    }
}
