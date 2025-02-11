using UnityEngine;
using UnityEngine.UI;
using ViewSystem.Implementation.ViewPresentations;

namespace ViewSystem.Implementation
{
    public abstract class BasicView : MonoBehaviour, IAmViewStackItem
    {
        [SerializeField]
        private Selectable defaultSelectedButton;
        
        [field: SerializeField]
        public CanvasGroup CanvasGroup { get; private set; }
        [field: SerializeReference]
        public BaseViewPresentation Presentation { get; private set; }

        public virtual bool IsPopup => false;
        public abstract bool Absolute { get; }
        public ViewStack ParentStack { get; set; }

        protected virtual void Awake()
        {
            Presentation.OnShowPresentationComplete += Presentation_OnShowPresentationComplete;
            Presentation.OnHidePresentationComplete += Presentation_OnHidePresentationComplete;
        }

        protected virtual void OnDestroy()
        {
            Presentation.OnShowPresentationComplete -= Presentation_OnShowPresentationComplete;
            Presentation.OnHidePresentationComplete -= Presentation_OnHidePresentationComplete;
        }

        public virtual void OnPushed()
        {
        }

        public virtual void OnPopped()
        {
        }

        public virtual void InjectParameters(IAmViewParameters viewParameters)
        {
        }

        public virtual void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            
            defaultSelectedButton?.Select();
        }

        public virtual void NavigateFrom(IAmViewStackItem nextViewStackItem)
        {
        }

        protected virtual void Presentation_OnShowPresentationComplete(IAmViewPresentation presentation)
        {
        }
        
        protected virtual void Presentation_OnHidePresentationComplete(IAmViewPresentation presentation)
        {
            gameObject.SetActive(false);
        }
    }
}
