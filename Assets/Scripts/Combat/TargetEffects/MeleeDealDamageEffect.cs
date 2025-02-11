using Damageable;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class MeleeDealDamageEffect : IMeleeWeaponTargetEffect
    {
        private int damageValue;

        public MeleeDealDamageEffect(int damageValue)
        {
            this.damageValue = damageValue;
        }

        public void Execute(MeleeCombatControllerBase source, IWeaponTarget target)
        {
            IDamageable damageable = target.GameObject.GetComponent<IDamageable>();

            damageable?.TakeDamage(damageValue);
        }
    }
}