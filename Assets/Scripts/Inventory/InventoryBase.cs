using Game.Item.Factory;
using Game.View;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using BlueRacconGames.Inventory.UI;
using System;

namespace BlueRacconGames.Inventory
{
    public abstract class InventoryBase : IInventory
    {
        [SerializeField]
        private List<InventoryItem> items = new();
        private InventoryManager inventoryManager;

        public event Action OnItemChangedE;
        public List<InventoryItem> Items => items;

        public void Initialize(InventoryManager inventoryManager, Action OnItemChangedE = null)
        {
            this.OnItemChangedE += OnItemChangedE;
            this.inventoryManager = inventoryManager;

            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].SetSlotId(i);
            }

            OnItemChangedE?.Invoke();
        }

        public bool AddItem(ItemFactorySO item, int count = 1, int slotId = -1)
        {
            if (item == null || count <= 0) return false;

            if(slotId != -1)
                Items.Add(new InventoryItem(item, slotId, count));
            else
            {
                var inventoryItem = Items.Find(x => x.Item == item);

                if (inventoryItem != null)
                    inventoryItem.Count += count;
                else
                {
                    var emptySlotId = inventoryManager.GetFirstFreeSlotId();

                    Items.Add(new InventoryItem(item, emptySlotId, count));
                }
            }

            OnItemChangedE?.Invoke();

            return true;
        }

        public bool RemoveItem(ItemFactorySO item, int count = 1)
        {
            var inventoryItem = Items.Find(x => x.Item == item);

            if (inventoryItem == null || count <= 0) return false;

            inventoryItem.Count -= count;

            if (inventoryItem.Count > 0) return true;

            Items.Remove(inventoryItem);
            return true;
        }

        public bool RemoveItemBySlot(int slotId)
        {
            var inventoryItem = Items.Find(x => x.SlotId == slotId);

            if (inventoryItem == null) return false;

            Items.Remove(inventoryItem);
            return true;
        }

        public InventoryItem GetItemBySlotId(int slotId)
        {
            return Items.Find(x => x.SlotId == slotId);
        }
    }
}
