using RDG.Platforms;
using System.Collections;
using UnityEngine;

namespace BlueRacconGames.Hit.ExpireEffect
{
    public class AnimationExpireEffect : ExpireEffectBase
    {
        private readonly int animationClipHash;
        private readonly float transitionDuration;
        public AnimationExpireEffect(AnimationExpireEffectFactorySO initialData)
        {
            animationClipHash = initialData.AnimationClipHash;
            transitionDuration = initialData.TransitionDuration;
        }

        public override void Execute(DestructableHitTarget target)
        {
            var animator = target.Animator;

            animator.CrossFade(animationClipHash, transitionDuration);

            var clipsInfo = animator.GetCurrentAnimatorClipInfo(0);

            var clipDuration = clipsInfo[0].clip.length;

            CorutineSystem.StartSequnce(WaitForEndAnimation(clipDuration));
        }

        private IEnumerator WaitForEndAnimation(float clipDuration)
        {
            yield return null;

            yield return new WaitForSeconds(clipDuration);

            OnExecuted();
        }
    }
}
