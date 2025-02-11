using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(MeleeDealDamageFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(MeleeDealDamageFactorySO))]

    public class MeleeDealDamageFactorySO : MeleeWeaponTargetEffectFactorySO
    {
        [SerializeField]
        private int damageValue;
        public override IMeleeWeaponTargetEffect CreateEffect()
        {
            return new MeleeDealDamageEffect(damageValue); 
        }
    }
}