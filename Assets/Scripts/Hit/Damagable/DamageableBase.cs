using Assets.Scripts.Events;
using BlueRacconGames.Animation;
using BlueRacconGames.Hit;
using BlueRacconGames.Movement;
using System;
using System.Collections;
using UnityEngine;

namespace Damageable.Implementation
{
    [RequireComponent(typeof(IHitTarget))]
    public abstract class DamageableBase : MonoBehaviour, IDamageable
    {
        [SerializeField] protected BoolGameEvent turnOnCanMoveEvent;
        [SerializeField] protected BoolGameEvent turnOffCanMoveEvent;
        protected int maxHealth;
        protected bool expired;
        protected bool isImmune;
        protected UnitAnimationControllerBase animationController;
        protected TopDownCharacterController2D characterController;
        private int currentHealth;
        private bool dead;

        public GameObject GameObject => gameObject;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public bool Dead => dead;
        public abstract bool ExpireOnDead { get; }

        public bool IsImmune => isImmune;

        public event Action OnTakeDamageE;
        public event Action OnHealE;
        public event Action OnDeadE;
        public event Action<IDamageable> OnExpireE;

        protected virtual void Awake()
        {
            characterController = GetComponent<TopDownCharacterController2D>();
            animationController = characterController.AnimationController;
        }

        public virtual void Launch(IDamagableDataSO damagableDataSO)
        {
            maxHealth = damagableDataSO.MaxHealth;
            ResetDamagable();
        }

        public void OnDead()
        {
            if (dead)
                return;

            OnDeadInternal();
        }

        public void TakeDamage(int damageValue)
        {
            if (dead)
                return;

            TakeDamageInternal(damageValue);
        }

        public void Heal(int healValue)
        {
            if (currentHealth >= MaxHealth)
                return;

            HealInternal(healValue);
        }

        public void IncreaseHealt(int increaseValue)
        {
            IncreaseHealtInternal(increaseValue);
        }

        public void DecreaseHealt(int decreaseValue)
        {
            DecreaseHealtInternal(decreaseValue);
        }

        protected virtual IEnumerator GetHitSequence()
        {
            turnOffCanMoveEvent.Reise();
            isImmune = true;
            animationController.GetHitAnimation();
            Knockback(5f);
            yield return new WaitUntil(() => animationController.GetBoolParameter("DamageDetect"));

            yield return new WaitWhile(() => animationController.GetBoolParameter("DamageDetect"));

            turnOnCanMoveEvent.Reise();
            isImmune = false;
        }

        protected virtual void TakeDamageInternal(int damageValue)
        {
            currentHealth -= damageValue;

            OnTakeDamageE?.Invoke();

            if (currentHealth > 0)
            {
                StartCoroutine(GetHitSequence());
                return;
            }

            OnDead();
        }

        protected virtual void Knockback(float force)
        {
            turnOffCanMoveEvent.Reise();

            Rigidbody2D rb = characterController.Rb;

            if (rb == null) return;

            rb.linearVelocity = Vector2.zero;
            rb.AddForceX((transform.localScale.x * force) * -1, ForceMode2D.Impulse);
        }

        protected virtual void HealInternal(int healValue)
        {
            currentHealth += healValue;

            OnHealE?.Invoke();

            currentHealth = currentHealth > MaxHealth ? MaxHealth : currentHealth;
        }

        protected virtual void IncreaseHealtInternal(int increaseValue)
        {
            maxHealth += increaseValue;

            currentHealth = maxHealth;
        }

        protected virtual void DecreaseHealtInternal(int decreaseValue)
        {
            maxHealth -= decreaseValue;

            currentHealth = maxHealth < currentHealth ? maxHealth : currentHealth;
        }

        private void OnDeadInternal()
        {
            dead = true;
            OnDeadE?.Invoke();

            if (!ExpireOnDead)
                return;

            Expire();
        }

        protected abstract void Expire();

        private void ResetDamagable()
        {
            expired = false;
            dead = false;
            currentHealth = MaxHealth;
        }
    }
}
