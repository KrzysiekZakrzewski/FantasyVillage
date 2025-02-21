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

            inventoryUI.UpdateUI();
        }

        public bool AddItem(ItemFactorySO item, int count = 1)
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

        public bool AddItem(ItemFactorySO item, int count, int slotId)
        {
            if (item == null || count <= 0) return false;

            Items.Add(new InventoryItem(item, slotId, count));
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
        public InventoryItem GetItemBySlotIdFromChest(int slotId)
        {
            return OpenedChestItems.Find(x => x.SlotId == slotId);
        }

        public void ChangeSlot(int oldSlotId, int newSlotId, InventorySlotType slotType)
        {
            var selectedItem = slotType == InventorySlotType.Main 
                ? GetItemBySlotId(oldSlotId) : GetItemBySlotIdFromChest(oldSlotId);
            var clickedItem = slotType == InventorySlotType.Main
                ? GetItemBySlotId(newSlotId) : GetItemBySlotIdFromChest(newSlotId);

            if (clickedItem == null)
            {
                selectedItem.SetSlotId(newSlotId);

                return;
            }

            if(selectedItem.Item == clickedItem.Item)
            {
                RemoveItemBySlot(oldSlotId);
                AddItem(selectedItem.Item, selectedItem.Count);

                return;
            }

            clickedItem.SetSlotId(oldSlotId);
        }

        public void GetFromChest(int oldSlotId, int newSlotId)
        {
            var selectedItem = GetItemBySlotIdFromChest(oldSlotId);
            var clickedItem = GetItemBySlotId(newSlotId);

            CurrentChestOpened.RemoveItemBySlot(oldSlotId);

            if (clickedItem == null)
            {
                AddItem(selectedItem.Item, selectedItem.Count, newSlotId);
                return;
            }

            if (selectedItem.Item == clickedItem.Item)
            {
                AddItem(selectedItem.Item, selectedItem.Count);

                return;
            }

            RemoveItemBySlot(newSlotId);

            AddItem(selectedItem.Item, selectedItem.Count, newSlotId);
            CurrentChestOpened.AddItem(clickedItem.Item, clickedItem.Count, oldSlotId);
        }

        public void PutInChest(int oldSlotId, int newSlotId)
        {
            var selectedItem = GetItemBySlotId(oldSlotId);
            var clickedItem = GetItemBySlotIdFromChest(newSlotId);

            RemoveItemBySlot(oldSlotId);

            if (clickedItem == null)
            {
                CurrentChestOpened.AddItem(selectedItem.Item, selectedItem.Count, newSlotId);

                return;
            }

            if (selectedItem.Item == clickedItem.Item)
            {
                CurrentChestOpened.AddItem(selectedItem.Item, selectedItem.Count);

                return;
            }

            CurrentChestOpened.RemoveItemBySlot(newSlotId);
            CurrentChestOpened.AddItem(selectedItem.Item, selectedItem.Count, newSlotId);
            AddItem(clickedItem.Item, clickedItem.Count, oldSlotId);
        }

        public void TryChangeSlotItem(InventorySlot selectedSlot, InventorySlot clickedSlot, ChangeSlotType changeSlotType)
        {
            switch (changeSlotType)
            {
                case ChangeSlotType.SameInventorySpace:
                    ChangeSlot(selectedSlot.Id, clickedSlot.Id, selectedSlot.InventorySlotType);
                    break;
                case ChangeSlotType.ToMainSpace:
                    GetFromChest(selectedSlot.Id, clickedSlot.Id);
                    break;
                case ChangeSlotType.ToChestSpace:
                    PutInChest(selectedSlot.Id, clickedSlot.Id);
                    break;
                default:
                    return;
            }
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

        public void SetSlotId(int slotId) => SlotId = slotId;
    }

    public enum ChangeSlotResult
    {
        Error,
        EmptyPlace,
        CountIncreased,
        Swap
    }
}