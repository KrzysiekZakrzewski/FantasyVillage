using BlueRacconGames.Crafting;
using BlueRacconGames.Hit;
using BlueRacconGames.InventorySystem;
using BlueRacconGames.Pool;
using Game.Managers;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game.Installer
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private InventoryController inventoryController;
        [SerializeField] private ToolbarManager toolbarManager;
        [SerializeField] private GameViewManager gameViewManager;
        [SerializeField] private HitObjectControllerBase hitObjectController;
        [SerializeField] private DefaultPooledEmitter pooledEmitter;
        [SerializeField] private CraftingSystem craftingSystem;
        [SerializeField] private MoneyManager moneyManager;
        [SerializeField] private ShopManager shopManager;

        public override void InstallBindings()
        {
            Container.BindInstance(inventoryController).AsSingle();
            Container.BindInstance(toolbarManager).AsSingle();
            Container.BindInstance(gameViewManager).AsSingle();
            Container.BindInstance(hitObjectController).AsSingle();
            Container.BindInstance(pooledEmitter).AsSingle();
            Container.BindInstance(craftingSystem).AsSingle();
            Container.BindInstance(moneyManager).AsSingle();
            Container.BindInstance(shopManager).AsSingle();
        }
    }
}