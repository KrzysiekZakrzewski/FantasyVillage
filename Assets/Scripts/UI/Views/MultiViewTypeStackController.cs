using System;
using System.Collections.Generic;
using UnityEngine;
using ViewSystem;
using ViewSystem.Implementation;

public class MultiViewTypeStackController : ViewStackControllerBase
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
        if (viewStack == null) return;
        viewStack.OnViewPopped -= ViewStackOnViewPopped;
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