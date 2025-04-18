using BlueRacconGames.Hit.Data;
using BlueRacconGames.HitEffect;
using BlueRacconGames.HitEffect.Data;
using BlueRacconGames.HitEffect.Factory;
using Game.Item;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Hit
{
    public abstract class HitTargetBase : MonoBehaviour, IHitTarget
    {
        protected DiContainer container;
        private HitTargetType targetType;
        private readonly List<IHitTargetEffect> getHitTargetEffects = new();

        public event Action OnTakeHitE;
        public GameObject GameObject => gameObject;

        public HitTargetType TargetType => targetType;

        public virtual void Initialize(DiContainer container, GetHitTargetDataSO initialData)
        {
            this.container ??= container;
            this.targetType = initialData.TargetType;

            foreach (IHitTargetEffectFactory toolTargetEffectFactory in initialData.GetHitTargetEffectFactory)
            {
                var effect = toolTargetEffectFactory.CreateEffect();
                this.container.Inject(effect);
                getHitTargetEffects.Add(effect);
            }
        }

        public abstract bool CanGetHit();
        public bool OnGetHit(HitObjectControllerBase source, ToolItemBase tool, int hitPoints)
        {
            if (!CanGetHit()) return false;

            OnGetHitInternal(source, tool, hitPoints);

            return true;
        }
        protected virtual void OnGetHitInternal(HitObjectControllerBase source, ToolItemBase tool, int hitPoints)
        {
            foreach (IHitTargetEffect toolTargetHitEffect in getHitTargetEffects)
            {
                toolTargetHitEffect.Execute(source, this);
            }

            OnTakeHitE?.Invoke();
        }
    }
}
