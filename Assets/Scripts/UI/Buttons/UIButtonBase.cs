using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightedColor = Color.white;
    [SerializeField] private Color disabledColor = new (0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);

    private bool isInteractable;

    public event Action OnClickE;
    public event Action<bool> OnEnterE;
    public event Action<bool> OnExitE;

    public bool IsInteractable => isInteractable;

    public void SetInteractable(bool value)
    {
        isInteractable = value;

        buttonImage.color = isInteractable ? normalColor : disabledColor;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnterE?.Invoke(isInteractable);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExitE?.Invoke(isInteractable);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickE?.Invoke();
    }
}
