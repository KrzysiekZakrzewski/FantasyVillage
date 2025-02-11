namespace Saves
{
    public interface ILoadListener
    {
        void Register();
        void Unregister();

        void OnGameLoadCalled();

        void OnGameLoadCompleted();
    }
}
