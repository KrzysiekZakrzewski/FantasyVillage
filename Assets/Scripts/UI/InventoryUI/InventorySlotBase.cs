using UnityEngine.EventSystems;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.InventorySystem
{
    public abstract class InventorySlotBase : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected DefaultDraggableInventoryItem draggableItem;

        private GameObject pointerDragCache;
        protected int slotId;
        protected InventoryUniqueId inventoryId;
        protected InventoryController inventoryController;

        [Inject]
        private void Inject(InventoryController inventoryController)
        {
            this.inventoryController = inventoryController;
        }

        public void Init(int slotId, InventoryUniqueId inventoryId)
        {
            this.slotId = slotId;
            this.inventoryId = inventoryId;

            draggableItem.Init(transform, inventoryId, slotId);
        }

        public void UpdateSlot(InventoryItem inventoryItem)
        {
            if (draggableItem == null) return;

            if (inventoryItem.IsNullOrEmpty())
            {
                ClearSlot();
                return;
            }

            draggableItem.UpdateItem(inventoryItem.ItemFactory.Icon, inventoryItem.Amount);
        }

        public void ClearSlot()
        {
            draggableItem.ClearItem();
        }

        public void OnDrop(PointerEventData eventData)
        {
            DraggableInventoryItemBase draggableItem = GetCurrentDragItem(eventData);

            pointerDragCache = null;

            if (draggableItem.IsFree) return;

            OnItemDropped(draggableItem);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (pointerDragCache == null || eventData.button != PointerEventData.InputButton.Right) return;

            DraggableInventoryItemBase draggableItem = pointerDragCache.GetComponent<DraggableInventoryItemBase>();

            if (draggableItem.IsFree) return;

            OnRightButtonClick(draggableItem);
        }

        protected abstract void OnItemDropped(DraggableInventoryItemBase draggableItem);

        private void OnRightButtonClick(DraggableInventoryItemBase draggableItem)
        {
            draggableItem.OnSlotItemRightClick(inventoryController, inventoryId, slotId);
        }

        private DraggableInventoryItemBase GetCurrentDragItem(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;

            return dropped.GetComponent<DraggableInventoryItemBase>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            pointerDragCache = eventData.pointerDrag;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            pointerDragCache = null;
        }
    }
}
