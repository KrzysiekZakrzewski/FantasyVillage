using UnityEngine;

namespace BlueRacconGames.Animation
{
    public abstract class UnitAnimationControllerBase : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float transitionDuration = 0.1f;

        private bool hasAnimator;

        private int animIDBase;
        private int animIDHorizontal;
        private int animIDHorizontalDirection;
        private int animIDVertical;
        private int animIDVerticalDirection;
        private int animIDSpeed;
        private int animIDGetHit;
        private int animIDDizzy;
        private int animIDDie;
        private int animIDAttack;

        private int toolLayer = 2;
        private int meleeCombatLayer = 1;
        private int baseAnimationLayer = 0;

        private void Awake()
        {
            hasAnimator = animator != null;

            AssignAnimationIDs();
        }

        public void WalkaalbleAnimation(float horizontal, float vertical, float speed)
        {
            if(!hasAnimator) return;

            animator.SetFloat(animIDHorizontal, horizontal);
            animator.SetFloat(animIDVertical, vertical);
            animator.SetFloat(animIDSpeed, speed);
        }

        public void IdleAnimation(float horizontal, float vertical)
        {
            if(!hasAnimator) return;

            animator.SetFloat(animIDHorizontalDirection, horizontal);
            animator.SetFloat(animIDVerticalDirection, vertical);
        }

        public void BackToBaseState()
        {
            if (!hasAnimator) return;

            animator.Play(animIDBase, (int)AnimationLayer.Melee);
        }

        public void GetHitAnimation()
        {
            if (!hasAnimator) return;

            animator.Play(animIDGetHit, meleeCombatLayer);
        }

        public void AttackAnimation()
        {
            if (!hasAnimator) return;

            animator.Play(animIDAttack, meleeCombatLayer);
        }

        public void ToolAnimationByName(string name)
        {
            if (!hasAnimator) return;

            var animId = Animator.StringToHash(name);

            animator.Play(animId, toolLayer);
        }

        public bool GetBoolParameter(string name)
        {
            return animator.GetBool(name);
        }

        protected virtual void AssignAnimationIDs()
        {
            if (!hasAnimator) return;

            animIDBase = Animator.StringToHash("EmptyCombat");
            animIDSpeed = Animator.StringToHash("Speed");
            animIDHorizontal = Animator.StringToHash("Horizontal");
            animIDVertical = Animator.StringToHash("Vertical");
            animIDHorizontalDirection = Animator.StringToHash("HorizontalDirection");
            animIDVerticalDirection = Animator.StringToHash("VerticalDirection");
            animIDGetHit = Animator.StringToHash("GetHit");
            animIDDizzy = Animator.StringToHash("Dizzy");
            animIDDie = Animator.StringToHash("Die");
            animIDAttack = Animator.StringToHash("Attack");
        }
    }
     
    public enum AnimationLayer
    {
        Base,
        Melee
    }
}