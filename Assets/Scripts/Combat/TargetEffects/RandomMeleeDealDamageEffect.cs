using Damageable;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class RandomMeleeDealDamageEffect : IMeleeWeaponTargetEffect
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

        public void Execute(MeleeCombatControllerBase source, IDamagableTarget target)
        {
            IDamageable damageable = target.GameObject.GetComponent<IDamageable>();

            damageable?.TakeDamage(GetRandomDamageValue());
        }
    }
}
