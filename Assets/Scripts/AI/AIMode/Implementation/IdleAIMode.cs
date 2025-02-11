using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public class IdleAIMode : AIModeBase
    {
        private float simulationDistance;
        private float reachDestinationDistance;
        private Vector2 destination;
        private float walkableAreaRadius;

        private IAIModeFactory idleAIModeOption;

        public IdleAIMode(AIControllerBase aIController, EnemyAIDataBaseSO enemyAIDataSO)
        {
            Initialize(aIController, enemyAIDataSO);
        }

        public override void Initialize(AIControllerBase aIController, EnemyAIDataBaseSO enemyAIDataSO)
        {
            base.Initialize(aIController, enemyAIDataSO);

            var enemyAIData = enemyAIDataSO as IdleEnemyAIDataSO;

            simulationDistance = enemyAIData.SimulationDistance;
            reachDestinationDistance = enemyAIData.ReachDestinationDistance;
            idleAIModeOption = enemyAIData.IdleAIModeOptions;

            SetRandomWalkPoint();
        }

        public override void Update()
        {
            base.Update();

            InternalUpdate();
        }

        private void InternalUpdate()
        {
            if (!CanSimulate() && destination == Vector2.zero)
                return;

            Walk();
        }

        private float CalculateDistance(Vector2 destination)
        {
            return Vector2.Distance(AIController.transform.position, destination);
        }

        private bool TryChase()
        {
            var distanceToPlayer = CalculateDistance(playerTransform.position);

            if (!idleAIModeOption.ChangeValidator(distanceToPlayer))
                return false;

            AIController.TryChangeAIMode();

            return true;
        }

        private void SetRandomWalkPoint()
        {
            var destination = CanSimulate() ? CalculateDestinationPoint() : Vector2.zero;

            AIController.UpdatePath(destination);
        }

        private Vector2 CalculateDestinationPoint(float radius = 0f, Transform pos = null)
        {
            var curRad = radius != 0 ? radius : walkableAreaRadius;
            var curTrans = pos != null ? pos.position : AIController.transform.position;

            var randPos = Random.insideUnitCircle * curRad;

            destination = randPos;

            Debug.Log("Rand Pos: " + destination);

            return destination;
        }

        private void Walk()
        {
            if (TryChase())
                return;

            if (!IsInDestination())
                return;

            SetRandomWalkPoint();
        }

        private bool IsInDestination()
        {
            return CalculateDistance(destination) <= reachDestinationDistance;
        }

        private bool CanSimulate()
        {
            if (CalculateDistance(playerTransform.position) > simulationDistance)
            {
                StopSimulate();
                return false;
            }

            StartSimulate();

            return true;
        }

        public override bool CanChangeMode(out IAIModeFactory modeFactory)
        {
            var distanceToPlayer = CalculateDistance(playerTransform.position);

            modeFactory = idleAIModeOption;

            return idleAIModeOption.ChangeValidator(distanceToPlayer);
        }

        public override void OnStartWonder()
        {

        }

        public override void OnEndWonder()
        {

        }
    }
}