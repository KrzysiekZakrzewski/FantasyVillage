using UnityEngine;

namespace Units.Implementation
{
    public class DefaultUnit : UnitBase
    {
        [SerializeField]
        private DefaultUnitDataSO initialData;

        public override void Launch(Vector3 position)
        {

        }

        protected override void ResetUnit()
        {

        }
    }
}