using Zenject;

namespace BlueRacconGames.HitEffect.Factory
{
    public interface IHitTargetEffectFactory
    {
        IHitTargetEffect CreateEffect();
    }
}