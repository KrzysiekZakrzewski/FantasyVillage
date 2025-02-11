using UnityEngine;

namespace Game.Item.Factory
{
    public class EquipmentFactorySO : ItemFactorySO
    {
        [field: SerializeField]
        public EquipmentSlot EquipmentSlot { get; private set; }

        public override IItemRuntimeLogic CreateItem()
        {
            throw new System.NotImplementedException();
        }
    }
    public enum EquipmentSlot
    {
        Head,
        Chest,
        Legs,
        Weapon,
        Shield,
        Feet
    }
}