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

        private void Awake()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].SetSlotId(i);
            }
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

        public bool AddItem(ItemFactorySO item, int count, int slotId)
        {
            if (item == null || count <= 0) return false;

            Items.Add(new InventoryItem(item, slotId, count));

            return true;
        }
        public bool AddItem(ItemFactorySO item, int count = 1)
        {
            if (item == null || count <= 0) return false;

            var inventoryItem = Items.Find(x => x.Item == item);

            if (inventoryItem == null) return false;

            inventoryItem.Count += count;

            return true;
        }
        public bool Remove(ItemFactorySO item, int count = 1)
        {
            var inventoryItem = Items.Find(x => x.Item == item);

            if (inventoryItem == null || count <= 0) return false;

            inventoryItem.Count -= count;

            if (inventoryItem.Count > 0) return true;

            Items.Remove(inventoryItem);

            return true;
        }

        public bool RemoveItemBySlot(int slotId)
        {
            var inventoryItem = Items.Find(x => x.SlotId == slotId);

            if (inventoryItem == null) return false;

            Items.Remove(inventoryItem);
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