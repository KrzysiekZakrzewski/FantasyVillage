using UnityEngine;

namespace Game.Item.Factory
{
    public abstract class ItemFactorySO : ScriptableObject, IItemFactory
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public bool IsDefault { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int BaseBuyCost { get; private set; }
        [field: SerializeField] public int BaseSellCost { get; private set; }
        [field: SerializeField] public int MaxStackAmount { get; private set; }
        public abstract IItemRuntimeLogic CreateItem();

        public void ImportFromCSV(int id, bool isDefault, string name, 
            string description, Sprite icon, int baseBuyCost, int baseSellCost,int maxStackAmount)
        {
            Id = id;
            IsDefault = isDefault;
            Name = name;
            Description = description;
            Icon = icon;
            BaseBuyCost = baseBuyCost;
            BaseSellCost = baseSellCost;
            MaxStackAmount = maxStackAmount;
        }
    }

    public class EmptyItemFactorySO : ItemFactorySO
    {
        public override IItemRuntimeLogic CreateItem()
        {
            return null;
        }
    }
}
