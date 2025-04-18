using UnityEngine;

namespace BlueRacconGames.HitEffect.Factory
{
    public abstract class HitEffectFactorySO : ScriptableObject, IHitTargetEffectFactory
    {
        public abstract IHitTargetEffect CreateEffect();
    }
}