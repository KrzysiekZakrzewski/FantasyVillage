using UnityEngine;

namespace Damageable.Implementation
{
    public class CollisionDealDamage : DealDamageBase
    {
        [SerializeField]
        private DefaultDealDamageDataSO initialData;

        public override int MaxDamage => initialData.MaxDamage;
        public override int MinDamage => initialData.MinDamage;
    }
}