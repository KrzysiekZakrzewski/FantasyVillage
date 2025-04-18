using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Shop
{
    public class ShopSellItemPanel : ShopItemPanel
    {
        [SerializeField] private Image itemAmountBackground;
        [SerializeField] private TextMeshProUGUI itemAmountTxt;

        public void SetupPanel(int slotId, string name, Sprite icon, int cost, Action<int> onClick, int amount = 1)
        {
            base.SetupPanel(slotId, name, icon, cost, onClick);
            UpdateAmount(slotId, amount);
        }

        public void UpdateAmount(int slotId, int amount)
        {
            this.slotId = slotId;
            itemAmountTxt.text = amount == 1 ? "" : amount.ToString();
            itemAmountBackground.color = amount == 1 ? Color.clear : Color.white;
        }
    }
}