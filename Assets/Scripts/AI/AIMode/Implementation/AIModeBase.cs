using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public abstract class AIModeBase : IAIMode
    {
        private AIControllerBase aIController;
        private bool isSimulated;

        protected Transform playerTransform;
        protected float reachDestinationDistance;

        public AIControllerBase AIController => aIController;
        public bool IsSimulated => isSimulated;
        public float MoveSpeed { get; protected set; }

        public float ReachDestinationDistance => reachDestinationDistance;

        public virtual void Initialize(AIControllerBase aIController, EnemyAIDataBaseSO enemyAIDataBaseSO)
        {
            this.aIController = aIController;
            MoveSpeed = enemyAIDataBaseSO.BaseMoveSpeed;
            playerTransform = aIController.PlayerTransform;
            reachDestinationDistance = enemyAIDataBaseSO.ReachDestinationDistance;
        }

        public virtual void Update()
        {

        }

        public virtual void OnDestory()
        {

        }

        public abstract bool CanChangeMode(out IAIModeFactory modeFactory);

        public abstract void OnStartWonder();

        public abstract void OnEndWonder();

        public virtual void StartSimulate()
        {
            if (isSimulated) return;

            isSimulated = true;
            aIController.StartNavMeshAgent();
        }

        public virtual void StopSimulate()
        {
            if (!isSimulated) return;

            isSimulated = false;
            aIController.StopNavMeshAgent();
        }
    }
}
