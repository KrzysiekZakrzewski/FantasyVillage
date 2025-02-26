using Game.View;
using Inputs;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using ViewSystem.Implementation;
using Zenject;

namespace BlueRacconGames.Inventory.UI
{
    public class InventoryUI : SingleViewTypeStackController
    {
        [SerializeField] private MainInventoryView mainInventoryView;
        [SerializeField] private SubInventoryView subInventoryView;

        [NonSerialized] protected Inputs.PlayerInput playerInput;

        private IInventorySlot selectedSlot;
        private InventoryManager inventoryManager;

        public event Action OnInventoryUIOpenedE;
        public event Action OnInventoryUIClosedE;

        public bool IsMainInventoryOpen => mainInventoryView.gameObject.activeSelf;
        public bool IsSubInventoryOpened => subInventoryView.gameObject.activeSelf;

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
            subInventoryView.Initialize(this);

            subInventoryView.OnInventoryOpenedE += UpdateUI;

            SetupInputs();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            RemoveInputs();

            subInventoryView.OnInventoryOpenedE -= UpdateUI;
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

        public void OpenSubInventory()
        {
            if (IsSubInventoryOpened) return;

            OpenMainInventory();

            mainInventoryView.ParentStack.Push(subInventoryView);
        }

        public void CloseInventory()
        {
            Clear();
            OnInventoryUIClosedE?.Invoke();
        }

        public void OnSlotClicked(IInventorySlot clickedSlot)
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

            CheckChangeSlotType(clickedSlot);

            selectedSlot.ValidStatus = clickedSlot.ValidStatus = ValidStatus.WaitForUpdate;

            selectedSlot = null;

            UpdateUI();
        }

        public int GetFirstFreeSlotId()//TO DO dodaæ, by szuka³o po ró¿nych inventory
        {
            return mainInventoryView.GetFirstFreeSlotId();
        }

        public void UpdateUI()
        {
            mainInventoryView.UpdateUI(inventoryManager);

            if (!IsSubInventoryOpened) return;

            subInventoryView.UpdateUI(inventoryManager);
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

        private void CheckChangeSlotType(IInventorySlot clickedSlot)
        {
            if (selectedSlot.Type == clickedSlot.Type)
            {
                inventoryManager.TransferBetweenSameInventory(selectedSlot.Id, clickedSlot.Id, clickedSlot.Type);
                return;
            }

            switch (clickedSlot.Type)
            {
                case SlotType.Main:
                    inventoryManager.TransferToMainInventory(selectedSlot.Id, clickedSlot.Id);
                    break;
                case SlotType.Sub:
                    inventoryManager.TransferFromMainInventory(selectedSlot.Id, clickedSlot.Id);
                    break;
            }
        }//TO DO refaktoryzacja 
    }
}