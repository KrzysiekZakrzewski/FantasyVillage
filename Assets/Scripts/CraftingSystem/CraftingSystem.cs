using BlueRacconGames.Crafting.Recipe;
using BlueRacconGames.InventorySystem;
using Game.Item;
using Game.Item.Factory;
using Game.Item.Factory.Implementation;
using Game.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewSystem;
using Zenject;

namespace BlueRacconGames.Crafting
{
    public class CraftingSystem : MonoBehaviour
    {
        [SerializeField] private InventoryUniqueId inventoryId;
        [SerializeField] private CraftingRecipeSO[] allRecipes;

        private readonly Dictionary<int, ResourceItemFactorySO> resourceItemLUT = new();
        private readonly Dictionary<int, List<CraftingRecipeSO>> recipesLUT = new();
        private CraftingRecipeSO currentRecipe;
        private List<int> knownRecipes;

        private InventoryController inventoryController;
        private GameViewManager gameViewManager;

        public event Action<bool, CraftingRecipeSO> OnRecipeUpdatedE;
        public event Action OnItemCraftedE;
        public event Action OnRecipeUnlockedE;

        public ItemFactorySO CraftedItem { private set; get; }

        [Inject]
        private void Inject(InventoryController inventoryController, GameViewManager gameViewManager)
        {
            this.inventoryController = inventoryController;
            this.gameViewManager = gameViewManager;
        }

        private void Awake()
        {
            SortRecipes();

            ClearTable();

            StartCoroutine(WaitForInventoryInitialize());

            knownRecipes = new();
        }

        private void OnDestroy()
        {
            var inventory = inventoryController.GetInventory(inventoryId);
            inventory.OnItemChangedE -= UpdateCraftStatus;
        }

        public void UpdateCraftStatus(int slotId, InventoryItem item)
        {
            if (item == null || item.IsNullOrEmpty())
            {
                RemoveItem(slotId);
                return;
            }

            if (item.ItemFactory is not ResourceItemFactorySO) return;

            var resourceItem = item.ItemFactory as ResourceItemFactorySO;

            AddItem(slotId, resourceItem);
        }

        public void ClearTable()
        {
            resourceItemLUT.Clear();
            currentRecipe = null;
            CraftedItem = null;
        }
         
        public void ResetTableAfterCrafted()
        {
            CraftedItem = null;

            TryCheckRecipe();
        }

        public void Craft(Action<ItemFactorySO> resultCallback)
        {
            if (currentRecipe == null) return;

            CraftedItem = currentRecipe.OutputItem;

            TryUnlockRecipe();

            resultCallback?.Invoke(CraftedItem);
            OnItemCraftedE?.Invoke();

            RemoveUsedItems(currentRecipe);
        }

        public void StartCraft()
        {
            gameViewManager.OpenInventory(inventoryId);
            gameViewManager.InventoryViewManager.Presentation.OnHidePresentationComplete += StopCraft;
        }

        public void StopCraft(IAmViewPresentation viewPresentation)
        {
            ClearTable();
            inventoryController.InventoryClear(inventoryId);
            gameViewManager.InventoryViewManager.Presentation.OnHidePresentationComplete -= StopCraft;
        }

        private void AddItem(int slotId, ResourceItemFactorySO resourceItem)
        {
            if (resourceItem == null) return;

            if (!resourceItemLUT.ContainsKey(slotId))
                resourceItemLUT.Add(slotId, resourceItem);

            TryCheckRecipe(resourceItem);
        }

        private void RemoveItem(int slotId)
        {
            Debug.Log("Czy wchodzê w Remove");

            if (!resourceItemLUT.ContainsKey(slotId)) return;

            resourceItemLUT.Remove(slotId);

            TryCheckRecipe(null);
        }

        private void RemoveUsedItems(CraftingRecipeSO recipe)
        {
            foreach (var item in recipe.NeedIngredients)
            {
                inventoryController.RemoveItem(inventoryId, item, 1);
            }
        }

        private void TryCheckRecipe(ResourceItemFactorySO item = null)
        {
            currentRecipe = null;

            if (!recipesLUT.TryGetValue(resourceItemLUT.Count, out var potentialRecipes)) return;

            foreach(CraftingRecipeSO recipe in potentialRecipes)
            {
                if(item != null)
                    if (!recipe.NeedIngredients.Contains(item)) continue;

                var result = DeepRecipeCheck(recipe);

                if (result)
                {
                    currentRecipe = recipe;
                    break;
                }
            }

            OnRecipeUpdatedE?.Invoke(IsRecipeKnown(currentRecipe),  currentRecipe);
        }

        private bool DeepRecipeCheck(CraftingRecipeSO recipe)
        {
            foreach(ResourceItemFactorySO item in resourceItemLUT.Values)
            {
                if (!recipe.NeedIngredients.Contains(item)) return false;
            }

            return true;
        }

        private void TryUnlockRecipe()
        {
            if (IsRecipeKnown(currentRecipe)) return;

            knownRecipes.Add(currentRecipe.Id);

            OnRecipeUnlockedE?.Invoke();
        }

        private bool IsRecipeKnown(CraftingRecipeSO recipe)
        {
            return recipe != null && knownRecipes.Contains(recipe.Id);
        }

        private void SortRecipes()
        {
            foreach(CraftingRecipeSO recipeSO in allRecipes)
            {
                int ingredientCount = recipeSO.NeedIngredients.Count;

                if (!recipesLUT.TryGetValue(ingredientCount, out var recipes))
                {
                    recipesLUT.Add(ingredientCount, new() { recipeSO });
                    continue;
                }

                recipes.Add(recipeSO);
            }

            allRecipes = null;
        }

        private IEnumerator WaitForInventoryInitialize()
        {
            yield return new WaitUntil(inventoryController.IsInitialized);

            var inventory = inventoryController.GetInventory(inventoryId);

            inventory.OnItemChangedE += UpdateCraftStatus;
        }
    }
}