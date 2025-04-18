using UnityEngine;

namespace BlueRacconGames.HitEffect.Factory
{
    [CreateAssetMenu(fileName = nameof(MeleeDealDamageFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(MeleeDealDamageFactorySO))]

    public class MeleeDealDamageFactorySO : MeleeWeaponTargetEffectFactorySO
    {
        [SerializeField]
        private int damageValue;

        public override IHitTargetEffect CreateEffect()
        {
            return new MeleeDealDamageEffect(damageValue); 
        }
    }
}