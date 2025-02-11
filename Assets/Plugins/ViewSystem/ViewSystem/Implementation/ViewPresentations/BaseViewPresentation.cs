using System;
using UnityEngine;

namespace ViewSystem.Implementation.ViewPresentations
{
    [System.Serializable]
    public abstract class BaseViewPresentation : MonoBehaviour, IAmViewPresentation
    {
        public abstract event Action<IAmViewPresentation> OnShowPresentationComplete;
        public abstract event Action<IAmViewPresentation> OnHidePresentationComplete;

        public abstract void PlayShowPresentation(IAmViewStackItem previousViewStackItem);

        public abstract void PlayHidePresentation(IAmViewStackItem nextViewStackItem);

        public abstract void ForceHidePresentationComplete();
    }
}