using Game.Item.Factory;
using Inputs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        private int space = 20;
        private int spaceInSlot = 64;

        [field: SerializeField]
        public List<InventoryItem> Items { get; private set; } = new();
        public event Action OnItemChangedE;

        public bool Add(ItemFactorySO item, int count = 1)
        {
            if(item.IsDefaultItem || Items.Count >= space) return false;

            var itemInInventory = Items.Find(x => x.Item ==  item);

            if (itemInInventory != null)
                itemInInventory.SetCount(itemInInventory.Count + count);
            else
                Items.Add(new InventoryItem(item, count));

            OnItemChangedE?.Invoke();

            return true;
        }

        public bool Remove(ItemFactorySO item, int count = 1) 
        {
            var itemInInventory = Items.Find(x => x.Item == item);

            if(itemInInventory == null || itemInInventory.Count < count) return false;

            if(itemInInventory.Count == count)
                Items.Remove(itemInInventory);
            else
                itemInInventory.SetCount(itemInInventory.Count - count);

            OnItemChangedE?.Invoke();

            return true;
        }
    }

    [System.Serializable]
    public class InventoryItem
    {
        public ItemFactorySO Item { get; private set; }
        public int Count { get; private set; }

        public InventoryItem(ItemFactorySO item, int count = 1) 
        {
            Item = item;
            Count = count;
        }

        public void SetCount(int count)
        {
            Count = count;
        }
    }
}