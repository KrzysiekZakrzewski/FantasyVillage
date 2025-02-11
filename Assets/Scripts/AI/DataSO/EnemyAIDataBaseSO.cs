using BlueRacconGames.AI.Factory;
using UnityEngine;

namespace BlueRacconGames.AI.Data
{
    public class EnemyAIDataBaseSO : ScriptableObject
    {
        [field: SerializeField]
        public float BaseMoveSpeed { private set; get; } = 1f;
        [field: SerializeField]
        public float ReachDestinationDistance { private set; get; } = 2f;
        [field: SerializeReference, ReferencePicker]
        public IAIModeFactory IdleAIModeOptions { private set; get; }
    }
}
