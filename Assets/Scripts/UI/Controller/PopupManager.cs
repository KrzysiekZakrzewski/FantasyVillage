using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField]
        private float popupShowDuration = .3f;
        [SerializeField]
        private float popupStayDuration = 5f;
        [SerializeField]
        RectTransform popupContainer;
        [SerializeField]
        Vector2 anchoredPositionOffset;
        [SerializeField]
        private CanvasGroup popupContainerCanvasGroup;
        [SerializeField]
        private TextMeshProUGUI popupTitleText;
        [SerializeField]
        private Image popupTitleImage;

        private Sequence sequence;

        private Vector2 defaultAnchoredPosition;

        private void Awake()
        {
            defaultAnchoredPosition = popupContainer.anchoredPosition;
            popupContainerCanvasGroup.interactable = false;
            popupContainerCanvasGroup.blocksRaycasts = false;
            popupContainerCanvasGroup.alpha = 0;
        }

        private void PrepareSequence()
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            sequence.SetUpdate(true);
        }

        public void ShowPopup(string popupTitle, Sprite popupSprite)
        {
            popupTitleText.text = popupTitle;
            popupTitleImage.sprite = popupSprite;
            PrepareSequence();
            popupContainer.anchoredPosition = defaultAnchoredPosition + anchoredPositionOffset;
            sequence.Append(popupContainer.DOAnchorPos(defaultAnchoredPosition, popupShowDuration).SetEase(Ease.OutExpo));
            sequence.Join(popupContainerCanvasGroup.DOFade(1, popupShowDuration));
            sequence.AppendInterval(popupStayDuration);
            sequence.Append(popupContainer.DOAnchorPos(defaultAnchoredPosition + anchoredPositionOffset, popupShowDuration).SetEase(Ease.OutExpo));
            sequence.Join(popupContainerCanvasGroup.DOFade(0, popupShowDuration));
        }
    }
}