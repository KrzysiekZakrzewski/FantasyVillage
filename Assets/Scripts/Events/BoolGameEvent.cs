using BlueRacconGames.Events;
using UnityEngine;

namespace Assets.Scripts.Events
{
    [CreateAssetMenu(fileName = nameof(BoolGameEvent), menuName = nameof(Events) + "/" + nameof(BoolGameEvent))]
    public class BoolGameEvent : DefaultGameEvent
    {
        [field: SerializeField] public bool Value { get; private set; }

        public void Register(BoolEventListener listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void DeRegister(BoolEventListener listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}
