using BlueRacconGames.Hit.ExpireEffect.Factory;
using UnityEngine;

namespace BlueRacconGames.Hit.ExpireEffect
{
    [CreateAssetMenu(fileName = nameof(AnimationExpireEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(ExpireEffect) + "/" + nameof(AnimationExpireEffectFactorySO))]
    public class AnimationExpireEffectFactorySO : ExpireEffectFactorySO
    {
        [field: SerializeField] public float TransitionDuration { get; private set; }
        [SerializeField] private string animationClipName;

        public int AnimationClipHash => Animator.StringToHash(animationClipName);

        public override IExpireEffect CreateExpireEffect()
        {
            return new AnimationExpireEffect(this);
        }
    }
}