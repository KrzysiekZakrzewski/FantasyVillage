using BlueRacconGames.Inventory;
using BlueRacconGames.Inventory.UI;
using UnityEngine;
using Zenject;

namespace Game.Installer
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField]
        private InventoryManager inventory;
        [SerializeField]
        private InventoryUI inventoryUI;

        public override void InstallBindings()
        {
            Container.BindInstance(inventory).AsSingle();
            Container.BindInstance(inventoryUI).AsSingle();
        }
    }
}