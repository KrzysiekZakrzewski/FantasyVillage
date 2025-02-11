using Damageable.Implementation;
using Interactable.Implementation.Data;
using UnityEngine;

namespace Interactable.Implementation
{
    public class HealInteractable : InteractableBase
    {
        [SerializeField]
        private HealDataSO initializeData;

        public override bool Interact(InteractorControllerBase interactor)
        {
            if (!interactor.TryGetComponent<PlayerDamagable>(out var playerDamagable))
                return false;

            playerDamagable.Heal(initializeData.HealValue);

            return true;
        }

        public override void LeaveInteract(InteractorControllerBase interactor)
        {

        }
    }
}
