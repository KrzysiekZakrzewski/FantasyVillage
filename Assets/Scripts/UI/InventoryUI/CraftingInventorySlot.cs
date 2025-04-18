namespace BlueRacconGames.InventorySystem.New
{
    public class CraftingInventorySlot : InventorySlotBase
    {
        protected override void OnItemDropped(DraggableInventoryItemBase draggableItem)
        {
            draggableItem.OnDrop(inventoryController, inventoryId, slotId);
        }
    }
}
