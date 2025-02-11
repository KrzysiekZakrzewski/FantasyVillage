using UnityEngine;

namespace Game.Item.Factory
{
    [CreateAssetMenu(fileName = nameof(TestItemSO), menuName = nameof(Item) + "/" + nameof(Factory) + "/" + nameof(TestItemSO))]
    public class TestItemSO : ItemFactorySO
    {
        public override IItemRuntimeLogic CreateItem()
        {
            return default(IItemRuntimeLogic);
        }
    }
}
