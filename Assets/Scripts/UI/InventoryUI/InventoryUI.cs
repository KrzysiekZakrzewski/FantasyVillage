using Game.View;
using Inputs;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ViewSystem.Implementation;
using Zenject;

namespace BlueRacconGames.Inventory
{
    public class InventoryUI : SingleViewTypeStackController
    {
        [SerializeField]
        private MainInventoryView mainInventoryView;
        [SerializeField]
        private InventorySlot[] slots;

        [NonSerialized]
        protected Inputs.PlayerInput playerInput;

        private InventorySlot selectedSlot;
        private InventoryManager inventory;

        public bool IsMainInventoryOpen => mainInventoryView.gameObject.activeSelf;

        [Inject]
        private void Inject(InventoryManager inventory)
        {
            this.inventory = inventory;
        }
        
        protected override void Awake()
        {
            base.Awake();

            playerInput = InputManager.GetPlayer(0);

            SetupInputs();

            inventory.OnItemChangedE += UpdateUI;

            for (int i = 0; i < slots.Length; i++) 
            {
                slots[i].OnSlotClickE += OnSlotClicked;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            inventory.OnItemChangedE -= UpdateUI;

            RemoveInputs();
        }

        private void Update()
        {
            SlotMovable();   
        }

        private void ChangeMainInventoryState(InputAction.CallbackContext callback)
        {
            if(IsMainInventoryOpen)
                CloseMainInventory();
            else
                OpenMainInventory();
        }

        private void OpenMainInventory()
        {
            TryOpenSafe<MainInventoryView>();
        }

        private void CloseMainInventory()
        {
            TryPopSafe();
        }

        private void SetupInputs()
        {
            playerInput.AddInputEventDelegate(ChangeMainInventoryState, InputActionEventType.ButtonPressed, InputUtilities.Inventory);
        }

        private void RemoveInputs()
        {
            playerInput.RemoveInputEventDelegate(ChangeMainInventoryState);
        }

        private void UpdateUI()
        {
            for (int i = 0; i < slots.Length; i++) 
            { 
                if(i < inventory.Items.Count)
                {
                    slots[i].AddItem(inventory.Items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }

        private void OnSlotClicked(InventorySlot clickedSlot)
        {
            if(selectedSlot == clickedSlot) return;

            if (clickedSlot.IsFree)
            {
                if (selectedSlot == null)
                    return;

                selectedSlot.ChangeSlot(clickedSlot);

                selectedSlot.Deselect();
                selectedSlot = null;

                return;
            }

            selectedSlot = clickedSlot;
            selectedSlot.Select();
        }

        private void SlotMovable()
        {
            if (selectedSlot == null)
                return;

            selectedSlot.Move(playerInput.GetCoordinates());
        }
    }
}