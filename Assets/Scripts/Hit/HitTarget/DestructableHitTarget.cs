using BlueRacconGames.HitEffect.Data;
using BlueRacconGames.Resources;
using Game.Item;
using System;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Hit
{
    [RequireComponent(typeof(DestructableResourceSource))]
    public class DestructableHitTarget : HitTargetBase
    {
        [field: SerializeField] public Animator Animator { private set; get; }
        private int maxHitPoints;
        private int currentHitpoints;

        protected bool isDestroyed;

        public event Action OnDestroyE;

        public override void Initialize(DiContainer container, GetHitTargetDataSO initialData)
        {
            base.Initialize(container, initialData);

            var destructableHitEffectData = initialData as DestructableGetHitTargetDataSO;

            maxHitPoints = currentHitpoints = destructableHitEffectData.HitPointsToDestroy;
        }

        public override bool CanGetHit()
        {
            return !isDestroyed;
        }

        protected override void OnGetHitInternal(HitObjectControllerBase source, ToolItemBase tool, int hitPoints)
        {
            base.OnGetHitInternal(source, tool, hitPoints);

            currentHitpoints -= tool.BaseHitPoints;

            if (currentHitpoints > 0) return;

            OnDestroyed();
        }

        private void OnDestroyed()
        {
            if (isDestroyed)
                return;

            OnDeadInternal();
        }

        private void OnDeadInternal()
        {
            isDestroyed = true;
            OnDestroyE?.Invoke();
        }
    }
}