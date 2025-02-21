using BlueRacconGames.Inventory;

namespace Game.View
{
    public class ChestInventoryView : InventoryView
    {
        public override bool Absolute => false;
        public override bool IsPopup => true;

        public override void Initialize(InventoryUI inventoryUI)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Initialize(inventoryUI.OnSlotClicked, i, InventorySlotType.Chest);
            }
        }
    }
}
