using BlueRacconGames.Crafting;
using UnityEngine.EventSystems;
using Zenject;

namespace BlueRacconGames.InventorySystem
{
    public class DraggableInventoryOutputItem : DraggableInventoryItemBase
    {
        private CraftingSystem craftingSystem;

        [Inject]
        private void Inject(CraftingSystem craftingSystem)
        {
            this.craftingSystem = craftingSystem;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            craftingSystem.Craft(OnItemCrafted);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);

            ClearItem();

            craftingSystem.ResetTableAfterCrafted();
        }

        public override void OnDrop(InventoryController inventoryController, InventoryUniqueId inventoryId, int slotId)
        {
            inventoryController.AddItemPos(inventoryId, craftingSystem.CraftedItem, slotId);
        }

        public override void OnSlotItemRightClick(InventoryController inventoryController, InventoryUniqueId inventoryId, int slotId)
        {
            return;
        }
    }
}