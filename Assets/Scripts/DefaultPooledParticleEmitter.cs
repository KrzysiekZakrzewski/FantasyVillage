using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace BlueRacconGames.Pool
{
    [Serializable]
    public class DefaultPooledParticleEmitter : MonoBehaviour, IPoolItemEmitter
    {
        private readonly Dictionary<IPoolItem, ObjectPool<IPoolItem>> partcilePrefabToPoolLut = new Dictionary<IPoolItem, ObjectPool<IPoolItem>>();
        private readonly Dictionary<IPoolItem, ObjectPool<IPoolItem>> partcileInstanceToPoolLut = new Dictionary<IPoolItem, ObjectPool<IPoolItem>>();

        public DefaultPooledParticleEmitter()
        {
        }

        public void Clear()
        {
            partcilePrefabToPoolLut.Clear();
            partcileInstanceToPoolLut.Clear();
        }

        public void EmitItem(IPoolItem particlePrefab, Vector3 startPosition)
        {
            ParticlePoolItem newParticle = GetParticleFromPool(particlePrefab);
            newParticle.Launch(this, startPosition);
        }

        private ParticlePoolItem GetParticleFromPool(IPoolItem particlePrefab)
        {
            if (!partcilePrefabToPoolLut.TryGetValue(particlePrefab, out ObjectPool<IPoolItem> pool))
            {
                pool = new ObjectPool<IPoolItem>(() => CreateParticle(particlePrefab), OnGetParticle, OnReleaseParticle);
                partcilePrefabToPoolLut.Add(particlePrefab, pool);
            }

            IPoolItem newParticle = pool.Get();
            partcileInstanceToPoolLut.TryAdd(newParticle, pool);
            return newParticle as ParticlePoolItem;
        }
        private void OnGetParticle(IPoolItem particle)
        {
            particle.OnExpireE += Particle_OnExpireE;
        }

        private void OnReleaseParticle(IPoolItem particle)
        {

        }

        private void Particle_OnExpireE(IPoolItem particle)
        {
            particle.OnExpireE -= Particle_OnExpireE;
            partcileInstanceToPoolLut[particle].Release(particle);
        }

        private ParticlePoolItem CreateParticle(IPoolItem particle)
        {
            GameObject newParticleGameObject = GameObject.Instantiate(particle as MonoBehaviour).gameObject;
            newParticleGameObject.transform.SetParent(null);
            ParticlePoolItem particlePoolItem = newParticleGameObject.GetComponent<ParticlePoolItem>();
            return particlePoolItem;
        }
    }
}