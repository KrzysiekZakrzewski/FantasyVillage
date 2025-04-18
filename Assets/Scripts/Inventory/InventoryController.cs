using BlueRacconGames.UI.Bars.Presentation;
using Game.Item.Factory;
using Game.View;
using Interactable.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.InventorySystem
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private ItemFactorySO testItem;
        [SerializeField] private InventoryDataSO[] inventoryDatas;

        private SerializedDictionary<string, Inventory> inventoryLUT;
        [SerializeField]
        private InventoryViewManager inventoryViewManager;
        private bool initialized;

        [Inject]
        private void Inject()
        {

        }

        private void Start()
        {
            inventoryLUT = new();

            foreach(InventoryDataSO inventoryData in inventoryDatas)
            {
                inventoryLUT.Add(inventoryData.InventoryName, new(inventoryData));
            }

            initialized = true;
        }

        public void AddItemPos(InventoryUniqueId inventoryId, InventoryItem item, int position)
        {
            if (item == null || !inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return;

            inventory.AddItemPos(item, position);
        }

        public void AddItemPos(InventoryUniqueId inventoryId, ItemFactorySO item, int position, int amount = 1)
        {
            if (item == null || !inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return;
            InventoryItem newItem = new(item, amount);

            AddItemPos(inventoryId, newItem, position);
        }

        public bool AddItem(InventoryUniqueId inventoryId, ItemFactorySO item, int amount = 1, bool updateUI = true)
        {
            if (item == null || !inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return false;

            return inventory.AddItemAuto(item, amount, updateUI);
        }

        public void RemoveItemPos(InventoryUniqueId inventoryId, int position, int amount)
        {
            if (!inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return;
            inventory.RemoveItemInPosition(position, amount);
        }

        public void RemoveItem(InventoryUniqueId inventoryId, ItemFactorySO item, int amount = 1)
        {
            if (!inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return;

            inventory.RemoveItemAuto(item, amount);
        }

        public void RemoveItem(InventoryUniqueId inventoryId, InventoryItem item, int amount)
        {
            if (item == null || amount == 0) return;

            if (!inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return;

            inventory.RemoveItemInPosition(item, amount);
        }

        public void SwitchItemPos(InventoryUniqueId oldInventoryId, InventoryUniqueId newInventoryId, int oldSlotId, int newSlotId, InventoryItem item)
        {
            RemoveItemPos(oldInventoryId, oldSlotId, item.Amount);

            AddItemPos(newInventoryId, item, newSlotId);
        }

        public void ResizeItemAmount(InventoryUniqueId oldInventoryId, InventoryUniqueId newInventoryId, int oldSlotId, int newSlotId, InventoryItem item)
        {
            RemoveItemPos(oldInventoryId, oldSlotId, 1);

            AddItemPos(newInventoryId, item.ItemFactory, newSlotId, 1);
        }

        public bool InventoryFull(InventoryUniqueId inventoryId, ItemFactorySO item)
        {
            if (!inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return true;

            return inventory.IsFull(item);
        }

        public void InventoryClear(InventoryUniqueId inventoryId)
        {
            if (!inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return;

            inventory.ClearInventory();
        }

        public int CountItems(InventoryUniqueId inventoryId, ItemFactorySO item)
        {
            if (!inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return 0;

            return inventory.Count(item);
        }

        public Inventory GetInventory(InventoryUniqueId inventoryId)
        {
            if (!inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return null;

            return inventory;
        }

        public InventoryItem GetItemByPos(InventoryUniqueId inventoryId, int index)
        {
            if (!inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return null;

            return inventory.GetItem(index);
        }
        public InventoryItem GetItem(InventoryUniqueId inventoryId, ItemFactorySO itemFactory)
        {
            if (!inventoryLUT.TryGetValue(inventoryId.Id, out var inventory)) return null;

            return inventory.GetItem(itemFactory);
        }
        public void ImportInventory(InventoryUniqueId inventoryId, InventoryItem[] items)
        {
            InventoryClear(inventoryId);

            foreach (InventoryItem item in items)
            {
                AddItemPos(inventoryId, item, item.SlotId);
            }
        }

        public InventoryItem[] ExportInventory(InventoryUniqueId inventoryId)
        {
            var inventory = GetInventory(inventoryId);

            return inventory.ExportInventory();
        }

        public void RemoveInventory(InventoryDataSO inventoryDataSO)
        {
            if (!IsInventoryExist(inventoryDataSO.InventoryName)) return;

            inventoryLUT.Remove(inventoryDataSO.InventoryName);
        }

        public void RemoveInventory(InventoryUniqueId inventoryId)
        {
            if (!IsInventoryExist(inventoryId.Id)) return;

            inventoryLUT.Remove(inventoryId.Id);
        }

        public void DropItemFromInventory(InventoryUniqueId inventoryId, InventoryItem item, int amount)
        {
            //TO DO wrzuciæ do œwiata 

            RemoveItem(inventoryId, item, amount);
        }

        public bool IsInitialized()
        {
            return initialized;
        }

        private bool IsInventoryExist(string inventoryName)
        {
            return inventoryLUT.ContainsKey(inventoryName);
        }

        [ContextMenu("Add test Item")]
        private void TestAddItem()
        {
            inventoryLUT.TryGetValue(inventoryDatas[0].InventoryName, out var inventory);

            inventory.AddItemAuto(testItem, 5);
        }
    }
}