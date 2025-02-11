using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public abstract class MeleeWeaponTargetEffectFactorySO : ScriptableObject, IMeleeWeaponTargetEffectFactory
    {
        public abstract IMeleeWeaponTargetEffect CreateEffect();
    }
}