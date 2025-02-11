using Game.Item.Factory;
using UnityEngine;

namespace Game.Item
{
    public abstract class ItemBase : IItemRuntimeLogic
    {
        protected int id;
        protected string name;
        protected string description;
        protected Sprite icon;

        public int Id => id;
        public string Name => name;
        public string Description => description;
        public Sprite Icon => icon;

        protected ItemBase(ItemFactorySO initialData)
        {
            id = initialData.Id;
            name = initialData.Name;
            description = initialData.Description;
            icon = initialData.Icon;
        }

        public bool CanUse()
        {
            throw new System.NotImplementedException();
        }

        public bool Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
