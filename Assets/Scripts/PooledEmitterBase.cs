using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace BlueRacconGames.Pool
{
    [Serializable]
    public abstract class PooledEmitterBase : MonoBehaviour, IPoolItemEmitter
    {
        private readonly Dictionary<PoolItemBase, ObjectPool<PoolItemBase>> partcilePrefabToPoolLut = new Dictionary<PoolItemBase, ObjectPool<PoolItemBase>>();
        private readonly Dictionary<PoolItemBase, ObjectPool<PoolItemBase>> partcileInstanceToPoolLut = new Dictionary<PoolItemBase, ObjectPool<PoolItemBase>>();

        public void Clear()
        {
            partcilePrefabToPoolLut.Clear();
            partcileInstanceToPoolLut.Clear();
        }

        public T EmitItem<T>(PoolItemBase prefab, Vector3 startPosition) where T : class
        {
            PoolItemBase newItem = GetItemFromPool(prefab);
            newItem.Launch(this, startPosition);

            return newItem.GetComponent<T>();
        }

        private PoolItemBase GetItemFromPool(PoolItemBase prefab)
        {
            if (!partcilePrefabToPoolLut.TryGetValue(prefab, out ObjectPool<PoolItemBase> pool))
            {
                pool = new ObjectPool<PoolItemBase>(() => CreateItem(prefab), OnGetItem, OnReleaseItem);
                partcilePrefabToPoolLut.Add(prefab, pool);
            }

            PoolItemBase newParticle = pool.Get();
            partcileInstanceToPoolLut.TryAdd(newParticle, pool);
            return newParticle;
        }

        private void OnGetItem(PoolItemBase item)
        {
            item.OnExpireE += Item_OnExpireE;
        }

        private void OnReleaseItem(PoolItemBase item)
        {

        }

        private void Item_OnExpireE(PoolItemBase item)
        {
            item.OnExpireE -= Item_OnExpireE;
            partcileInstanceToPoolLut[item].Release(item);
        }

        private PoolItemBase CreateItem(PoolItemBase item)
        {
            GameObject newParticleGameObject = GameObject.Instantiate(item as MonoBehaviour).gameObject;
            newParticleGameObject.transform.SetParent(transform);
            PoolItemBase particlePoolItem = newParticleGameObject.GetComponent<PoolItemBase>();
            return particlePoolItem;
        }
    }
}
