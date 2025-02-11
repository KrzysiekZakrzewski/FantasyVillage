using System;
using System.Collections.Generic;
using UnityEngine;
using ViewSystem.Implementation.ViewPresentations;

namespace ViewSystem
{
    public class ViewStack
    {
        private readonly Stack<IAmViewStackItem> viewStack = new Stack<IAmViewStackItem>();
        private readonly Dictionary<IAmViewPresentation, IAmViewStackItem> presentationToViewLut = new Dictionary<IAmViewPresentation, IAmViewStackItem>();
        private readonly HashSet<IAmViewPresentation> playingHidePresentations = new HashSet<IAmViewPresentation>();
        private readonly Queue<Action> navigationQueue = new Queue<Action>();
        private bool isNavigating;

        public event Action<IAmViewStackItem> OnViewPopped;
        public event Action<IAmViewStackItem> OnViewPushed;

        public bool TryPushSafe(IAmViewStackItem viewStackItem, IAmViewParameters viewParameters = null)
        {
            if (isNavigating) return false;
            Push(viewStackItem, viewParameters);
            return true;
        }

        public bool TryPopSafe()
        {
            if (isNavigating) return false;
            Pop();
            return true;
        }

        public void Push(IAmViewStackItem viewStackItem, IAmViewParameters viewParameters = null)
        {
            EnqueueNavigation(() => PushInternal(viewStackItem, viewParameters));
        }

        public void Pop()
        {
            EnqueueNavigation(PopInternal);
        }

        public void ClearStack()
        {
            while (viewStack.Count > 0)
            {
                IAmViewStackItem viewStackItem = viewStack.Pop();
                NavigateFrom(viewStackItem, null, true);
                viewStackItem.OnPopped();
                OnViewPopped?.Invoke(viewStackItem);
            }
        }
        
        public void ForceHidePresentationCompleteForPlayingPresentations()
        {
            foreach (IAmViewPresentation presentation in playingHidePresentations)
            {
                presentation.ForceHidePresentationComplete();
            }
        }

        public IAmViewStackItem Peek()
        {
            return viewStack.Count > 0 ? viewStack.Peek() : null;
        }
        
        public void NavigateTo(IAmViewStackItem viewStackItem, IAmViewStackItem previousViewStackItem, bool processNavigationQueueOnShown)
        {
            viewStackItem.NavigateTo(previousViewStackItem);
            viewStackItem.CanvasGroup.blocksRaycasts = true;
            viewStackItem.CanvasGroup.interactable = false;
            viewStackItem.Presentation.OnShowPresentationComplete += View_OnShowPresentationComplete;
            if (processNavigationQueueOnShown)
            {
                viewStackItem.Presentation.OnShowPresentationComplete += View_OnPresentationComplete_ProcessNavigationQueue;
            }

            viewStackItem.Presentation.PlayShowPresentation(previousViewStackItem);
        }

        public void NavigateFrom(IAmViewStackItem viewStackItem, IAmViewStackItem nextViewStackItem, bool processNavigationQueueOnHidden, bool skipPresentation = false)
        {
            viewStackItem.CanvasGroup.interactable = false;
            viewStackItem.NavigateFrom(nextViewStackItem);
            if (nextViewStackItem != null && nextViewStackItem.IsPopup) return;
            
            viewStackItem.Presentation.OnHidePresentationComplete += View_OnHidePresentationComplete;
            playingHidePresentations.Add(viewStackItem.Presentation);
            
            if (processNavigationQueueOnHidden)
            {
                viewStackItem.Presentation.OnHidePresentationComplete += View_OnPresentationComplete_ProcessNavigationQueue;
            }

            viewStackItem.Presentation.PlayHidePresentation(nextViewStackItem);
        }

        private void PushInternal(IAmViewStackItem viewStackItem, IAmViewParameters viewParameters = null)
        {
            isNavigating = true;
            viewStackItem.ParentStack = this;
            IAmViewStackItem previousViewStackItem = null;
            if (viewStackItem.Absolute)
            {
                while (viewStack.Count > 0)
                {
                    previousViewStackItem = viewStack.Pop();
                    NavigateFrom(previousViewStackItem, viewStackItem, false);
                    OnViewPopped?.Invoke(previousViewStackItem);
                }
            }
            else if (viewStack.Count > 0)
            {
                previousViewStackItem = viewStack.Peek();
                NavigateFrom(previousViewStackItem, viewStackItem, false);
            }

            presentationToViewLut.TryAdd(viewStackItem.Presentation, viewStackItem);
            if (viewParameters != null)
            {
                viewStackItem.InjectParameters(viewParameters);
            }
            NavigateTo(viewStackItem, previousViewStackItem, true);
            viewStack.Push(viewStackItem);
            viewStackItem.OnPushed();
            OnViewPushed?.Invoke(viewStackItem);
        }

        private void PopInternal()
        {
            if (viewStack.Count == 0) return;

            IAmViewStackItem viewStackItem = viewStack.Pop();
            IAmViewStackItem viewStackItemToShow = Peek();
            NavigateFrom(viewStackItem, viewStackItemToShow, true);
            viewStackItem.OnPopped();
            OnViewPopped?.Invoke(viewStackItem);

            if (viewStackItemToShow != null)
            {
                NavigateTo(viewStackItemToShow, viewStackItem, false);
            }

            isNavigating = true;
        }

        private void EnqueueNavigation(Action navigationAction)
        {
            if (isNavigating)
            {
                navigationQueue.Enqueue(navigationAction);
            }
            else
            {
                navigationAction?.Invoke();
            }
        }

        private void ProcessNavigationQueue()
        {
            if (navigationQueue.Count > 0)
            {
                navigationQueue.Dequeue()?.Invoke();
            }
            else
            {
                isNavigating = false;
            }
        }

        private void View_OnShowPresentationComplete(IAmViewPresentation presentation)
        {
            presentation.OnShowPresentationComplete -= View_OnShowPresentationComplete;
            presentationToViewLut[presentation].CanvasGroup.interactable = true;
        }

        private void View_OnHidePresentationComplete(IAmViewPresentation presentation)
        {
            presentation.OnHidePresentationComplete -= View_OnHidePresentationComplete;
            presentationToViewLut[presentation].CanvasGroup.blocksRaycasts = false;
            playingHidePresentations.Remove(presentation);
        }

        private void View_OnPresentationComplete_ProcessNavigationQueue(IAmViewPresentation presentation)
        {
            presentation.OnHidePresentationComplete -= View_OnPresentationComplete_ProcessNavigationQueue;
            ProcessNavigationQueue();
        }
    }
}