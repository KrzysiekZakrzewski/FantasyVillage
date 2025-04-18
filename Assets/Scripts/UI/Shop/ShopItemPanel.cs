using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.UI.Shop
{
    public class ShopItemPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI itemNameTxt;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemCostTxt;

        private Vector3 endScale = new(1.1f, 1.1f, 1f);
        private float scaleDuration = 0.2f;

        protected int slotId;

        private Tween tween;

        public event Action<int> OnClickE;

        public void SetupPanel(int slotId, string name, Sprite icon, int cost, Action<int> onClick)
        {
            this.slotId = slotId;
            itemNameTxt.text = name;
            itemIcon.sprite = icon;
            itemCostTxt.text = cost.ToString();
            OnClickE += onClick;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickE?.Invoke(slotId);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tween = transform.DOScale(endScale, scaleDuration).SetEase(Ease.OutBack); ;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.InBack); ;
        }
    }
}