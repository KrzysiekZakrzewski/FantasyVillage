using UnityEngine;

namespace BlueRacconGames.HitEffect.Factory
{
    [CreateAssetMenu(fileName = nameof(RandomMeleeDealDamageFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(RandomMeleeDealDamageFactorySO))]
    public class RandomMeleeDealDamageFactorySO : MeleeWeaponTargetEffectFactorySO
    {
        [field: SerializeField] public int MinDamageValue;
        [field: SerializeField] public int MaxDamageValue;

        public override IHitTargetEffect CreateEffect()
        {
            return new RandomMeleeDealDamageEffect(this);
        }
    }
}
