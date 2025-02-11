using BlueRacconGames.Inventory;
using UnityEngine;
using Zenject;

namespace Game.Installer
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField]
        private InventoryManager inventory;

        public override void InstallBindings()
        {
            Container.BindInstance(inventory).AsSingle();
        }
    }
}