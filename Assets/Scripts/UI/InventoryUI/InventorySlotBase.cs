using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BlueRacconGames.Inventory.UI
{
    public abstract class InventorySlotBase : MonoBehaviour, IInventorySlot
    {
        [SerializeField]
        private Image itemIcon;
        [SerializeField]
        private Image slotIcon;
        [SerializeField]
        private TextMeshProUGUI countTxt;
        [SerializeField]
        private Color normalColor, highlightedColor;

        private int id;

        protected ValidStatus validStatus = ValidStatus.WaitForUpdate;

        public event Action<IInventorySlot> OnSlotClickE;

        public int Id => id;
        public bool IsFree => !itemIcon.enabled;

        public abstract SlotType Type { get; }
        public ValidStatus ValidStatus { get => validStatus; set => validStatus = value; }

        private void OnDisable()
        {
            slotIcon.color = normalColor;
        }

        public void Initialize(Action<IInventorySlot> OnSlotClickedAction, int slotId)
        {
            ValidStatus = ValidStatus.WaitForUpdate;
            OnSlotClickE += OnSlotClickedAction;
            id = slotId;
        }

        public void Refresh(InventoryManager inventoryManager)
        {
            if (ValidStatus == ValidStatus.Valid) return;

            var item = inventoryManager.GetItemBySlotId(Id, Type);

            ResetPosition();
            ClearSlot();

            ValidStatus = ValidStatus.Valid;

            if (item != null)
                AddItem(item);
        }

        public void AddItem(InventoryItem newInventoryItem)
        {
            itemIcon.sprite = newInventoryItem.Item.Icon;
            itemIcon.enabled = true;

            countTxt.text = newInventoryItem.Count > 1 ? newInventoryItem.Count.ToString() : "";
        }

        public void ClearSlot()
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            countTxt.text = "";
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
