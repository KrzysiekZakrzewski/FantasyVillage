using UnityEngine;
using ViewSystem.Implementation.ViewPresentations;

namespace ViewSystem
{
    public interface IAmViewStackItem
    {
        /// <summary>
        /// If true the lower views on the stack will be visible and won't play their hide animations.
        /// </summary>
        bool IsPopup { get; }
        /// <summary>
        /// If true, all other views on the stack will be popped before this view is shown.
        /// </summary>
        bool Absolute { get; }
        CanvasGroup CanvasGroup { get; }
        BaseViewPresentation Presentation { get; }
        ViewStack ParentStack { get; set; }
        void OnPushed();
        void OnPopped();
        void InjectParameters(IAmViewParameters viewParameters);
        void NavigateTo(IAmViewStackItem previousViewStackItem);
        void NavigateFrom(IAmViewStackItem nextViewStackItem);
    }
}
