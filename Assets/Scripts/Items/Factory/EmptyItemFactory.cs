namespace Game.Item.Factory
{
    public class EmptyItemFactory : IItemFactory
    {
        public IItemRuntimeLogic CreateItem()
        {
            return null;
        }
    }
}