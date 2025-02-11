using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BlueRacconGames.Inventory
{
    public class ItemButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Image icon;
        private InventorySlot slot;

        private InventorySlot cacheSlot;
        private GameObject cachePointer;

        private void Awake()
        {
            icon = GetComponent<Image>();
            slot = GetComponentInParent<InventorySlot>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            icon.raycastTarget = false;
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;

            if(cachePointer == eventData.pointerCurrentRaycast.gameObject) return;

            cachePointer = eventData.pointerCurrentRaycast.gameObject;

            if(!cachePointer.TryGetComponent<InventorySlot>(out var selectedSlot)) return;

            if(selectedSlot == cacheSlot) return;

            cacheSlot = selectedSlot;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            icon.raycastTarget = true;
            transform.localPosition = Vector3.zero;

            cacheSlot = null;
        }
    }
}