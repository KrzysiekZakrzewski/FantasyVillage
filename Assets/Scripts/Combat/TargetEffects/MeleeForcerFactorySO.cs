using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(MeleeForcerFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(MeleeForcerFactorySO))]
    public class MeleeForcerFactorySO : MeleeWeaponTargetEffectFactorySO
    {
        [SerializeField]
        private Vector2 forceValue;
        public override IMeleeWeaponTargetEffect CreateEffect()
        {
            return new MeleeForcerEffect(forceValue);
        }
    }
}