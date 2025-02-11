using System;
using UnityEngine;

namespace TimeTickSystems
{
    public class TimeTickSystem : MonoBehaviour
    {
        private const float TICK_TIMER_MAX = 0.2f;

        private static int tick;
        private static GameObject timeTickSystemGameObject;

        public static event EventHandler<OnTickEventArgs> OnTick;

        public static void Create()
        {
            if (timeTickSystemGameObject != null)
                return;

            timeTickSystemGameObject = new GameObject("TimeTickSystemObject");
            timeTickSystemGameObject.AddComponent<TimeTickSystemObject>();
        }

        public static int GetTick()
        {
            return tick;
        }

       private class TimeTickSystemObject : MonoBehaviour
        {
            private float tickTimer;

            private void Awake()
            {
                tick = 0;
            }

            void Update()
            {
                tickTimer += Time.deltaTime;

                if (tickTimer >= TICK_TIMER_MAX)
                {
                    tickTimer -= TICK_TIMER_MAX;
                    tick++;
                    if (OnTick != null) OnTick(this, new OnTickEventArgs { tick = tick });
                }
            }
        }
    }
}
