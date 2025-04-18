using BlueRacconGames.Crafting;
using BlueRacconGames.Crafting.Recipe;
using Zenject;
using UnityEngine;

namespace BlueRacconGames.InventorySystem
{
    public class CraftingView : InventoryUIViewBase
    {
        [SerializeField] private OutputCraftSlot outputSlot;
        public override bool IsPopup => true;

        private CraftingSystem craftingSystem;

        [Inject]
        private void Inject(CraftingSystem craftingSystem)
        {
            this.craftingSystem = craftingSystem;
        }

        protected override void Awake()
        {
            base.Awake();

            craftingSystem.OnRecipeUpdatedE += OnRecipeUpdated;
            outputSlot.Init();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            craftingSystem.OnRecipeUpdatedE -= OnRecipeUpdated;
        }

        private void OnRecipeUpdated(bool recipeKnown, CraftingRecipeSO recipe)
        {
            var outputItem = recipe != null ? recipe.OutputItem : null;

            outputSlot.UpdateSlot(recipeKnown, outputItem);
        }
    }
}