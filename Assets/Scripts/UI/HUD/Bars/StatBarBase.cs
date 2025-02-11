using BlueRacconGames.UI.Bars.Presentation;
using UnityEngine;

namespace BlueRacconGames.UI.Bars
{
    public abstract class StatBarBase : MonoBehaviour, IStatBar
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [field: SerializeReference]
        public BaseStatBarPresentation Presentation { get; private set; }

        private int currentValue;
        private int maxValue;
        private int previousValue;

        public CanvasGroup CanvasGroup => canvasGroup;

        public int CurrentValue => currentValue;
        public int PreviousValue => previousValue;
        public int MaxValue => maxValue;

        protected virtual void Awake()
        {
            Presentation.OnShowPresentationComplete += Presentation_OnShowPresentationComplete;
            Presentation.OnHidePresentationComplete += Presentation_OnHidePresentationComplete;
            Presentation.OnLaunchComplete += Presentation_OnLaunchPresentationComplete;
        }

        protected virtual void OnDestroy()
        {
            Presentation.OnShowPresentationComplete -= Presentation_OnShowPresentationComplete;
            Presentation.OnHidePresentationComplete -= Presentation_OnHidePresentationComplete;
            Presentation.OnLaunchComplete -= Presentation_OnLaunchPresentationComplete;
        }

        public virtual void Launch(int currentValue, int maxValue)
        {
            ResetBar();

            UpdateValue(currentValue, maxValue);

            Presentation.Launch(this);
        }

        public virtual void Show()
        {
            Presentation.PlayShowPresentation(this);
        }

        public void UpdateBar(int currentValue, int maxValue)
        {
            UpdateValue(currentValue, maxValue);

            Presentation.PlayUpdatePresentation(this);
        }

        public void ForceUpdateBar(int currentValue, int maxValue)
        {
            UpdateValue(currentValue, maxValue);

            Presentation.ForceUpdate(this);
        }

        public virtual void Hide()
        {
            Presentation.PlayHidePresentation(this);
        }

        public virtual void OnShowed()
        {

        }

        public virtual void OnHided()
        {

        }

        public void ResetBar()
        {
            UpdateBar(0, 0);

            Presentation.ResetPresentation(this);
        }

        protected virtual void Presentation_OnShowPresentationComplete(IStatBarPresentation presentation)
        {
        }

        protected virtual void Presentation_OnHidePresentationComplete(IStatBarPresentation presentation)
        {
            gameObject.SetActive(false);
        }

        protected virtual void Presentation_OnLaunchPresentationComplete(IStatBarPresentation presentation)
        {
            ForceUpdateBar(currentValue, maxValue);
        }

        private void UpdateValue(int currentValue, int maxValue)
        {
            previousValue = this.currentValue;
            this.currentValue = currentValue;
            this.maxValue = maxValue;
        }
    }
}