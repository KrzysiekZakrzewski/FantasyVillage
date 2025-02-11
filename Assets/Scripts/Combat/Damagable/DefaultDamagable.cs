using UnityEngine;

namespace Damageable.Implementation
{
    public class DefaultDamagable : DamageableBase
    {
        [SerializeField]
        private DefaultDamagableDataSO initialData;

        public override bool ExpireOnDead => initialData.ExpireOnDead;

        protected override void Expire()
        {
        }
    }
}
