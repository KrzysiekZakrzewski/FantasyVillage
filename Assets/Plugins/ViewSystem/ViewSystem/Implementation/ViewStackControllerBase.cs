using System;
using UnityEngine;
using ViewSystem.Implementation.ViewPresentations;

namespace ViewSystem.Implementation
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ViewStackControllerBase : MonoBehaviour, IAmViewStackItem
    {
        protected readonly ViewStack viewStack = new ViewStack();
        
        public virtual bool IsPopup => false;
        public virtual bool Absolute => false;
        public CanvasGroup CanvasGroup { get; private set; }
        
        [field: SerializeReference]
        public BaseViewPresentation Presentation { get; private set; }
        public ViewStack ParentStack { get; set; }
        
        public event Action<IAmViewStackItem> OnViewPopped
        {
            add => viewStack.OnViewPopped += value;
            remove => viewStack.OnViewPopped -= value;
        }

        public event Action<IAmViewStackItem> OnViewPushed
        {
            add => viewStack.OnViewPushed += value;
            remove => viewStack.OnViewPushed -= value;
        }

        protected virtual void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
            Presentation.OnShowPresentationComplete += Presentation_OnShowPresentationComplete;
            Presentation.OnHidePresentationComplete += Presentation_OnHidePresentationComplete;
        }
        
        protected virtual void OnDestroy()
        {
            Presentation.OnShowPresentationComplete -= Presentation_OnShowPresentationComplete;
            Presentation.OnHidePresentationComplete -= Presentation_OnHidePresentationComplete;
        }

        public virtual void Clear()
        {
            viewStack.ClearStack();
        }
        
        public virtual bool TryPopSafe()
        {
            return viewStack.TryPopSafe();
        }
        
        public virtual IAmViewStackItem Peek()
        {
            return viewStack.Peek();
        }

        public virtual void OnPushed()
        {
        }

        public virtual void OnPopped()
        {
            Clear();
        }

        public virtual void InjectParameters(IAmViewParameters viewParameters)
        {
        }

        public virtual void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            gameObject.SetActive(true);
            IAmViewStackItem topmostViewStackItem = viewStack.Peek();

            if (topmostViewStackItem != null)
            {
                viewStack.NavigateTo(topmostViewStackItem, previousViewStackItem, false);
            }
        }

        public virtual void NavigateFrom(IAmViewStackItem nextViewStackItem)
        {
            IAmViewStackItem topmostViewStackItem = viewStack.Peek();
            if (topmostViewStackItem == null) return;
            
            viewStack?.NavigateFrom(topmostViewStackItem, nextViewStackItem, false);
        }
        
        protected virtual void Presentation_OnShowPresentationComplete(IAmViewPresentation presentation)
        {
        }
        
        protected virtual void Presentation_OnHidePresentationComplete(IAmViewPresentation presentation)
        {
            gameObject.SetActive(false);
            viewStack.ForceHidePresentationCompleteForPlayingPresentations();
        }
    }
}
