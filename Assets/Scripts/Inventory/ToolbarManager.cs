using BlueRacconGames.Animation;
using Game.Item;
using Inputs;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using PlayerInput = Inputs.PlayerInput;

namespace BlueRacconGames.InventorySystem
{
    [Serializable]
    public class ToolbarManager : MonoBehaviour
    {
        [field: SerializeField] public InventoryUniqueId InventoryId { get; private set; }
        [SerializeField] private UnitAnimationControllerBase animationController;

        [NonSerialized] private PlayerInput playerInput;

        private readonly int maxSlotId = 5;
        private IItemRuntimeLogic currentItem;
        private int selectedSlotId = -1;

        private InventoryController inventoryController;

        public event Action<int, int> OnToolbarSlotChangedE;
        public event Action<IItemRuntimeLogic> OnItemRefreshedE;
        public UsableItemBase UsableItem { get; private set; }

        [Inject]
        private void Inject(InventoryController inventoryController)
        {
            this.inventoryController = inventoryController;
        }

        private void Awake()
        {
            SetupInput();

            StartCoroutine(WaitForInventoryInitialize());
        }

        private void OnDestroy()
        {
            playerInput.AnyButtonDown -= CheckInput;

            var inventory = inventoryController.GetInventory(InventoryId);
            inventory.OnItemChangedE -= RefreshSelectedItem;
        }

        public void UseItem()
        {
            if (UsableItem == null) return;

            var result = UsableItem.Use(animationController);
        }

        private void ChangeSelectedSlot(int newSlotId)
        {
            if (selectedSlotId == newSlotId) return;

            OnToolbarSlotChangedE?.Invoke(selectedSlotId, newSlotId);

            selectedSlotId = newSlotId;

            RefreshSelectedItem(newSlotId);
        }

        private void RefreshSelectedItem(int slotId)
        {
            if (slotId >= maxSlotId) return;

            var item = inventoryController.GetItemByPos(InventoryId, slotId);
            RefreshSelectedItem(slotId, item);
        }

        private void RefreshSelectedItem(int slotId, InventoryItem item)
        {
            if (slotId >= maxSlotId) return;

            UsableItem = null;

            if (item == null || item.IsNullOrEmpty()) return;

            currentItem = item.ItemFactory.CreateItem();

            OnItemRefreshedE?.Invoke(currentItem);

            if (currentItem is not UsableItemBase) return;

            UsableItem = (UsableItemBase)currentItem;
        }

        private void SetupInput()
        {
            playerInput = InputManager.GetPlayer(0);

            playerInput.AnyButtonDown += CheckInput;
        }

        private void CheckInput(InputControl inputControl)
        {
            if (inputControl == null || inputControl.displayName == null) return;

            bool isNumber = int.TryParse(inputControl.displayName, out int number);

            if (!isNumber || number > maxSlotId) return;

            ChangeSelectedSlot(number - 1);
        }

        private IEnumerator WaitForInventoryInitialize()
        {
            yield return new WaitUntil(inventoryController.IsInitialized);

            var inventory = inventoryController.GetInventory(InventoryId);

            inventory.OnItemChangedE += RefreshSelectedItem;

            ChangeSelectedSlot(0);
        }
    }
}