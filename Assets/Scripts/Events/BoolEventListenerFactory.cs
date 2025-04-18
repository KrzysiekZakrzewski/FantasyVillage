using Assets.Scripts.Events;
using UnityEngine;

namespace BlueRacconGames.Events
{
    public class BoolEventListenerFactory : IEventListenerFactory<BoolEventListener>
    {
        [field: SerializeField] private BoolGameEvent gameEvent;
        public BoolEventListener Create()
        {
            return new BoolEventListener(gameEvent);
        }
    }
}
