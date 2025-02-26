using BlueRacconGames.Inventory;
using BlueRacconGames.Inventory.UI;

namespace Game.View
{
    public class MainInventoryView : InventoryView
    {
        public override bool Absolute => false;

        public override void Initialize(InventoryUI inventoryUI)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Initialize(inventoryUI.OnSlotClicked, i);
            }
        }
    }
}