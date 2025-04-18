using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Game.Item.Factory;

namespace BlueRacconGames.InventorySystem
{
    public abstract class DraggableInventoryItemBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI amountTxt;

        protected Transform slotTransform;
        protected bool isDragging;

        public bool IsDragging => isDragging;
        public bool IsFree => iconImage.color == Color.clear;

        public void Init(Transform slotTransform)
        {
            this.slotTransform = slotTransform;
            ClearItem();
        }

        public void UpdateItem(Sprite icon, int amount)
        {
            iconImage.sprite = icon;
            iconImage.color = Color.white;

            amountTxt.text = amount > 1 ? amount.ToString() : "";
        }

        public void OnItemCrafted(ItemFactorySO itemFactory)
        {
            iconImage.sprite = itemFactory.Icon;
            iconImage.color = Color.white;

            amountTxt.text = "";
        }

        public void ClearItem()
        {
            iconImage.color = Color.clear;
            amountTxt.text = "";
        }

        public abstract void OnDrop(InventoryController inventoryController, InventoryUniqueId inventoryId, int slotId);

        public abstract void OnSlotItemRightClick(InventoryController inventoryController, InventoryUniqueId inventoryId, int slotId);

        #region Drag
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            iconImage.raycastTarget = false;
        }
        public virtual void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;

            if (!eventData.dragging) return;

            //Debug.Log(eventData.pointerCurrentRaycast);
        }
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(slotTransform);
            iconImage.raycastTarget = true;
            iconImage.transform.localPosition = Vector3.zero;
            isDragging = false;
        }
        #endregion
    }
}
