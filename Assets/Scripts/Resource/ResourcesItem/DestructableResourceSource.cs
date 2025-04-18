using BlueRacconGames.Hit;
using BlueRacconGames.Hit.ExpireEffect;
using BlueRacconGames.Hit.ExpireEffect.Factory;
using BlueRacconGames.Pool;
using BlueRacconGames.Resources.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Resources
{
    public class DestructableResourceSource : ResourceSourceBase
    {
        [SerializeField] private DestructableResourceSourceDataSO initialData;
        [SerializeField] private ExpireEffectFactorySO[] expireEffectsFactory;

        private IExpireEffect[] expireEffects;
        private DestructableHitTarget hitTarget;
        private DiContainer container;

        private List<IExpireEffect> effectProcess;

        [Inject]
        private void Inject(DiContainer container)
        {
            this.container = container;
        }

        protected override void Awake()
        {
            base.Awake();

            hitTarget = GetComponent<DestructableHitTarget>();

            expireEffects = new IExpireEffect[expireEffectsFactory.Length];

            for (int i = 0; i < expireEffectsFactory.Length; i++)
            {
                var effect = expireEffectsFactory[i].CreateExpireEffect();
                expireEffects[i] = effect;
            }

            effectProcess = new();
        }

        public override void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition)
        {
            base.Launch(sourceEmitter, startPosition);

            resourceSourcesDatas = initialData.ResourceItemDataSO;

            hitTarget.Initialize(container, initialData.DestructableGetHitTargetData);
            hitTarget.OnDestroyE += OnDestroyEvent;

            foreach(IExpireEffect effect in expireEffects)
            {
                effect.OnExecutedE += OnExpireEventExecuted;
                container.Inject(effect);
            }
        }

        public override void ResetItem()
        {
            base.ResetItem();

            if (hitTarget == null) return;

            foreach (IExpireEffect effect in expireEffects)
            {
                effect.OnExecutedE -= OnExpireEventExecuted;
            }

            hitTarget.OnDestroyE -= OnDestroyEvent;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (hitTarget == null) return;

            hitTarget.OnDestroyE -= OnDestroyEvent;
        }

        private void OnDestroyEvent()
        {
            if (expireEffects.Length == 0)
            {
                Expire();
                SpawnItems();
                return;
            }

            ReiseExpireEffects();

            StartCoroutine(WaitForEndEffects());
        }

        private void ReiseExpireEffects()
        {
            foreach(var effect in expireEffects)
            {
                effectProcess.Add(effect);
                effect.Execute(hitTarget);
            }
        }

        private IEnumerator WaitForEndEffects()
        {
            yield return null;

            while(effectProcess.Count > 0)
            {
                yield return null;
            }

            Expire();
            SpawnItems();
        }

        private void OnExpireEventExecuted(IExpireEffect expireEffect)
        {
            effectProcess.Remove(expireEffect);
        }
    }
}