using Game.Item.Factory;
using Game.UI.Shop;
using System.Collections.Generic;
using UnityEngine;

namespace Game.View
{
    public class ShopBuyView : ShopViewBase
    {
        [SerializeField] private ShopSellView sellView;
        [SerializeField] private UIButtonBase swipeViewButton;

        protected override void OnDestroy()
        {
            base.OnDestroy();

            swipeViewButton.OnClickE -= SwipeToSellView;
        }

        public override void Init()
        {
            base.Init();

            swipeViewButton.OnClickE += SwipeToSellView;
        }

        protected override void ConfirmButtonValidate(int totalCost)
        {
            confirmButton.SetInteractable(shopManager.CanBuyValidate(totalCost));
        }
        protected override void OnConfirmButtonClick()
        {
            shopManager.TryBuyItem();
        }
        protected override void SpawnIcons(Dictionary<int, ItemFactorySO> itemsLUT)
        {
            ClearItemStack();

            for (int i = 0; i < itemsLUT.Count; i++)
            {
                if (!itemsLUT.TryGetValue(i, out ItemFactorySO item)) continue;

                GameObject newItemPanelGO = Instantiate(itemPanelPrefab, itemsGrid.transform);

                ShopItemPanel newItemPanel = newItemPanelGO.GetComponent<ShopItemPanel>();
                newItemPanel.SetupPanel(i, item.Name, item.Icon, item.BaseBuyCost, OnIconClick);
                shopItemPanels.Add(item, newItemPanel);
            }
        }

        private void SwipeToSellView()
        {
            SwipeToOtherShopView(sellView);
            shopManager.OnShopStateSwiped(Managers.ShopState.Sell);
        }
    }
}