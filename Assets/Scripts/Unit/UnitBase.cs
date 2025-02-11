using System;
using UnityEngine;

namespace Units.Implementation
{
    public abstract class UnitBase : MonoBehaviour, IUnit
    {
        public GameObject GameObject => gameObject;

        public event Action<IUnit> OnLaunchE;
        public event Action<IUnit> OnExpireE;

        protected abstract void ResetUnit();

        public abstract void Launch(Vector3 position);
    }
}