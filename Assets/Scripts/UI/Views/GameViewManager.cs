using BlueRacconGames.InventorySystem;
using Inputs;
using Interactable.Implementation;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ViewSystem.Implementation;
using Zenject;

namespace Game.View
{
    public class GameViewManager : SingleViewTypeStackController
    {
        [SerializeField] private InventoryViewManager inventoryViewManager;
        [SerializeField] private ShopViewManager shopViewManager;

        [NonSerialized] protected Inputs.PlayerInput playerInput;

        public InventoryViewManager InventoryViewManager => inventoryViewManager;
        public ShopViewManager ShopViewManager => shopViewManager;

        protected override void Awake()
        {
            base.Awake();

            playerInput = InputManager.GetPlayer(0);

            SetupInputs();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            RemoveInputs();
        }

        public void OpenShopView()
        {
            TryOpenSafe<ShopViewManager>();
        }
        public void OpenInventory(InventoryUniqueId inventoryId)
        {
            TryOpenSafe<InventoryViewManager>();

            inventoryViewManager.OpenInventoryView(inventoryId);
        }

        private void CloseAll(InputAction.CallbackContext callback)
        {
            Clear();
        }
        private void OpenMainInventory()
        {
            TryOpenSafe<InventoryViewManager>();

            inventoryViewManager.OpenMainInventory();
        }
        private void ChangeMainInventoryState(InputAction.CallbackContext callback)
        {
            if (inventoryViewManager.IsOpened)
                CloseInventory();
            else
                OpenMainInventory();
        }
        private void CloseInventory()
        {
            inventoryViewManager.CloseAllInventoryView();
        }
        private void RemoveInputs()
        {
            playerInput.RemoveInputEventDelegate(ChangeMainInventoryState);
        }
        private void SetupInputs()
        {
            playerInput.AddInputEventDelegate(ChangeMainInventoryState, InputActionEventType.ButtonPressed, InputUtilities.Inventory);
            playerInput.AddInputEventDelegate(CloseAll, InputActionEventType.ButtonPressed, InputUtilities.Close);
        }
    }
}