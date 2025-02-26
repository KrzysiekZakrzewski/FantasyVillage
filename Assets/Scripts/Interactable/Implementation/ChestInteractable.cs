using BlueRacconGames.Animation;
using BlueRacconGames.Inventory;
using Game.Item.Factory;
using Game.View;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Interactable.Implementation
{
    public class ChestInteractable : InteractableBase
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private ChestInventory inventory = new();
        private readonly float transitionDuration = 0.1f;
        private bool canOpen = true;
        private bool isOpened = false;

        private InventoryManager inventoryManager;

        [Inject]
        private void Inject(InventoryManager inventoryManager)
        {
            this.inventoryManager = inventoryManager;
        }

        private void Awake()
        {
            inventory.Initialize(inventoryManager);
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

        public void Open()
        {
            if (!canOpen) return;

            isOpened = true;

            animator.CrossFade(AnimationHashIDs.OpenAnimationHash, transitionDuration);

            inventoryManager.OpenSubInventory(inventory);
            inventoryManager.InventoryUI.OnInventoryUIClosedE += Close;
        }

        public void Close()
        {
            animator.CrossFade(AnimationHashIDs.CloseAnimationHash, transitionDuration);
            inventoryManager.InventoryUI.OnInventoryUIClosedE -= Close;
            isOpened = false;
        }
    }
}