using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.Events
{
    [CreateAssetMenu(fileName = nameof(DefaultGameEvent), menuName = nameof(Events) + "/" + nameof(DefaultGameEvent))]

    public class DefaultGameEvent : GameEventBase
    {
        public override void Reise()
        {
            for (int i = 0; i < eventListeners.Count; i++)
            {
                eventListeners[i].OnEventReised();
            }
        }

        public override void Register(IEventListener listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public override void DeRegister(IEventListener listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }

    public interface IGameEvent
    {
        void Reise();
        void Register(IEventListener listener);
        void DeRegister(IEventListener listener);
    }
    public abstract class GameEventBase : ScriptableObject, IGameEvent
    {
        protected readonly List<IEventListener> eventListeners = new();

        public abstract void Reise();

        public abstract void Register(IEventListener listener);

        public abstract void DeRegister(IEventListener listener);
    }
}