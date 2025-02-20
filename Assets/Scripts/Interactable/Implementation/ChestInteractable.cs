using BlueRacconGames.Animation;
using BlueRacconGames.Inventory;
using Game.Item.Factory;
using System.Collections.Generic;
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

        [Inject]
        private void Inject(InventoryManager inventoryManager)
        {
            this.inventoryManager = inventoryManager;
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

        public bool AddItem(ItemFactorySO item, int count = 1)
        {
            return true;
        }

        public bool Remove(ItemFactorySO item, int count = 1)
        {
            return true;
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
    }
}