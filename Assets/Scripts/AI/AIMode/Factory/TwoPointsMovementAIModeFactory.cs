using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;
using UnityEngine;

namespace BlueRacconGames.AI.Factory
{
    public class TwoPointsMovementAIModeFactory : AIModeFactoryBase
    {
        [field: SerializeField]
        protected float MaxDestinationSideDistance { get; private set; }

        public override IAIMode CreateAIMode(AIControllerBase aIController, EnemyAIDataBaseSO aIData)
        {
            return new TwoPointsMovementAIMode(aIController, aIData, MaxDestinationSideDistance);
        }

        public override bool ChangeValidator(float distanceToPlayer)
        {
            return false;
        }
    }
}
