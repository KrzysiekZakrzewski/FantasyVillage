using System;
namespace BlueRacconGames.Events
{
    public class EventListenerBase : IEventListener
    {
        public DefaultGameEvent gameEvent;
        public event Action ResponseE;

        public EventListenerBase(DefaultGameEvent gameEvent)
        {
            this.gameEvent = gameEvent;
            Register();
        }

        public void Register()
        {
            gameEvent.Register(this);
        }

        public void DeRegister()
        {
            gameEvent.DeRegister(this);
        }

        public virtual void OnEventReised()
        {
            ResponseE.Invoke();
        }

        public virtual void Dispose()
        {
            DeRegister();
        }
    }
}