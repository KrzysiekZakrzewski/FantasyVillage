using UnityEngine;

namespace Game.Item.Factory.Implementation
{
    [CreateAssetMenu(fileName = nameof(AxeItemFactorySO), menuName = nameof(Item) + "/" + nameof(Implementation) + "/" + nameof(AxeItemFactorySO))]
    public class AxeItemFactorySO : ToolBaseFactorySO
    {
        public override IItemRuntimeLogic CreateItem()
        {
            return new BaseAxeUsableItem(this);
        }
    }
}