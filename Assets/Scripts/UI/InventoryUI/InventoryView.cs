using BlueRacconGames.Inventory;
using BlueRacconGames.Inventory.UI;
using System;
using UnityEngine;
using ViewSystem;
using ViewSystem.Implementation;

namespace Game.View
{
    public abstract class InventoryView : BasicView
    {
        [SerializeField]
        protected InventorySlotBase[] slots;

        public override bool Absolute => false;

        public event Action OnInventoryOpenedE;
        public event Action OnInventoryClosedE;

        public abstract void Initialize(InventoryUI inventoryUI);

        public override void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            base.NavigateTo(previousViewStackItem);

            OnInventoryOpenedE?.Invoke();
        }

        public override void NavigateFrom(IAmViewStackItem nextViewStackItem)
        {
            base.NavigateFrom(nextViewStackItem);

            OnInventoryClosedE?.Invoke();
        }

        public void UpdateUI(InventoryManager inventory)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Refresh(inventory);
            }
        }

        public int GetFirstFreeSlotId()
        {
            foreach(IInventorySlot slot in slots)
            {
                if (!slot.IsFree) continue;

                return slot.Id;
            }

            return -1;
        }
    }
}
