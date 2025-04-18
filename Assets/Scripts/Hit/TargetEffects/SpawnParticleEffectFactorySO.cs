using BlueRacconGames.Extensions;
using BlueRacconGames.Pool;
using UnityEngine;

namespace BlueRacconGames.HitEffect.Factory
{
    [CreateAssetMenu(fileName = nameof(SpawnParticleEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(Factory) + "/" + nameof(SpawnParticleEffectFactorySO))]
    public class SpawnParticleEffectFactorySO : HitEffectFactorySO
    {
        [field: SerializeField] public ParticlePoolItem VfxEffect { get; private set; }
        [field: SerializeField] public TransformDataContainer TransformDataContainer { get; private set; }
        public override IHitTargetEffect CreateEffect()
        {
            return new SpawnParticleEffect(this);
        }
    }
}