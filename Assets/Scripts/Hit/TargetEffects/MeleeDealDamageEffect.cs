using BlueRacconGames.Hit;
using Damageable;

namespace BlueRacconGames.HitEffect
{
    public class MeleeDealDamageEffect : IHitTargetEffect
    {
        private int damageValue;

        public MeleeDealDamageEffect(int damageValue)
        {
            this.damageValue = damageValue;
        }

        public void Execute(HitObjectControllerBase source, IHitTarget target)
        {
            IDamageable damageable = target.GameObject.GetComponent<IDamageable>();

            damageable?.TakeDamage(damageValue);
        }
    }
}