using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public interface IMeleeWeapon
    {
        List<IMeleeWeaponTargetEffect> MeleeWeaponTargetHitEffects { get; }
        event Action<IDamagableTarget> OnHitE;
        void ResetWeapon();
        void OnHit(MeleeCombatControllerBase source, IDamagableTarget target);
    }
}