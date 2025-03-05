using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class MeleeHitVFXEffect : IMeleeWeaponTargetEffect
    {
        private ParticleSystem vfxEffect;

        public MeleeHitVFXEffect(ParticleSystem vfxEffect)
        {
            this.vfxEffect = vfxEffect;
        }

        public void Execute(MeleeCombatControllerBase source, IDamagableTarget target)
        {
            source.SpawnHitEffect(vfxEffect);
        }
    }
}