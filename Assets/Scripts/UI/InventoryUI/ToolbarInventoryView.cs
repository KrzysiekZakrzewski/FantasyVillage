using ViewSystem.Implementation;
using Zenject;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

namespace BlueRacconGames.InventorySystem
{
    public class ToolbarInventoryView : BasicView
    {
        [SerializeField] private ToolbarSlot[] toolbarSlots;
        [SerializeField] private TextMeshProUGUI selectedItemName;

        private float showItemNameDuration = 1f;
        private float fadeDuration = 0.33f;

        private Sequence textNameSequence;

        private ToolbarManager toolbarManager;
        private InventoryController inventoryController;

        public override bool Absolute => false;

        [Inject]
        private void Inject(ToolbarManager toolbarManager, InventoryController inventoryController)
        {
            this.toolbarManager = toolbarManager;
            this.inventoryController = inventoryController;
        }

        protected override void Awake()
        {
            base.Awake();

            toolbarManager.OnToolbarSlotChangedE += ChangeSelectedSlot;

            for (int i = 0; i < toolbarSlots.Length; i++)
            {
                toolbarSlots[i].Deselect();
            }

            textNameSequence = DOTween.Sequence();
            textNameSequence.SetAutoKill(false);

            textNameSequence.Append(selectedItemName.DOFade(1f, fadeDuration));
            textNameSequence.Append(selectedItemName.DOFade(0f, fadeDuration).SetDelay(showItemNameDuration));

            StartCoroutine(WaitForInventoryInitialize());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            toolbarManager.OnToolbarSlotChangedE -= ChangeSelectedSlot;

            var inventory = inventoryController.GetInventory(toolbarManager.InventoryId);
            inventory.OnItemChangedE -= RefreshSlot;
        }

        public void ChangeSelectedSlot(int oldSlotId, int newSlotId)
        {
            if (oldSlotId >= 0)
                toolbarSlots[oldSlotId].Deselect();

            toolbarSlots[newSlotId].Select();

            InventoryItem item = inventoryController.GetItemByPos(toolbarManager.InventoryId, newSlotId);

            selectedItemName.text = item.IsNullOrEmpty() ? "" : item.ItemFactory.Name;

            textNameSequence.Complete();

            if (selectedItemName.text == "")
                return;

            textNameSequence.Restart();
        }

        private void RefreshSlot(int slotId, InventoryItem item)
        {
            if (!IsSlotExist(slotId)) return;

            toolbarSlots[slotId].UpdateSlot(item);
        }

        private bool IsSlotExist(int slotID)
        {
            return slotID >= 0 && slotID < toolbarSlots.Length;
        }

        private IEnumerator WaitForInventoryInitialize()
        {
            yield return new WaitUntil(inventoryController.IsInitialized);

            var inventory = inventoryController.GetInventory(toolbarManager.InventoryId);
            inventory.OnItemChangedE += RefreshSlot;

            for (int i = 0; i < toolbarSlots.Length; i++)
            {
                var item = inventoryController.GetItemByPos(toolbarManager.InventoryId, i);
                toolbarSlots[i].UpdateSlot(item);
            }
        }
    }
}