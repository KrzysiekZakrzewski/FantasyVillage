using BlueRacconGames.Animation;
using BlueRacconGames.Inventory;
using Game.Item.Factory;
using UnityEngine;
using Zenject;

namespace Interactable.Implementation
{
    public class ChestInteractable : InteractableBase
    {
        [SerializeField]
        private ItemFactorySO item;
        [SerializeField]
        private Animator animator;

        private float transitionDuration = 0.1f;
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

        private void Open()
        {
            if (!canOpen) return;

            isOpened = true;

            animator.CrossFade(AnimationHashIDs.OpenAnimationHash, transitionDuration);

            inventoryManager.Add(item);
        }

        private void Close()
        {
            animator.CrossFade(AnimationHashIDs.OpenAnimationHash, transitionDuration);

            isOpened = false;
        }
    }
}