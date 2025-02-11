using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(MeleeDizzyEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(MeleeDizzyEffectFactorySO))]
    public class MeleeDizzyEffectFactorySO : MeleeWeaponTargetEffectFactorySO
    {
        [field: SerializeField] public float DizzyTime;
        public override IMeleeWeaponTargetEffect CreateEffect()
        {
            return new MeleeDizzyEffect(this);
        }
    }
}
