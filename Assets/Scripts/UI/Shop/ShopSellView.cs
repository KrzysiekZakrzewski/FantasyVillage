using BlueRacconGames.InventorySystem;
using Game.Item.Factory;
using Game.UI.Shop;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.View
{
    public class ShopSellView : ShopViewBase
    {
        [SerializeField] private ShopBuyView buyView;
        [SerializeField] private UIButtonBase swipeViewButton;

        protected override void OnDestroy()
        {
            base.OnDestroy();

            swipeViewButton.OnClickE -= SwipeToBuyView;
            shopManager.OnTransactionValidateE -= UpdateItem;
        }

        public override void Init()
        {
            base.Init();

            swipeViewButton.OnClickE += SwipeToBuyView;
            shopManager.OnTransactionValidateE += UpdateItem;
        }

        protected override void ConfirmButtonValidate(int totalCost)
        {
            confirmButton.SetInteractable(shopManager.CanSellValidate());
        }
        protected override void OnConfirmButtonClick()
        {
            shopManager.TrySellItem();
        }
        protected override void SpawnIcons(Dictionary<int, ItemFactorySO> itemsLUT)
        {
            Debug.Log("SIS");

            ClearItemStack();

            InventoryItem[] inventoryItems = shopManager.GetItemsFromInventory();

            foreach (InventoryItem item in inventoryItems)
            {
                SpawnIcon(item);
            }
        }

        private void UpdateItem(InventoryItem item, int amount)
        {
            if (!shopItemPanels.TryGetValue(item.ItemFactory, out var shopItem) && amount > 0)
            {
                SpawnIcon(item);
                return;
            }

            if(amount <= 0)
            {
                Destroy(shopItem.gameObject);
                shopItemPanels.Remove(item.ItemFactory);
                return;
            }

            ShopSellItemPanel shopSellItem = shopItem as ShopSellItemPanel;

            shopSellItem.UpdateAmount(item.SlotId, amount);
        }

        private void SpawnIcon(InventoryItem item)
        {
            if (item.IsNullOrEmpty()) return;

            if (shopItemPanels.ContainsKey(item.ItemFactory)) return;

            GameObject newItemPanelGO = Instantiate(itemPanelPrefab, itemsGrid.transform);
            ShopSellItemPanel newItemPanel = newItemPanelGO.GetComponent<ShopSellItemPanel>();

            ItemFactorySO itemFactory = item.ItemFactory;

            int itemAmount = shopManager.GetItemCount(itemFactory);

            newItemPanel.SetupPanel(item.SlotId, itemFactory.Name, itemFactory.Icon, itemFactory.BaseSellCost, OnIconClick, itemAmount);
            shopItemPanels.Add(item.ItemFactory, newItemPanel);
        }
        private void SpawnIcon(ItemFactorySO item)
        {
            InventoryItem inventoryItem = shopManager.GetItemFromInventory(item);

            if (shopItemPanels.ContainsKey(item)) return;

            GameObject newItemPanelGO = Instantiate(itemPanelPrefab, itemsGrid.transform);
            ShopSellItemPanel newItemPanel = newItemPanelGO.GetComponent<ShopSellItemPanel>();

            int itemAmount = shopManager.GetItemCount(item);

            newItemPanel.SetupPanel(inventoryItem.SlotId, item.Name, item.Icon, item.BaseSellCost, OnIconClick, itemAmount);
            shopItemPanels.Add(item, newItemPanel);
        }
        private void SwipeToBuyView()
        {
            SwipeToOtherShopView(buyView);
            shopManager.OnShopStateSwiped(Managers.ShopState.Buy);
        }
    }
}