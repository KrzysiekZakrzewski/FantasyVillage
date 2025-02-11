using Damageable.Implementation;
using BlueRacconGames.AI.Data;
using UnityEngine;

namespace Units.Implementation
{
    [CreateAssetMenu(fileName = nameof(EnemyUnitDataSO), menuName = nameof(Units) + "/" + nameof(Units.Implementation) + "/" + nameof(EnemyUnitDataSO))]
    public class EnemyUnitDataSO : ScriptableObject
    {
        [field: SerializeField]
        public EnemyDamagableDataSO EnemyDamagableDataSO { get; private set; }
        [field: SerializeField]
        public EnemyAIDataBaseSO EnemyAIDataSO { get; private set; }
    }
}