using UnityEngine;

namespace Damageable.Implementation
{
    [CreateAssetMenu(fileName = nameof(DefaultDealDamageDataSO), menuName = nameof(Damageable) + "/" + nameof(Implementation) + "/" + nameof(DefaultDealDamageDataSO))]
    public class DefaultDealDamageDataSO : ScriptableObject
    {
        [field: SerializeField]
        public int MaxDamage { get; private set; }
        [field: SerializeField]
        public int MinDamage { get; private set; }
        [field: SerializeField]
        public float KnockbackForce { get; private set; }
    }
}