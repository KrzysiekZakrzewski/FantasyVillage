using BlueRacconGames.Animation;
using BlueRacconGames.Inventory;
using Game.Item.Factory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Interactable.Implementation
{
    public class ChestInteractable : InteractableBase
    {
        [SerializeField]
        private Animator animator;
        [field: SerializeField]
        public List<InventoryItem> Items { get; private set; } = new();

        private readonly float transitionDuration = 0.1f;
        private bool canOpen = true;
        private bool isOpened = false;

        private InventoryManager inventoryManager;

        public ChestStatusResult ChestStatus { get; private set; }

        [Inject]
        private void Inject(InventoryManager inventoryManager)
        {
            this.inventoryManager = inventoryManager;
        }

        private void Awake()
        {
            CheckChestStatus();
        }

        public override bool Interact(InteractorControllerBase interactor)
        {
            if(isOpened) return false;

            Open();

            return true;
        }

        public override void LeaveInteract(InteractorControllerBase interactor)
        {

        }

        public void AddItem(ItemFactorySO item)
        {

        }

        public void RemoveItem(ItemFactorySO item)
        {

        }

        public void Open()
        {
            if (!canOpen) return;

            isOpened = true;

            animator.CrossFade(AnimationHashIDs.OpenAnimationHash, transitionDuration);

            inventoryManager.OpenChestInventory(this);
        }

        public void Close()
        {
            animator.CrossFade(AnimationHashIDs.CloseAnimationHash, transitionDuration);

            isOpened = false;
        }

        private void CheckChestStatus()
        {
            ChestStatus = Items.Count == 0 ? ChestStatusResult.Free : ChestStatusResult.WithItems;

            if (ChestStatus == ChestStatusResult.Free)
                return;

            ChestStatus = Items.Count <= inventoryManager.ChestInventorySpace ? ChestStatusResult.WithItems : ChestStatusResult.Overload;

            if (ChestStatus == ChestStatusResult.WithItems)
                return;

            FixChestSpace();
        }

        private void FixChestSpace()
        {
            if(ChestStatus != ChestStatusResult.Overload)
                return;

            var itemsCount = Items.Count;

            Items.RemoveRange(inventoryManager.ChestInventorySpace, itemsCount);

            ChestStatus = ChestStatusResult.WithItems;
        }
    }

    public enum ChestStatusResult
    {
        Free,
        WithItems,
        Overload
    }
}