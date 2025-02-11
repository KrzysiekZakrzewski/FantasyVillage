using UnityEngine;

namespace Units.Implementation
{
    [CreateAssetMenu(fileName = nameof(DefaultUnitDataSO), menuName = nameof(Units) + "/" + nameof(Units.Implementation) + "/" + nameof(DefaultUnitDataSO))]
    public class DefaultUnitDataSO : ScriptableObject
    {
        [field: SerializeField]
        public float MaxHealth { private set; get; }
    }
}