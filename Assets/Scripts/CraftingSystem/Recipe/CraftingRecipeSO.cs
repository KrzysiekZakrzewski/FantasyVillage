using Game.Item.Factory;
using Game.Item.Factory.Implementation;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.Crafting.Recipe
{
    [CreateAssetMenu(fileName = nameof(CraftingRecipeSO), menuName = nameof(Crafting) + "/" + nameof(Recipe) + "/" + nameof(CraftingRecipeSO))]
    public class CraftingRecipeSO : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public List<ResourceItemFactorySO> NeedIngredients { get; private set; }
        [field: SerializeField] public ItemFactorySO OutputItem { get; private set; }
        [field: SerializeField] public bool IsDefault { get; private set; }
    }
}