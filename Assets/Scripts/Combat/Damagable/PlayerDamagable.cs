using BlueRacconGames.Animation;
using BlueRacconGames.UI.Bars;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Damageable.Implementation
{
    public class PlayerDamagable : DamageableBase
    {
        private PlayerDamagableDataSO damagableData;
        private HealthBar healthBar;

        [Inject]
        private void Inject(HealthBar healthBar)
        {
            this.healthBar = healthBar;
        }

        public override bool ExpireOnDead => damagableData.ExpireOnDead;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isImmune) return;

            if (!damagableData.DamagableLayer.Contains(collision.gameObject.layer)) return;

            TakeDamage(1);
        }

        public override void Launch(IDamagableDataSO damagableDataSO)
        {
            damagableData = damagableDataSO as PlayerDamagableDataSO;

            base.Launch(damagableDataSO);

            healthBar.Launch(CurrentHealth, MaxHealth);
        }

        protected override void HealInternal(int healValue)
        {
            base.HealInternal(healValue);

            healthBar.UpdateBar(CurrentHealth, MaxHealth);
        }

        protected override void TakeDamageInternal(int damageValue)
        {
            base.TakeDamageInternal(damageValue);

            Debug.Log("2");

            healthBar.UpdateBar(CurrentHealth, MaxHealth);

            StartCoroutine(GetHitSequence());
        }

        protected override void IncreaseHealtInternal(int increaseValue)
        {
            base.IncreaseHealtInternal(increaseValue);

            healthBar.UpdateBar(CurrentHealth, MaxHealth);
        }

        protected override void Expire()
        {
            if (expired) return;

            expired = true;
        }
    }
}