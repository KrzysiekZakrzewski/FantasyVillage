using Interactable.Implementation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewSystem.Implementation;
using Zenject;

namespace BlueRacconGames.InventorySystem
{
    public abstract class InventoryUIViewBase : BasicView
    {
        [SerializeField] private InventoryUniqueId inventoryId;
        [SerializeField] private InventorySlotBase[] slots;

        private InventoryController inventoryController;

        public override bool Absolute => false;
        public InventoryUniqueId InventoryId => inventoryId;

        [Inject]
        private void Inject(InventoryController inventoryController)
        {
            this.inventoryController = inventoryController;
        }

        private void Start()
        {
            StartCoroutine(WaitForInventoryInitialize());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
                
            var inventory = inventoryController.GetInventory(inventoryId);

            inventory.OnItemChangedE -= UpdateSlot;
        }

        public void Init()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Init(i, inventoryId);

                var item = inventoryController.GetItemByPos(inventoryId, i);

                slots[i].UpdateSlot(item);
            }

            var inventory = inventoryController.GetInventory(inventoryId);

            inventory.OnItemChangedE += UpdateSlot;
        }

        public void UpdateSlot(int slotId, InventoryItem item)
        {
            if (!IsSlotExist(slotId)) return;

            slots[slotId].UpdateSlot(item);
        }

        public void ManuallyAllSlotsUpdate()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                var item = inventoryController.GetItemByPos(inventoryId, i);

                slots[i].UpdateSlot(item);
            }
        }

        private bool IsSlotExist(int slotId)
        {
            return slotId >= 0 && slotId < slots.Length; 
        }

        private IEnumerator WaitForInventoryInitialize()
        {
            yield return new WaitUntil(inventoryController.IsInitialized);

            Init();
        }
    }
}