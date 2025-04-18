using UnityEngine;

namespace BlueRacconGames.InventorySystem
{
    public class DefaultInventorySlot : InventorySlotBase
    {
        protected override void OnItemDropped(DraggableInventoryItemBase draggableItem)
        {
            draggableItem.OnDrop(inventoryController, inventoryId, slotId);
        }
    }
}