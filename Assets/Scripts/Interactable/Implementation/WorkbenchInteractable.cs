using BlueRacconGames.Crafting;
using Interactable;
using UnityEngine;
using Zenject;

namespace Interactable.Implementation
{
    public class WorkbenchInteractable : InteractableBase
    {
        private CraftingSystem craftingSystem;

        [Inject]
        private void Inject(CraftingSystem craftingSystem)
        {
            this.craftingSystem = craftingSystem;
        }

        public override bool Interact(InteractorControllerBase interactor)
        {
            craftingSystem.StartCraft();

            return true;
        }

        public override void LeaveInteract(InteractorControllerBase interactor)
        {
        }
    }
}