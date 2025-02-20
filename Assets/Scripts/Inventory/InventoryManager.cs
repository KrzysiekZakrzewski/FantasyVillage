using Game.Item.Factory;
using Game.View;
using Interactable.Implementation;
using Saves.Serializiation;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [field: SerializeField]
        public List<InventoryItem> Items { get; private set; } = new();

        private InventoryUI inventoryUI;

        public ChestInteractable CurrentChestOpened;
        public List<InventoryItem> OpenedChestItems => CurrentChestOpened != null ? CurrentChestOpened.Items: new List<InventoryItem>();

        [Inject]
        private void Inject(InventoryUI inventoryUI)
        {
            this.inventoryUI = inventoryUI;
        }

        private void Awake()
        {
            for(int i = 0; i < Items.Count; i++)
            {
                Items[i].SetSlotId(i);
            }
        }

        public bool AddItem(ItemFactorySO item, int count = 1, int slotId = -1)
        {
            if (item == null || count <= 0) return false;

            var inventoryItem = Items.Find(x => x.Item == item);

            if (inventoryItem != null)
                inventoryItem.Count += count;
            else
            {
                var emptySlotId = inventoryUI.GetFirstFreeSlotId<MainInventoryView>();

                Items.Add(new InventoryItem(item, emptySlotId, count));
            }

            inventoryUI.UpdateUI();

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

        public InventoryItem GetItemBySlotId(int slotId)
        {
            return Items.Find(x => x.SlotId == slotId);
        }

        public bool GetFromChest(InventoryItem inventoryItem)
        {
            CurrentChestOpened.Remove(inventoryItem.Item, inventoryItem.Count);

            AddItem(inventoryItem.Item, inventoryItem.Count);

            return true;
        }

        public bool PutInChest(InventoryItem inventoryItem)
        {
            RemoveItem(inventoryItem.Item, inventoryItem.Count);

            CurrentChestOpened.AddItem(inventoryItem.Item, inventoryItem.Count);

            return true;
        }

        public void TryChangeSlotItem()
        {

        }

        public void OpenChestInventory(ChestInteractable chest)
        {
            CurrentChestOpened = chest;

            inventoryUI.OnInventoryUIClosedE += CloseChestInventory;

            inventoryUI.OpenChestInventory();
        }

        public void CloseChestInventory()
        {
            if (CurrentChestOpened == null) return;

            inventoryUI.OnInventoryUIClosedE -= CloseChestInventory;

            CurrentChestOpened.Close();
            CurrentChestOpened = null;
        }
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

        public void SetSlotId(int slotId)
        {
            SlotId = slotId;
        }
    }
}