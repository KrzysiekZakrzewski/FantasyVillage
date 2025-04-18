using BlueRacconGames.Extensions;
using BlueRacconGames.Hit;
using BlueRacconGames.HitEffect.Factory;
using BlueRacconGames.Pool;
using Zenject;

namespace BlueRacconGames.HitEffect
{
    public class SpawnParticleEffect : IHitTargetEffect
    {
        private readonly ParticlePoolItem vfxEffect;
        private readonly TransformData transformData;
        private DefaultPooledEmitter objectPooledEmitter;

        [Inject]
        private void Inject(DefaultPooledEmitter objectPooledEmitter)
        {
            this.objectPooledEmitter = objectPooledEmitter;
        }

        public SpawnParticleEffect(SpawnParticleEffectFactorySO initialData)
        {
            vfxEffect = initialData.VfxEffect;
            transformData = initialData.TransformDataContainer.NodeTransformData;
        }

        public void Execute(HitObjectControllerBase source, IHitTarget target)
        {
            objectPooledEmitter.EmitItem<ParticlePoolItem>(vfxEffect, transformData.Position);
        }
    }
}