using BlueRacconGames.Inventory.UI;

namespace Game.View
{
    public class SubInventoryView : InventoryView
    {
        public override bool Absolute => false;
        public override bool IsPopup => true;

        public override void Initialize(InventoryUI inventoryUI)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Initialize(inventoryUI.OnSlotClicked, i);
            }
        }
    }
}
