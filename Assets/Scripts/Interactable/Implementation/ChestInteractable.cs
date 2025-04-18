using BlueRacconGames.Animation;
using BlueRacconGames.InventorySystem;
using Game.Item.Factory;
using Game.View;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ViewSystem;
using Zenject;

namespace Interactable.Implementation
{
    public class ChestInteractable : InteractableBase
    {
        [SerializeField] private Animator animator;
        [SerializeField] private InventoryUniqueId inventoryId;
        [SerializeField] private InventoryItem[] items;
        private readonly float transitionDuration = 0.1f;
        private bool canOpen = true;
        private bool isOpened = false;

        private GameViewManager gameViewManager;
        private InventoryController inventoryController;

        [Inject]
        private void Inject(GameViewManager gameViewManager, InventoryController inventoryController)
        {
            this.gameViewManager = gameViewManager;
            this.inventoryController = inventoryController;
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

            inventoryController.ImportInventory(inventoryId, items);

            gameViewManager.OpenInventory(inventoryId);

            animator.CrossFade(AnimationHashIDs.OpenAnimationHash, transitionDuration);

            gameViewManager.InventoryViewManager.Presentation.OnHidePresentationComplete += Close;
        }

        private void Close(IAmViewPresentation viewPresentation)
        {
            gameViewManager.InventoryViewManager.Presentation.OnHidePresentationComplete -= Close;

            Debug.Log("UU");

            animator.CrossFade(AnimationHashIDs.CloseAnimationHash, transitionDuration);

            items = inventoryController.ExportInventory(inventoryId);

            isOpened = false;
        }
    }
}