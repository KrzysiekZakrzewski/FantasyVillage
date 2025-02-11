using BlueRacconGames.AI.Factory;
using UnityEngine;

namespace BlueRacconGames.AI.Data
{
    [CreateAssetMenu(fileName = nameof(IdleEnemyAIDataSO), menuName = nameof(AI) + "/" + nameof(AI.Data) + "/" + nameof(IdleEnemyAIDataSO))]
    public class IdleEnemyAIDataSO : EnemyAIDataBaseSO
    {
        [field: SerializeField]
        public float SimulationDistance { private set; get; } = 20f;
    }
}
