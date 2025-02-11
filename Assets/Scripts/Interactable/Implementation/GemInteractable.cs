using BlueRacconGames.InventorySystem;
using Interactable.Implementation.Data;
using UnityEngine;
using Zenject;

namespace Interactable.Implementation
{
    public class GemInteractable : InteractableBase
    {
        [SerializeField]
        private GemDataSO initializeData;

        private GemsManager gemsManager;

        [Inject]
        private void Inject(GemsManager gemsManager)
        {
            this.gemsManager = gemsManager;
        }

        public override bool Interact(InteractorControllerBase interactor)
        {
            if (!IsInteractable)
                return false;

            SwitchInteractable(false);

            Destroy(gameObject, 0.1f);

            return gemsManager.AddGems(initializeData.GemsAmount, transform.position);
        }

        public override void LeaveInteract(InteractorControllerBase interactor)
        {
            
        }

        private void SpawnPickUpParticle()
        {

        }
    }
}
