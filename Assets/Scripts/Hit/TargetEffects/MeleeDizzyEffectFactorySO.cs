using UnityEngine;

namespace BlueRacconGames.HitEffect.Factory
{
    [CreateAssetMenu(fileName = nameof(MeleeDizzyEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(MeleeDizzyEffectFactorySO))]
    public class MeleeDizzyEffectFactorySO : MeleeWeaponTargetEffectFactorySO
    {
        [field: SerializeField] public float DizzyTime;

        public override IHitTargetEffect CreateEffect()
        {
            return new MeleeDizzyEffect(this);
        }
    }
}
