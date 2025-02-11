using Damageable;
using BlueRacconGames.AI;
using UnityEngine;

namespace Units.Implementation
{
    public class EnemyUnit : UnitBase
    {
        [SerializeField]
        private EnemyUnitDataSO unitDataSO;

        private IDamageable damageable;
        private AIControllerBase aIController;

        private void Start()
        {
            Launch(transform.position);
        }

        protected override void ResetUnit()
        {
            damageable?.Launch(unitDataSO.EnemyDamagableDataSO);
            aIController.Initialize(unitDataSO.EnemyAIDataSO);
        }

        public override void Launch(Vector3 position)
        {
            damageable ??= GetComponentInChildren<IDamageable>();
            aIController = GetComponent<AIControllerBase>();
            ResetUnit();
        }
    }
}
