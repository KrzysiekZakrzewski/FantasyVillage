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
        [SerializeField] private MainInventoryView mainInventoryView;
        [SerializeField] private ChestInventoryView chestInventoryView;

        [NonSerialized] protected Inputs.PlayerInput playerInput;

        private InventorySlot selectedSlot;
        private InventoryManager inventoryManager;

        public event Action OnInventoryUIOpenedE;
        public event Action OnInventoryUIClosedE;

        public bool IsMainInventoryOpen => mainInventoryView.gameObject.activeSelf;
        public bool IsChestInventoryOpen => chestInventoryView.gameObject.activeSelf;

        [Inject]
        private void Inject(InventoryManager inventoryManager)
        {
            this.inventoryManager = inventoryManager;
        }

        protected override void Awake()
        {
            base.Awake();

            playerInput = InputManager.GetPlayer(0);

            mainInventoryView.Initialize(this);
            chestInventoryView.Initialize(this);

            chestInventoryView.OnInventoryOpenedE += UpdateUI;

            SetupInputs();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            RemoveInputs();

            chestInventoryView.OnInventoryOpenedE -= UpdateUI;
            //chestInventoryView.OnChestInventoryClosedE += UpdateUI;
        }

        private void Update()
        {
            SlotMovable();   
        }

        public void OpenMainInventory()
        {
            if (IsMainInventoryOpen) return;

            TryOpenSafe<MainInventoryView>();
            OnInventoryUIOpenedE?.Invoke();
        }

        public void OpenChestInventory()
        {
            if (IsChestInventoryOpen) return;

            OpenMainInventory();

            mainInventoryView.ParentStack.Push(chestInventoryView);
        }

        public void CloseInventory()
        {
            Clear();
            OnInventoryUIClosedE?.Invoke();
        }

        public void OnSlotClicked(InventorySlot clickedSlot)
        {
            if (clickedSlot == null) return;

            if (clickedSlot.IsFree && selectedSlot == null) return;

            if (selectedSlot == null)
            {
                selectedSlot = clickedSlot;
                selectedSlot.Select();
                return;
            }

            if (selectedSlot == clickedSlot)
            {
                selectedSlot.ResetPosition();
                selectedSlot = null;

                return;
            }

            var changeSlotType = CheckChangeSlotType(clickedSlot);

            inventoryManager.TryChangeSlotItem(selectedSlot, clickedSlot, changeSlotType);

            clickedSlot.ValidStatus = selectedSlot.ValidStatus = ValidStatus.WaitForUpdate;

            selectedSlot = null;

            UpdateUI();
        }

        public int GetFirstFreeSlotId<InventoryView>()
        {
            return mainInventoryView.GetFirstFreeSlotId();
        }

        public void UpdateUI()
        {
            mainInventoryView.UpdateUI(inventoryManager);

            if (IsChestInventoryOpen)
                chestInventoryView.UpdateUI(inventoryManager);
        }

        private void ChangeMainInventoryState(InputAction.CallbackContext callback)
        {
            if(IsMainInventoryOpen)
                CloseInventory();
            else
                OpenMainInventory();
        }

        private void SetupInputs()
        {
            playerInput.AddInputEventDelegate(ChangeMainInventoryState, InputActionEventType.ButtonPressed, InputUtilities.Inventory);
        }

        private void RemoveInputs()
        {
            playerInput.RemoveInputEventDelegate(ChangeMainInventoryState);
        }

        private void SlotMovable()
        {
            if (selectedSlot == null)
                return;

            selectedSlot.Move(playerInput.GetCoordinates());
        }

        private void SlotUpdate(ChangeSlotResult result)
        {
            switch (result)
            {
                case ChangeSlotResult.Error:
                    Debug.LogError("Change Slot Error!!!");
                    break;
                case ChangeSlotResult.EmptyPlace:

                    break;
                case ChangeSlotResult.CountIncreased:

                    break;
                case ChangeSlotResult.Swap:

                    break;
            }


            selectedSlot = null;
        }
        private ChangeSlotType CheckChangeSlotType(InventorySlot clickedSlot)
        {
            if (selectedSlot.InventorySlotType == clickedSlot.InventorySlotType)
                return ChangeSlotType.SameInventorySpace;

            return selectedSlot.InventorySlotType switch
            {
                InventorySlotType.Main => ChangeSlotType.ToChestSpace,
                InventorySlotType.Chest => ChangeSlotType.ToMainSpace,
                _ => ChangeSlotType.Error,
            };
        }
    }
    public enum ChangeSlotType
    {
        Error,
        SameInventorySpace,
        ToChestSpace,
        ToMainSpace
    }
}