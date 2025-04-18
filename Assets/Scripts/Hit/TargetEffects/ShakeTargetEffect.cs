using BlueRacconGames.Hit;
using BlueRacconGames.HitEffect.Factory;
using DG.Tweening;

namespace BlueRacconGames.HitEffect
{
    public class ShakeTargetEffect : IHitTargetEffect
    {
        private readonly float duration;
        private readonly float strenght;

        public ShakeTargetEffect(ShakeTargetEffectFactorySO initialData)
        {
            duration = initialData.Duration;
            strenght = initialData.Strenght;
        }

        public void Execute(HitObjectControllerBase source, IHitTarget target)
        {
            target.GameObject.transform.DOShakePosition(duration, strenght);
        }
    }
}
