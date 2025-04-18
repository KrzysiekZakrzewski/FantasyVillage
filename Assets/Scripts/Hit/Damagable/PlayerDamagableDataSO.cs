using UnityEngine;

namespace Damageable.Implementation
{
    [CreateAssetMenu(fileName = nameof(PlayerDamagableDataSO), menuName = nameof(Damageable) + "/" + nameof(Damageable.Implementation) + "/" + nameof(PlayerDamagableDataSO))]
    public class PlayerDamagableDataSO : ScriptableObject, IDamagableDataSO
    {
        [field: SerializeField]
        public int MaxHealth { get; private set; }

        [field: SerializeField]
        public bool ExpireOnDead { get; private set; }
        [field: SerializeField]
        public LayerMask DamagableLayer { get; private set; }
    }
}
