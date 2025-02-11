using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;

namespace BlueRacconGames.AI.Factory
{
    public class IdleAIModeFactory : AIModeFactoryBase
    {
        public override IAIMode CreateAIMode(AIControllerBase aIController, EnemyAIDataBaseSO aIData)
        {
            return new IdleAIMode(aIController, aIData);
        }

        public override bool ChangeValidator(float distanceToPlayer)
        {
            return distanceToPlayer > ValidateDistance;
        }
    }
}
