using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlueRacconGames.AI.Implementation
{
    public class TwoPointsMovementAIMode : AIModeBase
    {
        private float simulationDistance;
        private float reachDestinationDistance;
        private float minDestinationSideDistance;
        private float maxDestinationSideDistance;
        private Vector2 destination;
        private Vector2 startPos;

        public TwoPointsMovementAIMode(AIControllerBase aIController, EnemyAIDataBaseSO enemyAIDataSO, float maxDestinationSideDistance)
        {
            this.maxDestinationSideDistance = maxDestinationSideDistance;
            minDestinationSideDistance = maxDestinationSideDistance / 2;

            Initialize(aIController, enemyAIDataSO);
        }

        public override void Initialize(AIControllerBase aIController, EnemyAIDataBaseSO enemyAIDataSO)
        {
            base.Initialize(aIController, enemyAIDataSO);

            var enemyAIData = enemyAIDataSO as TwoPointsMovementEnemyAIDataSO;

            simulationDistance = enemyAIData.SimulationDistance;
            reachDestinationDistance = enemyAIData.ReachDestinationDistance;
            startPos = AIController.transform.position;

            AIController.OnObstacleCollide += SetRandomWalkPoint;

            SetRandomWalkPoint();
        }

        public override void Update()
        {
            base.Update();

            InternalUpdate();
        }

        public override void OnDestory()
        {
            base.OnDestory();

            AIController.OnObstacleCollide -= SetRandomWalkPoint;
        }

        public override bool CanChangeMode(out IAIModeFactory modeFactory)
        {
            modeFactory = null;

            return false;
        }

        public override void OnEndWonder()
        {

        }

        public override void OnStartWonder()
        {

        }

        private void SetRandomWalkPoint()
        {
            var destination = CanSimulate() ? CalculateDestinationPoint() : Vector2.zero;

            AIController.UpdatePath(destination);
        }

        private Vector2 CalculateDestinationPoint(float radius = 0f, Transform pos = null)
        {
            Vector2 destinationMultiplayer = new (Random.Range(minDestinationSideDistance, maxDestinationSideDistance), 0);

            destination = destination.x > startPos.x ? destination -= destinationMultiplayer : destination += destinationMultiplayer;

            return destination;
        }

        private void Walk()
        {
            if (!IsInDestination())
                return;

            SetRandomWalkPoint();
        }

        private bool IsInDestination()
        {
            return CalculateDistance(destination) <= reachDestinationDistance;
        }

        private float CalculateDistance(Vector2 destination)
        {
            return Vector2.Distance(AIController.transform.position, destination);
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

        private void InternalUpdate()
        {
            if (!CanSimulate() && destination == Vector2.zero)
                return;

            Walk();
        }
    }
}
