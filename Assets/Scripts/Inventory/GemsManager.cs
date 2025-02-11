using BlueRacconGames.Pool;
using BlueRacconGames.UI.HUD;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.InventorySystem
{
    public class GemsManager : MonoBehaviour
    {
        [SerializeField]
        private GemsHUD hud;
        [SerializeField]
        private ParticlePoolItem pickUpParticle;

        private DefaultPooledParticleEmitter particleEmitter;

        public int GemsAmount {  get; private set; }

        [Inject]
        private void Inject(DefaultPooledParticleEmitter particleEmitter)
        {
            this.particleEmitter = particleEmitter;
        }

        private void Awake()
        {
            hud.RefreshGemsValue(0);//TO DO change to load
        }

        public bool AddGems(int amount, Vector2 position)
        {
            GemsAmount += amount;

            hud.RefreshGemsValue(GemsAmount);

            particleEmitter.EmitItem(pickUpParticle, position);

            return true;
        }

        public bool RemoveGems(int amount)
        {
            if(GemsAmount < amount)
                return false;

            GemsAmount -= amount;

            hud.RefreshGemsValue(GemsAmount);

            return true;
        }
    }
}