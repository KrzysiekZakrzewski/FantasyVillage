using BlueRacconGames.Inventory;
using System;
using UnityEngine;
using ViewSystem;
using ViewSystem.Implementation;

namespace Game.View
{
    public class InventoryView : BasicView
    {
        [SerializeField]
        protected InventorySlot[] slots;

        public override bool Absolute => false;

        public event Action OnInventoryOpenedE;
        public event Action OnInventoryClosedE;

        public void Initialize(InventoryUI inventoryUI)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Initialize(inventoryUI.OnSlotClicked, i);
            }
        }

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
                if (i < inventory.Items.Count)
                {
                    slots[i].AddItem(inventory.Items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }

        public int GetFirstFreeSlotId()
        {
            foreach(InventorySlot slot in slots)
            {
                if (!slot.IsFree) continue;

                return slot.Id;
            }

            return -1;
        }
    }
}
