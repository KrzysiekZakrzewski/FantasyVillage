using System;
using TimeTickSystems;

namespace BlueRacconGames.AI.Implementation
{
    public class WonderAIMode
    {
        private int tick = 0;
        private int wonderTick = 10;

        private event Action onStartWonderE;
        private event Action onEndWonderE;

        public bool isWondering = false;

        private void OnTick(object sender, OnTickEventArgs e)
        {
            tick++;

            if (tick < wonderTick) return;

            EndWonder();
        }

        private void StartWonder()
        {
            isWondering = true;

            onStartWonderE?.Invoke();
        }

        private void EndWonder()
        {
            onEndWonderE?.Invoke();

            TimeTickSystem.OnTick -= OnTick;

            tick = 0;

            isWondering = false;
        }

        public void UnsubscribeEvent(Action eventFromUnsub, Action eventToUnsub)
        {
            eventFromUnsub -= eventToUnsub;
            eventFromUnsub -= () => UnsubscribeEvent(eventFromUnsub, eventToUnsub);
        }

        public void SetupTimeTickSystem(Action onStartWonderE, Action onEndWonderE)
        {
            this.onStartWonderE += onStartWonderE;
            this.onStartWonderE += () => UnsubscribeEvent(this.onStartWonderE, onStartWonderE);

            this.onEndWonderE += onEndWonderE;
            this.onEndWonderE += () => UnsubscribeEvent(this.onEndWonderE, onEndWonderE);

            TimeTickSystem.OnTick += OnTick;

            StartWonder();
        }
    }
}
