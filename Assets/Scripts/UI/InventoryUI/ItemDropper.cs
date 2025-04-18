using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace BlueRacconGames.InventorySystem
{
    public class ItemDropper : MonoBehaviour, IDropHandler
    {
        private InventoryController inventoryController;

        [Inject]
        private void Inject(InventoryController inventoryController)
        {
            this.inventoryController = inventoryController;
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;

            DraggableInventoryItemBase draggableItem = dropped.GetComponent<DraggableInventoryItemBase>();

            if (draggableItem.IsFree) return;

            //inventoryController.DropItemFromInventory(draggableItem.in)
        }
    }
}