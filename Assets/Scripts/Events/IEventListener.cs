using System;

namespace BlueRacconGames.Events
{
    public interface IEventListener : IDisposable
    {
        void Register();
        void DeRegister();
        void OnEventReised();
    }
}