using System;
using UnityEngine;

namespace BlueRacconGames.Pool
{
    public abstract class PoolItemBase : MonoBehaviour, IPoolItem
    {
        private IPoolItemEmitter sourceEmitter;
        private bool expired;

        public GameObject GameObject => gameObject;

        public event Action<IPoolItem> OnLaunchE;
        public event Action<IPoolItem> OnExpireE;

        public void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition)
        {
            ResetItem();
            this.sourceEmitter = sourceEmitter;
            transform.position = startPosition;
            gameObject.SetActive(true);
        }

        protected virtual void Expire()
        {
            if (expired) return;

            gameObject.SetActive(false);
            expired = true;
            OnExpireE?.Invoke(this);
        }

        public virtual void ResetItem()
        {
            expired = false;
        }
    }
}