using Game.Item.Factory;
using Game.Managers;
using Game.UI.Shop;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ViewSystem;
using ViewSystem.Implementation;
using Zenject;

namespace Game.View
{
    public abstract class ShopViewBase : BasicView
    {
        [SerializeField] protected GridLayoutGroup itemsGrid;
        [SerializeField] protected GameObject itemPanelPrefab;
        [SerializeField] protected TextMeshProUGUI playerMoneyTxt;
        [SerializeField] protected ShopItemDetailsPanel detailsPanel;
        [SerializeField] protected ShopCostPanelBase totalCostPanel;
        [SerializeField] protected UIButtonBase confirmButton;

        protected ShopManager shopManager;
        protected MoneyManager moneyManager;
        protected Dictionary<ItemFactorySO, ShopItemPanel> shopItemPanels = new();

        public override bool Absolute => true;

        [Inject]
        private void Inject(ShopManager shopManager, MoneyManager moneyManager)
        {
            this.shopManager = shopManager;
            this.moneyManager = moneyManager;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            shopManager.OnItemsGeneratedE -= SpawnIcons;
            shopManager.OnItemChangedE -= detailsPanel.UpdatePanel;
            shopManager.OnAmountChangedE -= OnAmountChanged;
            moneyManager.OnMoneyChangedE -= PlayerMoneyUpdate;
            confirmButton.OnClickE -= OnConfirmButtonClick;
        }

        public override void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            base.NavigateTo(previousViewStackItem);

            PlayerMoneyUpdate(moneyManager.MoneyAmount);
        }

        public virtual void Init()
        {
            totalCostPanel.SetupButtonAction(OnIncreaseAmountClick, OnDecreaseAmountClick);

            shopManager.OnItemsGeneratedE += SpawnIcons;
            shopManager.OnAmountChangedE += OnAmountChanged;
            shopManager.OnItemChangedE += detailsPanel.UpdatePanel;
            moneyManager.OnMoneyChangedE += PlayerMoneyUpdate;
            confirmButton.OnClickE += OnConfirmButtonClick;
        }

        protected abstract void ConfirmButtonValidate(int totalCost);
        protected abstract void OnConfirmButtonClick();
        protected abstract void SpawnIcons(Dictionary<int, ItemFactorySO> itemsLUT);
        protected virtual void SwipeToOtherShopView(IAmViewStackItem view)
        {
            ParentStack.TryPushSafe(view);
        }
        protected void PlayerMoneyUpdate(int value)
        {
            playerMoneyTxt.text = value.ToString();
        }
        protected void OnIconClick(int slotId)
        {
            shopManager.SelectItem(slotId);
        }
        protected void ClearItemStack()
        {
            if (shopItemPanels.Count <= 0) return;

            foreach(ShopItemPanel panel in shopItemPanels.Values)
            {
                Destroy(panel.gameObject);
            }

            shopItemPanels.Clear();
        }

        private void OnIncreaseAmountClick()
        {
            shopManager.ChangeItemAmount(true);
        }
        private void OnDecreaseAmountClick()
        {
            shopManager.ChangeItemAmount(false);
        }
        private void OnAmountChanged(int amount, int totalCost)
        {
            totalCostPanel.UpdatePanel(amount, totalCost);
            ConfirmButtonValidate(totalCost);
        }
    }
}