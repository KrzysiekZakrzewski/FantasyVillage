using BlueRacconGames.Inventory.UI;
using Interactable.Implementation;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [field: SerializeField] public MainInventory PlayerInventory { get; private set; }

        private IInventory subInventory;
        public InventoryUI InventoryUI { get; private set; }

        [Inject]
        private void Inject(InventoryUI inventoryUI)
        {
            this.InventoryUI = inventoryUI;
        }

        private void Awake()
        {
            PlayerInventory.Initialize(this, InventoryUI.UpdateUI);
        }

        private void OnDestroy()
        {
            PlayerInventory.OnItemChangedE -= InventoryUI.UpdateUI;
        }

        public InventoryItem GetItemBySlotId(int slotId, SlotType type) => GetInventoryByType(type).GetItemBySlotId(slotId);

        public int GetFirstFreeSlotId() => InventoryUI.GetFirstFreeSlotId();

        public void TransferBetweenSameInventory(int sourceSlotId, int targetSlotId, SlotType type)
        {
            IInventory inventory = GetInventoryByType(type);

            var sourceItem = inventory.GetItemBySlotId(sourceSlotId);
            var targetItem = inventory.GetItemBySlotId(targetSlotId);

            if (targetItem == null)
            {
                sourceItem.SetSlotId(targetSlotId);
                return;
            }

            if (sourceItem.Item == targetItem.Item)
            {
                inventory.RemoveItemBySlot(sourceSlotId);
                targetItem.Count += sourceItem.Count;
                return;
            }

            sourceItem.SetSlotId(targetSlotId);
            targetItem.SetSlotId(sourceSlotId);
        }

        public void TransferToMainInventory(int sourceSlotId, int targetSlotId)
        {
            TransferItem(subInventory, PlayerInventory, sourceSlotId, targetSlotId);
        }

        public void TransferFromMainInventory(int sourceSlotId, int targetSlotId)
        {
            TransferItem(PlayerInventory, subInventory, sourceSlotId, targetSlotId);
        }

        public void OpenSubInventory(IInventory inventory)
        {
            subInventory = inventory;

            InventoryUI.OnInventoryUIClosedE += CloseSubInventory;

            InventoryUI.OpenSubInventory();
        }

        public void CloseSubInventory()
        {
            if (subInventory == null) return;

            InventoryUI.OnInventoryUIClosedE -= CloseSubInventory;

            subInventory = null;
        }

        private void TransferItem(IInventory sourceInventory, IInventory targetInventory, int sourceSlotId, int targetSlotId)
        {
            var sourceItem = sourceInventory.GetItemBySlotId(sourceSlotId);
            var targetItem = targetInventory.GetItemBySlotId(targetSlotId);

            if (sourceItem == null) return;

            sourceInventory.RemoveItemBySlot(sourceSlotId);

            if (targetItem == null)
            {
                targetInventory.AddItem(sourceItem.Item, sourceItem.Count, targetSlotId);
                return;
            }

            if (sourceItem.Item == targetItem.Item)
            {
                targetItem.Count += sourceItem.Count;
                return;
            }

            targetInventory.RemoveItemBySlot(targetSlotId);
            targetInventory.AddItem(sourceItem.Item, sourceItem.Count, targetSlotId);
            sourceInventory.AddItem(targetItem.Item, targetItem.Count, sourceSlotId);
        }

        private IInventory GetInventoryByType(SlotType type)
        {
            var inventory = type == SlotType.Main ? PlayerInventory : subInventory;
            return inventory;
        }
    }
}