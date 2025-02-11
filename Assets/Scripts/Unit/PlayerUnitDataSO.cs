using Damageable.Implementation;
using UnityEngine;

namespace Units.Implementation
{
    [CreateAssetMenu(fileName = nameof(PlayerUnitDataSO), menuName = nameof(Units) + "/" + nameof(Units.Implementation) + "/" + nameof(PlayerUnitDataSO))]
    public class PlayerUnitDataSO : ScriptableObject
    {
        [field: SerializeField]
        public PlayerDamagableDataSO PlayerDamagableDataSO { get; private set; }
    }
}
