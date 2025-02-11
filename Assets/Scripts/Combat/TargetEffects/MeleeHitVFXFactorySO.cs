using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(MeleeHitVFXFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(MeleeHitVFXFactorySO))]
    public class MeleeHitVFXFactorySO : MeleeWeaponTargetEffectFactorySO
    {
        [SerializeField]
        private ParticleSystem vfxEffect;

        public override IMeleeWeaponTargetEffect CreateEffect()
        {
            return new MeleeHitVFXEffect(vfxEffect);
        }
    }
}