using ViewSystem;
using ViewSystem.Implementation;
using UnityEngine;
using System.Collections.Generic;

namespace Game.View
{
    public class ShopViewManager : SingleViewTypeStackController
    {
        [SerializeField] private UIButtonBase closeButton;

        private List<ShopViewBase> shopViews;

        protected override void Awake()
        {
            base.Awake();

            closeButton.OnClickE += CloseShopPanel;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();

            closeButton.OnClickE -= CloseShopPanel;
        }

        public override void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            base.NavigateTo(previousViewStackItem);

            TryOpenSafe<ShopBuyView>();
        }

        private void CloseShopPanel()
        {
            Clear();

            ParentStack.TryPopSafe();
        }
        private void GatherViews()
        {
            shopViews.Clear();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (!TryGetComponent<ShopViewBase>(out ShopViewBase shopView)) continue;
                shopViews.Add(shopView);
                shopView.Init();
            }
        }
    }
}