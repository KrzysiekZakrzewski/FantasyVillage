using UnityEngine;

namespace Damageable.Implementation
{
    public abstract class DealDamageBase : MonoBehaviour, IDealDamage
    {
        public abstract int MaxDamage { get; }

        public abstract int MinDamage { get; }

        public int Damage => Random.Range(MaxDamage, MinDamage);
    }
}
