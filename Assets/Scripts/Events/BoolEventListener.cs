using UnityEngine;
using System;
using Assets.Scripts.Events;

namespace BlueRacconGames.Events
{
    public class BoolEventListener : IEventListener
    {
        private readonly GameEventBase gameEvent;
        private readonly bool value;

        public event Action<bool> ResponseE;

        public BoolEventListener(BoolGameEvent gameEvent)
        {
            this.gameEvent = gameEvent;
            this.value = gameEvent.Value;
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
            ResponseE.Invoke(value);
        }

        public virtual void Dispose()
        {
            DeRegister();
        }
    }
}
