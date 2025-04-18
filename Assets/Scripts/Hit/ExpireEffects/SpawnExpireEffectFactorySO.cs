using BlueRacconGames.Extensions;
using BlueRacconGames.Pool;
using UnityEngine;

namespace BlueRacconGames.Hit.ExpireEffect.Factory
{
    [CreateAssetMenu(fileName = nameof(SpawnExpireEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(ExpireEffect) + "/" + nameof(SpawnExpireEffectFactorySO))]

    public class SpawnExpireEffectFactorySO : ExpireEffectFactorySO
    {
        [field: SerializeField] public PoolItemBase Prefab { get; private set; }
        [field: SerializeField] public TransformDataContainer TransformDataContainer { get; private set; }
        public override IExpireEffect CreateExpireEffect()
        {
            return new SpawnExpireEffect(this);
        }
    }
}