using ViewSystem.Implementation;
using UnityEngine;
using System.Collections.Generic;
using Interactable.Implementation;

namespace BlueRacconGames.InventorySystem
{
    public class InventoryViewManager : SingleViewTypeStackController
    {
        [SerializeField] private InventoryUIViewBase[] inventoryViews;

        private Dictionary<InventoryUniqueId, InventoryUIViewBase> inventoryViewLUT;

        public bool IsOpened => gameObject.activeSelf;

        protected override void Awake()
        {
            base.Awake();

            inventoryViewLUT = new();

            foreach(InventoryUIViewBase view in inventoryViews)
            {
                inventoryViewLUT.Add(view.InventoryId, view);
            }
        }

        public void OpenMainInventory()
        {
            TryOpenSafe<MainInventoryView>();
        }

        public void OpenInventoryView(InventoryUniqueId inventoryId)
        {
            OpenMainInventory();

            var openedView = Peek();

            inventoryViewLUT.TryGetValue(inventoryId, out var view);

            view.ManuallyAllSlotsUpdate();

            openedView.ParentStack.Push(view);
        }

        public void CloseAllInventoryView()
        {
            ParentStack.ClearStack();

            Clear();
        }
    }
}