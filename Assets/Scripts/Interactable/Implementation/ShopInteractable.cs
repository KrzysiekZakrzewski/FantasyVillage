using BlueRacconGames.Animation;
using BlueRacconGames.InventorySystem;
using Game.Item.Factory;
using Game.Managers;
using Game.View;
using Interactable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ViewSystem;
using Zenject;

namespace Assets.Scripts.Interactable.Implementation
{
    public class ShopInteractable : InteractableBase
    {
        [SerializeField] private ItemFactorySO[] shopItems;
        private bool canOpen = true;
        private bool isOpened = false;

        private GameViewManager gameViewManager;
        private ShopManager shopManager;

        [Inject]
        private void Inject(GameViewManager gameViewManager, ShopManager shopManager)
        {
            this.gameViewManager = gameViewManager;
            this.shopManager = shopManager;
        }

        public override bool Interact(InteractorControllerBase interactor)
        {
            if (isOpened) return false;

            Open();

            return true;
        }

        public override void LeaveInteract(InteractorControllerBase interactor)
        {

        }
        public void Open()
        {
            if (!canOpen) return;

            isOpened = true;

            gameViewManager.OpenShopView();
            shopManager.GenerateShopEnviroment(shopItems);

            gameViewManager.ShopViewManager.Presentation.OnHidePresentationComplete += Close;
        }

        private void Close(IAmViewPresentation viewPresentation)
        {
            gameViewManager.ShopViewManager.Presentation.OnHidePresentationComplete -= Close;

            isOpened = false;
        }
    }
}
