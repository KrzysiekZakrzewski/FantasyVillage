namespace Saves
{
    public interface ISaveListener
    {
        void Register();
        void Unregister();

        void OnGameSaveCalled();

        void OnGameSaveCompleted();
    }
}