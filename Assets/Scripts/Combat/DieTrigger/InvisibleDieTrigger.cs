using Game.Managers.Life;
using System.Collections;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class InvisibleDieTrigger : BaseDieTrigger
    {
        private readonly float dieTriggerDuration = 2f;
        private bool timerStarted;

        private Coroutine coroutine;

        private void OnBecameVisible()
        {
            ResetTrigger();
        }

        private void OnBecameInvisible()
        {
            if(timerStarted || isTriggered) return;

            coroutine = StartCoroutine(DieTimer());
        }

        protected override void InternalReset()
        {
            base.InternalReset();

            if(coroutine  != null)
                StopCoroutine(coroutine);

            timerStarted = false;
        }

        private IEnumerator DieTimer()
        {
            timerStarted = true;

            yield return new WaitForSeconds(dieTriggerDuration);

            Trigger();
        }
    }
}
