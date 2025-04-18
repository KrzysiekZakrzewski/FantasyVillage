using BlueRacconGames.HitEffect;
using System;
using System.Collections.Generic;

namespace BlueRacconGames.Hit
{
    public interface IMeleeWeapon
    {
        List<IHitTargetEffect> MeleeWeaponTargetHitEffects { get; }
        event Action<IHitTarget> OnHitE;
        void ResetWeapon();
        void OnHit(HitObjectControllerBase source, IHitTarget target);
    }
}