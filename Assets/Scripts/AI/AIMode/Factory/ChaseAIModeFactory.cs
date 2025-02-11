using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;

namespace BlueRacconGames.AI.Factory
{
    public class ChaseAIModeFactory : AIModeFactoryBase
    {
        public override IAIMode CreateAIMode(AIControllerBase aIController, EnemyAIDataBaseSO aIData)
        {
            return new ChaseAIMode(aIController, aIData);
        }

        public override bool ChangeValidator(float distanceToPlayer)
        {
            return distanceToPlayer < ValidateDistance;
        }
    }
}