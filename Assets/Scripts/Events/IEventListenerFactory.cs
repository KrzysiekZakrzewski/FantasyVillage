using Zenject;

namespace BlueRacconGames.Events
{
    public interface IEventListenerFactory
    {

    }

    public interface IEventListenerFactory<out TValue> : IEventListenerFactory where TValue : IEventListener
    {
        TValue Create();
    }
    public interface IEventListenerFactory<in Param1, out TValue> : IEventListenerFactory where TValue : IEventListener
    {
        TValue Create(Param1 param1);
    }
}