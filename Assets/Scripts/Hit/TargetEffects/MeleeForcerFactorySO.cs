using UnityEngine;

namespace BlueRacconGames.HitEffect.Factory
{
    [CreateAssetMenu(fileName = nameof(MeleeForcerFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(MeleeForcerFactorySO))]
    public class MeleeForcerFactorySO : MeleeWeaponTargetEffectFactorySO
    {
        [SerializeField]
        private Vector2 forceValue;

        public override IHitTargetEffect CreateEffect()
        {
            return new MeleeForcerEffect(forceValue);
        }
    }
}