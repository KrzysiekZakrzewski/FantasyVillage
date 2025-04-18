using UnityEngine;

namespace BlueRacconGames.Events
{
    public class EventListenerBaseFactory : IEventListenerFactory<EventListenerBase>
    {
        [field: SerializeField] private DefaultGameEvent gameEvent;
        public EventListenerBase Create()
        {
            return new EventListenerBase(gameEvent);
        }
    }
}
