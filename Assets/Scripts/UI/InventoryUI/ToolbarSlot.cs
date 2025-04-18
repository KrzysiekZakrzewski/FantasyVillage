using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlueRacconGames.InventorySystem
{
    class ToolbarSlot : MonoBehaviour
    {
        [SerializeField] private Image targetImage;

        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Image selectedImage;

        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI amountTxt;

        public void Select()
        {
            ChangeSelectedState(true);
        }

        public void Deselect()
        {
            ChangeSelectedState(false);
        }

        public void UpdateSlot(InventoryItem inventoryItem)
        {
            if (inventoryItem.IsNullOrEmpty())
            {
                ClearSlot();
                return;
            }

            CountRefresh(inventoryItem.Amount);
            IconRefresh(inventoryItem.ItemFactory.Icon);
        }

        public void ClearSlot()
        {
            itemImage.color = Color.clear;
            amountTxt.text = "";
        }

        private void CountRefresh(int count)
        {
            amountTxt.text = count > 1 ? count.ToString() : "";
        }

        private void IconRefresh(Sprite sprite)
        {
            itemImage.sprite = sprite;
            itemImage.color = sprite == null ? Color.clear : Color.white;
        }

        private void ChangeSelectedState(bool isSelected)
        {
            if (selectedImage == null) return;

            selectedImage.gameObject.SetActive(isSelected);
        }
    }
}