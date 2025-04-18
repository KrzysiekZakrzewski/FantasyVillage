using BlueRacconGames.Pool;
using BlueRacconGames.UI.HUD;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.InventorySystem
{
    public class GemsManager : MonoBehaviour
    {
        [SerializeField]
        private MoneyHUD hud;
        [SerializeField]
        private ParticlePoolItem pickUpParticle;

        private DefaultPooledEmitter particleEmitter;

        public int GemsAmount {  get; private set; }

        [Inject]
        private void Inject(DefaultPooledEmitter particleEmitter)
        {
            this.particleEmitter = particleEmitter;
        }

        private void Awake()
        {
            hud.UpdateMoneyAmount(0);//TO DO change to load
        }

        public bool AddGems(int amount, Vector2 position)
        {
            GemsAmount += amount;

            hud.UpdateMoneyAmount(GemsAmount);

            particleEmitter.EmitItem<ParticleSystem>(pickUpParticle, position);

            return true;
        }

        public bool RemoveGems(int amount)
        {
            if(GemsAmount < amount)
                return false;

            GemsAmount -= amount;

            hud.UpdateMoneyAmount(GemsAmount);

            return true;
        }
    }
}