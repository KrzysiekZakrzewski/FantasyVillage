using Game.Item.Factory;
using Interactable.Implementation;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        private Dictionary<ItemFactorySO, InventoryItem> itemDictionary = new();

        private readonly int mainInventorySpace = 36;
        private int spaceInSlot = 64;

        private InventoryUI inventoryUI;

        public ChestInteractable CurrentChestOpened;

        [field: SerializeField]
        public List<InventoryItem> Items { get; private set; } = new();
        public event Action OnInventoryChangedE;

        public List<InventoryItem> OpenedChestItems => CurrentChestOpened != null ? CurrentChestOpened.Items : new List<InventoryItem>();
        public readonly int ChestInventorySpace = 27;

        [Inject]
        private void Inject(InventoryUI inventoryUI)
        {
            this.inventoryUI = inventoryUI;
        }

        private void Awake()
        {
            OnInventoryChangedE += inventoryUI.UpdateUI;
        }

        public bool AddItem(ItemFactorySO item, int count = 1)
        {
            if (item == null || count <= 0) return false;

            if (itemDictionary.TryGetValue(item, out InventoryItem inventoryItem))
            {
                inventoryItem.Count += count;
            }
            else
            {
                itemDictionary[item] = new InventoryItem(item, count);
            }

            OnInventoryChangedE?.Invoke();
            return true;
        }

        public bool Remove(ItemFactorySO item, int count = 1) 
        {
            if (!itemDictionary.TryGetValue(item, out InventoryItem inventoryItem) || count <= 0) return false;

            inventoryItem.Count -= count;
            if (inventoryItem.Count <= 0)
            {
                itemDictionary.Remove(item);
            }

            OnInventoryChangedE?.Invoke();
            return true;
        }

        public bool GetFromChest(ItemFactorySO item, int count = 1)
        {
            return true;
        }

        public bool PutInChest(ItemFactorySO item, int count = 1)
        {
            return true;
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

        public InventoryItem(ItemFactorySO item, int count = 1) 
        {
            Item = item;
            Count = count;
        }
    }
}