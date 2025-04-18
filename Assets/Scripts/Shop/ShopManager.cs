using BlueRacconGames.InventorySystem;
using Game.Item.Factory;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private InventoryUniqueId inventoryId;

        private ShopState shopState;

        private Dictionary<int, ItemFactorySO> itemsLUT;

        private ItemFactorySO currentItemSelected;
        private int itemAmount;

        private MoneyManager moneyManager;
        private InventoryController inventoryController;

        public event Action<Dictionary<int, ItemFactorySO>> OnItemsGeneratedE;
        public event Action<int, int> OnAmountChangedE;
        public event Action<ItemFactorySO> OnItemChangedE;
        public event Action<InventoryItem, int> OnTransactionValidateE;

        public InventoryController InventoryController => inventoryController;

        [Inject]
        private void Inject(MoneyManager moneyManager, InventoryController inventoryController)
        {
            this.moneyManager = moneyManager;
            this.inventoryController = inventoryController;
        }

        public bool TryBuyItem()
        {
            int totalCost = currentItemSelected.BaseBuyCost * itemAmount;

            if (!CanBuyValidate(totalCost)) return false;

            if (!moneyManager.TryRemoveMoney(totalCost)) return false;

            inventoryController.AddItem(inventoryId, currentItemSelected, itemAmount);

            OnTransactionValidate();

            return true;
        }
        public bool TrySellItem()
        {
            if (!CanSellValidate()) return false;

            int moneyEarned = currentItemSelected.BaseSellCost * itemAmount;

            inventoryController.RemoveItem(inventoryId, currentItemSelected, itemAmount);

            moneyManager.AddMoney(moneyEarned);

            OnTransactionValidate();

            return true;
        }
        public bool CanBuyValidate(int totalCost)
        {
            if (currentItemSelected == null || shopState != ShopState.Buy) return false;

            return moneyManager.HaveEnoughtMoney(totalCost);
        }
        public bool CanSellValidate()
        {
            if (currentItemSelected == null || shopState != ShopState.Sell) return false;

            return itemAmount <= inventoryController.CountItems(inventoryId, currentItemSelected);
        }
        public ItemFactorySO GetItem(int slotId)
        {
            switch (shopState)
            {
                case ShopState.Buy:
                    return itemsLUT.TryGetValue(slotId, out ItemFactorySO item) ? item : null;
                case ShopState.Sell:
                    return inventoryController.GetItemByPos(inventoryId, slotId).ItemFactory;
                case ShopState.Trade:
                    return null;
                default:
                    return null;
            }
        }
        public InventoryItem GetItemFromInventory(ItemFactorySO itemFactory)
        {
            return inventoryController.GetItem(inventoryId, itemFactory);
        }
        public void SelectItem(int slotId)
        {
            itemAmount = 1;
            currentItemSelected = GetItem(slotId);

            OnItemChangedE?.Invoke(currentItemSelected);
            OnAmountChangedE.Invoke(itemAmount, CalculateTotalCost());
        }
        public void ChangeItemAmount(bool increase = true)
        {
            if (currentItemSelected == null) return;

            itemAmount = increase ? itemAmount + 1 : itemAmount - 1;

            itemAmount = itemAmount >= 1 ? itemAmount : 1;

            OnAmountChangedE?.Invoke(itemAmount, CalculateTotalCost());
        }
        public void GenerateShopEnviroment(ItemFactorySO[] items)
        {
            itemsLUT = new();

            for (int i = 0; i < items.Length; i++)
            {
                itemsLUT.Add(i, items[i]);
            }

            OnItemsGeneratedE?.Invoke(itemsLUT);

            ClearItem();
        }
        public InventoryItem[] GetItemsFromInventory()
        {
            return inventoryController.GetInventory(inventoryId).ItemLUT;
        }
        public int GetItemCount(ItemFactorySO item)
        {
            return inventoryController.CountItems(inventoryId, item);
        }
        public int GetItemAmount(InventoryItem item)
        {
            return inventoryController.CountItems(inventoryId, item.ItemFactory);
        }
        public void OnShopStateSwiped(ShopState shopState)
        {
            this.shopState = shopState;

            ClearItem();
        } 

        private void ClearItem()
        {
            OnItemChangedE?.Invoke(currentItemSelected = null);
            OnAmountChangedE?.Invoke(0, 0);
        }
        private int CalculateTotalCost()
        {
            int baseCost = shopState == ShopState.Buy ? currentItemSelected.BaseBuyCost : currentItemSelected.BaseSellCost;

            return currentItemSelected != null ? itemAmount * baseCost : 0;
        }
        private void OnTransactionValidate()
        {
            InventoryItem item = GetItemFromInventory(currentItemSelected);

            int itemAmount = GetItemAmount(item);

            OnTransactionValidateE?.Invoke(item, itemAmount);

            if (itemAmount == 0)
                ClearItem();
        }
    }

    public enum ShopState
    {
        Buy,
        Sell,
        Trade
    }
}