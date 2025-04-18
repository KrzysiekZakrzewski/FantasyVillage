using UnityEngine;

namespace BlueRacconGames.Hit.ExpireEffect.Factory
{
    public abstract class ExpireEffectFactorySO : ScriptableObject, IExpireEffectFactory
    {
        public abstract IExpireEffect CreateExpireEffect();
    }
}
