using UnityEngine;

namespace BlueRacconGames.InventorySystem
{
    public class DefaultDraggableInventoryItem : DraggableInventoryItemBase
    {
        public int SlotId { get; private set; }
        public InventoryUniqueId InventoryId { get; private set; }

        public void Init(Transform slotTransform, InventoryUniqueId inventoryId, int slotId)
        {
            this.slotTransform = slotTransform;
            InventoryId = inventoryId;
            SlotId = slotId;
        }

        public override void OnDrop(InventoryController inventoryController, InventoryUniqueId inventoryId, int slotId)
        {
            var itemToAdd = inventoryController.GetItemByPos(InventoryId, SlotId);
            inventoryController.SwitchItemPos(InventoryId, inventoryId, SlotId, slotId, itemToAdd);

        }

        public override void OnSlotItemRightClick(InventoryController inventoryController, InventoryUniqueId inventoryId, int slotId)
        {
            var itemToAdd = inventoryController.GetItemByPos(InventoryId, SlotId);
            Debug.Log("P");
            inventoryController.ResizeItemAmount(InventoryId, inventoryId, SlotId, slotId, itemToAdd);
        }
    }
}
