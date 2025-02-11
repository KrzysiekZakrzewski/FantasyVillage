using BlueRacconGames.InventorySystem;
using BlueRacconGames.MiniGame;
using BlueRacconGames.Pool;
using BlueRacconGames.UI.Bars;
using BlueRacconGames.View;
using Game.Managers;
using Game.Managers.Life;
using UnityEngine;
using Zenject;

namespace Game.Installer
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private GameHUD gameHUD;
        [SerializeField]
        private GemsManager gemsManager;
        [SerializeField]
        private LifeManager lifeManager;
        [SerializeField]
        private DefaultPooledParticleEmitter particleEmitter;
        [SerializeField]
        private HealthBar playerHealthBar;
        [SerializeField]
        private MiniGameController miniGameController;

        public override void InstallBindings()
        {
            Container.BindInstance(gameHUD).AsSingle();
            Container.BindInstance(gemsManager).AsSingle();
            Container.BindInstance(lifeManager).AsSingle();
            Container.BindInstance(particleEmitter).AsSingle();
            Container.BindInstance(playerHealthBar).AsSingle();
            Container.BindInstance(miniGameController).AsSingle();
        }
    }
}