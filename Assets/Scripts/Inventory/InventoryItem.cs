using Game.Item.Factory;
using UnityEngine;

namespace BlueRacconGames.InventorySystem
{
    [System.Serializable]
    public class InventoryItem
    {
        [SerializeField] private ItemFactorySO itemFactory;
        [SerializeField] private int amount;
        [SerializeField] private int slotId;


        public ItemFactorySO ItemFactory => itemFactory;
        public int Amount => amount;
        public int MaxStackAmount => itemFactory == null ? 0 : itemFactory.MaxStackAmount;
        public int SlotId => slotId;

        public InventoryItem()
        {
            itemFactory = null;
            amount = 0;
            slotId = 0;
        }

        public InventoryItem(ItemFactorySO itemFactory, int amount)
        {
            this.itemFactory = itemFactory;
            this.amount = amount;
        }

        public InventoryItem(InventoryItem prevItem, int amount)
        {
            this.itemFactory = prevItem.ItemFactory;
            this.amount = amount;
        }

        public void SetAmount(int amount)
        {
            this.amount = amount;
        }
        public void SetPosition(int position)
        {
            this.slotId = position;
        }
        public bool IsNullOrEmpty()
        {
            return amount == 0 || itemFactory == null;
        }
    }
}
