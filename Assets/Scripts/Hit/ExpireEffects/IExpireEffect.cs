using System;

namespace BlueRacconGames.Hit.ExpireEffect
{
    public interface IExpireEffect
    {

        event Action<IExpireEffect> OnExecutedE;

        void Execute(DestructableHitTarget target);
    }
}