using Game.Item.Factory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.Inventory
{
    public interface IInventory
    {
        List<InventoryItem> Items { get; }

        event Action OnItemChangedE;

        void Initialize(InventoryManager inventoryManager, Action OnItemChangedE);
        bool AddItem(ItemFactorySO item, int count, int slotId = -1);
        bool RemoveItem(ItemFactorySO item, int count = 1);
        bool RemoveItemBySlot(int slotId);
        InventoryItem GetItemBySlotId(int slotId);
    }

    [System.Serializable]
    public class InventoryItem
    {
        [field: SerializeField]
        public ItemFactorySO Item { get; private set; }
        [field: SerializeField]
        public int Count;

        public int SlotId { get; private set; } = -1;

        public InventoryItem(ItemFactorySO item, int slotId, int count)
        {
            Item = item;
            Count = count;
            SlotId = slotId;
        }

        public void SetSlotId(int slotId) => SlotId = slotId;
    }
}