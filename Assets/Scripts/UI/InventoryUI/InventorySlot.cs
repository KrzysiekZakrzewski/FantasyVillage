using Game.Item.Factory;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BlueRacconGames.Inventory
{
    public class InventorySlot : MonoBehaviour, 
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private Image itemIcon;
        [SerializeField]
        private Image slotIcon;
        [SerializeField]
        private TextMeshProUGUI countTxt;
        [SerializeField]
        private Color normalColor, highlightedColor;

        public InventoryItem InventoryItem {  get; private set; }
        public event Action<InventorySlot> OnSlotClickE;

        public bool IsFree => !itemIcon.enabled; 

        private void Awake()
        {
            ClearSlot();
        }

        public void AddItem(InventoryItem newInventoryItem)
        {
            ResetPosition();
            InventoryItem = newInventoryItem;
            itemIcon.sprite = InventoryItem.Item.Icon;
            itemIcon.enabled = true;

            countTxt.text = newInventoryItem.Count > 1 ? newInventoryItem.Count.ToString() : "";
        }

        public void ClearSlot()
        {
            InventoryItem = null;

            itemIcon.sprite = null;
            itemIcon.enabled = false;
            countTxt.text = "";

            ResetPosition();
        }

        public void ChangeSlot(InventorySlot newSlot)
        {
            var cacheItem = newSlot.InventoryItem;

            newSlot.ClearSlot();
            newSlot.AddItem(InventoryItem);

            ClearSlot();
            AddItem(cacheItem);
        }

        public void Move(Vector2 position)
        {
            if (IsFree) return;

            itemIcon.transform.position = position;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            slotIcon.color = highlightedColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            slotIcon.color = normalColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSlotClickE?.Invoke(this);
        }

        public void Select()
        {
            itemIcon.transform.SetParent(transform.root);
            itemIcon.raycastTarget = false;
        }

        public void Deselect()
        {
            itemIcon.transform.SetParent(transform);
            itemIcon.raycastTarget = true;
        }

        public void ResetPosition()
        {
            Deselect();
            itemIcon.transform.localPosition = Vector3.zero;
        }
    }
}