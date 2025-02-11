using Game.Managers.Life;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.MeleeCombat
{
    public abstract class BaseDieTrigger : MonoBehaviour, IDieTrigger
    {
        protected bool isTriggered;
        public bool IsTriggered => isTriggered;

        protected LifeManager lifeManager;

        [Inject]
        private void Inject(LifeManager lifeManager)
        {
            this.lifeManager = lifeManager;
        }

        public void ResetTrigger()
        {
            isTriggered = false;

            InternalReset();
        }

        public void Trigger()
        {
            if(IsTriggered) return;

            isTriggered = true;

            InternalTrigger();
        }

        protected virtual void InternalReset()
        {

        }

        protected virtual void InternalTrigger()
        {
            lifeManager.OnDie();
            Debug.Log($"Triggered: {gameObject.name}");
        }
    }
}
