using BlueRacconGames.Extensions;
using BlueRacconGames.Hit.ExpireEffect.Factory;
using BlueRacconGames.Pool;
using Zenject;

namespace BlueRacconGames.Hit.ExpireEffect
{
    class SpawnExpireEffect : ExpireEffectBase
    {
        private readonly PoolItemBase prefab;
        private DefaultPooledEmitter objectPooledEmitter;
        private TransformData nodeTransformData;

        [Inject]
        private void Inject(DefaultPooledEmitter objectPooledEmitter)
        {
            this.objectPooledEmitter = objectPooledEmitter;
        }

        public SpawnExpireEffect(SpawnExpireEffectFactorySO initialData)
        {
            prefab = initialData.Prefab;
        }

        public override void Execute(DestructableHitTarget target)
        {
            objectPooledEmitter.EmitItem<PoolItemBase>(prefab, nodeTransformData.Position);

            OnExecuted();
        }
    }
}
