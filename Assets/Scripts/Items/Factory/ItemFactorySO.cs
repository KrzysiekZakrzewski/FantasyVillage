using UnityEngine;

namespace Game.Item.Factory
{
    public abstract class ItemFactorySO : ScriptableObject, IItemFactory
    {
        [field: SerializeField]
        public int Id { get; private set; }
        [field: SerializeField]
        public bool IsDefaultItem { get; private set; }
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public string Description { get; private set; }
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        public abstract IItemRuntimeLogic CreateItem();
    }
}
