using BlueRacconGames.AI.Factory;
using UnityEngine;

namespace BlueRacconGames.AI.Data
{
    [CreateAssetMenu(fileName = nameof(ChaseEnemyAIDataSO), menuName = nameof(AI) + "/" + nameof(AI.Data) + "/" + nameof(ChaseEnemyAIDataSO))]
    public class ChaseEnemyAIDataSO : IdleEnemyAIDataSO
    {
        [field: SerializeField]
        public float ChaseMoveSpeed = 2f;
        [field: SerializeReference, ReferencePicker]
        public IAIModeFactory[] ChaseAIModeOptions { private set; get; }
    }
}
