using UnityEngine;

namespace Damageable.Implementation
{
    public class EnemyDamagable : DamageableBase
    {
        private EnemyDamagableDataSO initialData;
        public override bool ExpireOnDead => initialData.ExpireOnDead;

        public override void Launch(IDamagableDataSO damagableDataSO)
        {
            initialData = damagableDataSO as EnemyDamagableDataSO;

            base.Launch(damagableDataSO);
        }

        protected override void Expire()
        {
            if (expired) return;

            expired = true;

            SpawnDeathEffect();
            Destroy(gameObject);
        }
        private void SpawnDeathEffect()
        {
            Instantiate(initialData.ExpireParticle, transform.position, Quaternion.identity, null);
        }
    }
}