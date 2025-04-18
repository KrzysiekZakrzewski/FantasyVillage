using Game.Item.Factory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Shop
{
    public class ShopItemDetailsPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI descriptionTxt;

        public void SetupPanel(string name, Sprite icon, string description)
        {
            nameTxt.text = name;
            itemImage.sprite = icon;
            descriptionTxt.text = description;
            itemImage.color = Color.white;
        }

        public void ClearPanel()
        {
            nameTxt.text = "";
            itemImage.color = Color.clear;
            descriptionTxt.text = "";
        }

        public void UpdatePanel(ItemFactorySO item)
        {
            Debug.Log("UpdatePanel");

            if(item == null)
            {
                ClearPanel();
                return;
            }

            SetupPanel(item.Name, item.Icon, item.Description);
        }
    }
}