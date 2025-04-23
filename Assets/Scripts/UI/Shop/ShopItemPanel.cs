using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.UI.Shop
{
    public class ShopItemPanel : UIButtonBase
    {
        [SerializeField] private TextMeshProUGUI itemNameTxt;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemCostTxt;

        private Vector3 endScale = new(1.1f, 1.1f, 1f);

        protected int slotId;

        new public event Action<int> OnClickE;

        public void SetupPanel(int slotId, string name, Sprite icon, int cost, Action<int> onClick)
        {
            this.slotId = slotId;
            itemNameTxt.text = name;
            itemIcon.sprite = icon;
            itemCostTxt.text = cost.ToString();
            OnClickE += onClick;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            OnClickE?.Invoke(slotId);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            transform.DOScale(endScale, duration).SetEase(Ease.OutBack);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            transform.DOScale(baseScale, duration).SetEase(Ease.InBack); ;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            transform.DOScale(baseScale, duration).SetEase(Ease.OutBack);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            Vector3 scale = isHighlighted ? endScale : baseScale;

            transform.DOScale(scale, duration).SetEase(Ease.InBack);
        }
    }
}