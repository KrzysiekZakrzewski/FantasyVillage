using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BlueRacconGames.Inventory.UI
{
    public interface IInventorySlot : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        SlotType Type { get; }
        int Id { get; }
        bool IsFree { get; }
        ValidStatus ValidStatus { get; set; }

        event Action<IInventorySlot> OnSlotClickE;

        public void Initialize(Action<IInventorySlot> OnSlotClickedAction, int slotId);
        public void Refresh(InventoryManager inventoryManager);
        public void AddItem(InventoryItem newInventoryItem);
        public void ClearSlot();
        public void Move(Vector2 position);
        public void Select();
        public void Deselect();
        public void ResetPosition();
    }
    public enum ValidStatus
    {
        Valid,
        WaitForUpdate
    }
    public enum SlotType
    {
        Main,
        Sub
    }
}
