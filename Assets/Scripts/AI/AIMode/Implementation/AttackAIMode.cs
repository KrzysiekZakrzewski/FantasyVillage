using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;

namespace BlueRacconGames.AI.Implementation
{
    public class AttackAIMode : AIModeBase
    {
        public AttackAIMode(AIControllerBase aiController, EnemyAIDataBaseSO enemyAIDataSO) 
        {
            Initialize(aiController, enemyAIDataSO);
        }

        public override void Initialize(AIControllerBase aIController, EnemyAIDataBaseSO enemyAIDataBaseSO)
        {
            base.Initialize(aIController, enemyAIDataBaseSO);
        }

        public override bool CanChangeMode(out IAIModeFactory modeFactory)
        {
            modeFactory = null;

            return false;
        }

        public override void OnEndWonder()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStartWonder()
        {
            throw new System.NotImplementedException();
        }
    }
}
