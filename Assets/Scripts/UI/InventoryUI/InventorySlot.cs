using Game.Item.Factory;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BlueRacconGames.Inventory
{
    public class InventorySlot : MonoBehaviour, IEndDragHandler, 
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
            itemIcon.transform.localPosition = Vector3.zero;
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
            itemIcon.transform.localPosition = Vector3.zero;
        }

        public void ChangeSlot(InventorySlot newSlot)
        {
            newSlot.AddItem(InventoryItem);
            ClearSlot();
        }

        public void Move(Vector2 position)
        {
            if (IsFree) return;

            itemIcon.transform.position = position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemIcon.raycastTarget = true;

            var pointerGO = eventData.pointerCurrentRaycast.gameObject;

            if (pointerGO == null || pointerGO == gameObject || !pointerGO.TryGetComponent<InventorySlot>(out var selectedSlot))
            {
                itemIcon.transform.localPosition = Vector3.zero;
                return;
            }

            ChangeSlot(selectedSlot);
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
            itemIcon.raycastTarget = false;
        }

        public void Deselect()
        {
            itemIcon.raycastTarget = true;
        }
    }
}