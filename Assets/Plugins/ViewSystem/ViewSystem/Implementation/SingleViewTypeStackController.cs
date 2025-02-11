using System;
using System.Collections.Generic;
using UnityEngine;

namespace ViewSystem.Implementation
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SingleViewTypeStackController : ViewStackControllerBase
    {
        private readonly Dictionary<Type, IAmViewStackItem> viewLut = new Dictionary<Type, IAmViewStackItem>();
        private readonly HashSet<Type> openedViews = new HashSet<Type>();

        protected override void Awake()
        {
            base.Awake();
            if (viewLut.Count == 0)
            {
                GatherViews();
            }
            
            viewStack.OnViewPopped += ViewStackOnViewPopped;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if(viewStack == null) return;
            viewStack.OnViewPopped -= ViewStackOnViewPopped;
        }

        public bool TryOpenSafe<T>(IAmViewParameters viewParameters = null) where T : IAmViewStackItem
        {
            if (openedViews.Contains(typeof(T))) return false;
            if (!viewLut.TryGetValue(typeof(T), out IAmViewStackItem view)) return false;
            if (!viewStack.TryPushSafe(view, viewParameters)) return false;

            openedViews.Add(view.GetType());
            return true;
        }

        public void Open<T>(IAmViewParameters viewParameters = null) where T : IAmViewStackItem
        {
            IAmViewStackItem viewStackItem = viewLut[typeof(T)];
            viewStack.Push(viewStackItem, viewParameters);
            openedViews.Add(viewStackItem.GetType());
        }

        private void GatherViews()
        {
            viewLut.Clear();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                IAmViewStackItem viewStackItem = child.GetComponent<IAmViewStackItem>();
                if (viewStackItem == null) continue;
                child.gameObject.SetActive(false);
                viewLut.Add(viewStackItem.GetType(), viewStackItem);
            }
        }

        private void ViewStackOnViewPopped(IAmViewStackItem viewStackItem)
        {
            openedViews.Remove(viewStackItem.GetType());
        }
    }
}