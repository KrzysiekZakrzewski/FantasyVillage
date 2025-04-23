using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeInventoryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image fillImage;

    private float duration = 2f;
    private bool completed;

    public Action OnStartFillStartedE;
    public Action OnFillCompletedE;
    public Action OnFillFaildE;

    Tween fillTween;

    public void OnPointerEnter(PointerEventData eventData)
    {
        CrateTween();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!completed)
        {
            fillTween.PlayBackwards();
            return;
        }

        ClearFillTween();
    }

    private void CrateTween()
    {
        if(fillTween != null)
        {
            fillTween.PlayForward();
            return;
        }

        fillTween = fillImage.DOFillAmount(1, duration);
        fillTween.SetAutoKill(false);
        fillTween.OnRewind(OnFillFaild);
        fillTween.OnComplete(OnFillCompleted);
    }

    private void OnFillCompleted()
    {
        OnFillCompletedE?.Invoke();

        completed = true;
    }

    private void OnFillFaild()
    {
        OnFillFaildE?.Invoke();

        ClearFillTween();
    }

    private void ClearFillTween()
    {
        fillTween.Kill();
        fillTween = null;
        completed = false;
        fillImage.fillAmount = 0f;
    }
}
