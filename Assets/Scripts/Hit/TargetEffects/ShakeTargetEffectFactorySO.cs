using System;
using UnityEngine;

namespace BlueRacconGames.HitEffect.Factory
{
    [CreateAssetMenu(fileName = nameof(ShakeTargetEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(Factory) + "/" + nameof(ShakeTargetEffectFactorySO))]

    public class ShakeTargetEffectFactorySO : HitEffectFactorySO
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Strenght { get; private set; }
        public override IHitTargetEffect CreateEffect()
        {
            return new ShakeTargetEffect(this);
        }
    }
}
