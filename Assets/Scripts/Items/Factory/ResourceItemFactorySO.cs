using Game.Item.Factory.Implementation;
using Mono.Cecil;
using UnityEngine;

namespace Game.Item.Factory.Implementation
{
    [CreateAssetMenu(fileName = nameof(ResourceItemFactorySO), menuName = nameof(Item) + "/" + nameof(Implementation) + "/" + nameof(ResourceItemFactorySO))]

    public class ResourceItemFactorySO : ItemFactorySO
    {
        public override IItemRuntimeLogic CreateItem()
        {
            return new ResourceItem(this);
        }
    }
}
