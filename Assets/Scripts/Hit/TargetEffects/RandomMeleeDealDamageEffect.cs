using BlueRacconGames.Hit;
using BlueRacconGames.HitEffect.Factory;
using Damageable;
using UnityEngine;

namespace BlueRacconGames.HitEffect
{
    public class RandomMeleeDealDamageEffect : IHitTargetEffect
    {
        private int minDamageValue;
        private int maxDamageValue;

        public RandomMeleeDealDamageEffect(RandomMeleeDealDamageFactorySO initialData)
        {
            this.minDamageValue = initialData.MinDamageValue;
            this.maxDamageValue = initialData.MaxDamageValue;
        }

        private int GetRandomDamageValue()
        {
            return Random.Range(minDamageValue, maxDamageValue);
        }

        public void Execute(HitObjectControllerBase source, IHitTarget target)
        {
            IDamageable damageable = target.GameObject.GetComponent<IDamageable>();

            damageable?.TakeDamage(GetRandomDamageValue());
        }
    }
}
