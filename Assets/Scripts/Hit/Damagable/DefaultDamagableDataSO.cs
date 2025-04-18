using UnityEngine;

namespace Damageable.Implementation
{
    [CreateAssetMenu(fileName = nameof(DefaultDamagableDataSO), menuName = nameof(Damageable) + "/" + nameof(Damageable.Implementation) + "/" + nameof(DefaultDamagableDataSO))]
    public class DefaultDamagableDataSO : ScriptableObject
    {
        [field: SerializeField]
        public int MaxHealt { get; private set; }

        [field: SerializeField]
        public bool ExpireOnDead { get; private set; }
    }
}
