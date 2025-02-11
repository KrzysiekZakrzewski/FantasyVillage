using Damageable;
using UnityEngine;

namespace Units.Implementation
{
    public class PlayerUnit : UnitBase
    {
        [SerializeField]
        private PlayerUnitDataSO unitDataSO;

        private IDamageable damageable;

        private void Awake()
        {
            Launch(Vector3.zero);
        }

        protected override void ResetUnit()
        {
            damageable.Launch(unitDataSO.PlayerDamagableDataSO);
        }

        public override void Launch(Vector3 position)
        {
            damageable ??= GetComponent<IDamageable>();

            ResetUnit();
        }
    }
}