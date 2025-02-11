namespace Game.Item.Factory
{
    public interface IItemFactory
    {
        IItemRuntimeLogic CreateItem();
    }
}
