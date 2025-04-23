using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightedColor = new(0.9f,0.9f,0.9f);
    [SerializeField] private Color disabledColor = new (0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);
    [SerializeField] private bool clickAnimation = true;

    protected float duration = 0.15f;
    protected Vector3 baseScale;
    protected bool isHighlighted;
    protected bool isInteractable = true;
    protected float scaleFactor = 0.8f;

    public event Action OnClickE;
    public event Action<bool> OnEnterE;
    public event Action<bool> OnExitE;

    public bool IsInteractable => isInteractable;

    private void Awake()
    {
        baseScale = transform.localScale;
    }

    public void SetInteractable(bool value)
    {
        isInteractable = value;

        Color targetColor = isInteractable ? normalColor : disabledColor;
        buttonImage.DOColor(targetColor, duration);
    }


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isHighlighted = true;

        OnEnterE?.Invoke(isInteractable);

        Color targetColor = isInteractable ? highlightedColor : disabledColor;
        buttonImage.DOColor(targetColor, duration);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isHighlighted = false;

        OnExitE?.Invoke(isInteractable);

        Color targetColor = isInteractable ? normalColor : disabledColor;
        buttonImage.DOColor(targetColor, duration);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OnClickE?.Invoke();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!clickAnimation) return;

        transform.DOScale(baseScale * scaleFactor, duration).SetEase(Ease.OutBack);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (!clickAnimation) return;

        transform.DOScale(baseScale, duration).SetEase(Ease.OutBack);
    }
}
