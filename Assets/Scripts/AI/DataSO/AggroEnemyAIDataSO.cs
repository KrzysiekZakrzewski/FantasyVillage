using UnityEngine;

namespace BlueRacconGames.AI.Data
{
    [CreateAssetMenu(fileName = nameof(AggroEnemyAIDataSO), menuName = nameof(AI) + "/" + nameof(AI.Data) + "/" + nameof(AggroEnemyAIDataSO))]
    public class AggroEnemyAIDataSO : ChaseEnemyAIDataSO
    {
        [field: SerializeField]
        public float AttackDelay = 2f;
    }
}
