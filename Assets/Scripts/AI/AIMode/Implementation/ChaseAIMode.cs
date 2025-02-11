using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public class ChaseAIMode : AIModeBase
    {
        private IAIModeFactory[] chaseAIModeOptions;

        public ChaseAIMode(AIControllerBase aiController, EnemyAIDataBaseSO enemyAIDataSO) 
        { 
            Initialize(aiController, enemyAIDataSO);
        }

        public override void Initialize(AIControllerBase aIController, EnemyAIDataBaseSO enemyAIDataSO)
        {
            base.Initialize(aIController, enemyAIDataSO);

            var enemyAIData = enemyAIDataSO as ChaseEnemyAIDataSO;

            chaseAIModeOptions = enemyAIData.ChaseAIModeOptions;
            MoveSpeed = enemyAIData.ChaseMoveSpeed;

            StartSimulate();
        }

        public override void Update()
        {
            base.Update();

            InternalUpdate();
        }

        private void InternalUpdate()
        {
            FollowPlayer();
        }

        private float CalculateDistance()
        {
            return Vector3.Distance(playerTransform.position, AIController.transform.position);
        }

        private void FollowPlayer()
        {
            if (TryChangeAIMode(out var factoryMode))
            {
                AIController.TryChangeAIMode();
                return;
            }

            AIController.UpdatePath(playerTransform.position);
        }

        private bool TryChangeAIMode(out IAIModeFactory modeFactory)
        {
            bool result = false;
            modeFactory = null;

            var distanceToPlayer = CalculateDistance();

            for (int i = 0; i < chaseAIModeOptions.Length; i++)
            {
                modeFactory = chaseAIModeOptions[i];

                result = modeFactory.ChangeValidator(distanceToPlayer);

                if (result) break;
            }

            return result;
        }

        public override bool CanChangeMode(out IAIModeFactory modeFactory)
        {
            bool result = TryChangeAIMode(out modeFactory);

            return result;
        }

        public override void OnEndWonder()
        {

        }

        public override void OnStartWonder()
        {

        }
    }
}
