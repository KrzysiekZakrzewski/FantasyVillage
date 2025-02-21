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

        public event Action<InventorySlot> OnSlotClickE;

        public int SlotCount { get; private set; }
        public bool IsFree => !itemIcon.enabled;
        public InventorySlotType InventorySlotType { get; private set; } = InventorySlotType.Main;
        public int Id { get; private set; }

        public ValidStatus ValidStatus = ValidStatus.WaitForUpdate;

        public void Initialize(Action<InventorySlot> OnSlotClickedAction, int slotId, InventorySlotType inventorySlotType = InventorySlotType.Main)
        {
            OnSlotClickE += OnSlotClickedAction;
            InventorySlotType = inventorySlotType;
            Id = slotId;
        }

        public void Refresh(InventoryManager inventoryManager)
        {
            if (ValidStatus == ValidStatus.Valid) return;

            var item = InventorySlotType == InventorySlotType.Main ? 
                inventoryManager.GetItemBySlotId(Id) : inventoryManager.GetItemBySlotIdFromChest(Id);

            ResetPosition();

            ClearSlot();
            if (item == null) return;

            AddItem(item);

            ValidStatus = ValidStatus.Valid;
        }

        public void AddItem(InventoryItem newInventoryItem)
        {
            itemIcon.sprite = newInventoryItem.Item.Icon;
            itemIcon.enabled = true;

            CountUpdate(newInventoryItem.Count);
        }

        public void ClearSlot()
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            countTxt.text = "";
        }

        public void CountUpdate(int newCount)
        {
            SlotCount = newCount;

            countTxt.text = SlotCount > 1 ? SlotCount.ToString() : "";
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

    public enum InventorySlotType
    {
        Main,
        Chest
    }

    public enum ValidStatus
    {
        Valid,
        WaitForUpdate
    }
}