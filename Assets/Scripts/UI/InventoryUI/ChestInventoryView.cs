using BlueRacconGames.Inventory;
using Game.Item.Factory;
using System;
using UnityEngine;
using ViewSystem;
using ViewSystem.Implementation;

namespace Game.View
{
    public class ChestInventoryView : BasicView
    {
        [SerializeField]
        private InventorySlot[] slots;

        public event Action OnChestInventoryOpenedE;
        public event Action OnChestInventoryClosedE;

        public override bool Absolute => false;
        public override bool IsPopup => true;

        public override void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            base.NavigateTo(previousViewStackItem);

            OnChestInventoryOpenedE?.Invoke();
        }

        public override void NavigateFrom(IAmViewStackItem nextViewStackItem)
        {
            base.NavigateFrom(nextViewStackItem);

            OnChestInventoryClosedE?.Invoke();
        }

        public void Initialize(InventoryUI inventoryUI)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].OnSlotClickE += inventoryUI.OnSlotClicked;
            }
        }

        public void UpdateUI(InventoryManager inventory)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.OpenedChestItems.Count)
                {
                    slots[i].AddItem(inventory.OpenedChestItems[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }
}
