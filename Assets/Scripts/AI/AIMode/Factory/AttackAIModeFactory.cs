using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;

namespace BlueRacconGames.AI.Factory
{
    public class AttackAIModeFactory : AIModeFactoryBase
    {
        public override IAIMode CreateAIMode(AIControllerBase aIController, EnemyAIDataBaseSO aIData)
        {
            return new AttackAIMode(aIController, aIData);
        }
        public override bool ChangeValidator(float distanceToPlayer)
        {
            return distanceToPlayer <= ValidateDistance;
        }
    }
}
