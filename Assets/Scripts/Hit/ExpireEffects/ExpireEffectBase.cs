using System;

namespace BlueRacconGames.Hit.ExpireEffect
{
    public abstract class ExpireEffectBase : IExpireEffect
    {
        protected bool executed;

        public event Action<IExpireEffect> OnExecutedE;

        public abstract void Execute(DestructableHitTarget target);
        protected void OnExecuted()
        {
            OnExecutedE?.Invoke(this);
        }
    }
}