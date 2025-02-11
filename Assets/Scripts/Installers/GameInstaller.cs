using Audio.Manager;
using BlueRacconGames.InventorySystem;
using BlueRacconGames.Pool;
using Game.Managers.Life;
using Game.Managers;
using Game.SceneLoader;
using Settings;
using UnityEngine;
using Zenject;

namespace Game.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private SceneLoadManagers sceneLoadManagers;
        [SerializeField]
        private AudioManager audioManager;
        [SerializeField]
        private SettingsManager settingsManager;

        public override void InstallBindings()
        {
            Container.BindInstance(sceneLoadManagers).AsSingle();
            Container.BindInstance(audioManager).AsSingle();
            Container.BindInstance(settingsManager).AsSingle();
        }
    }
}