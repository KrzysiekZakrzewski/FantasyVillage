using System.Collections.Generic;
using System;
using Game.Item.Factory.Implementation;
using BlueRacconGames.Animation;
using RDG.Platforms;
using System.Collections;
using BlueRacconGames.Hit;
using BlueRacconGames.HitEffect;
using BlueRacconGames.HitEffect.Factory;
using Assets.Scripts.Events;
using BlueRacconGames.Hit.Validator;
using UnityEngine;
using BlueRacconGames.Hit.Data;

namespace Game.Item
{
    public abstract class ToolItemBase : UsableItemBase
    {
        protected BoolGameEvent turnOnCanMoveEvent;
        protected BoolGameEvent turnOffCanMoveEvent;
        private readonly int baseHitPoints;
        private readonly string useAnimation;
        private readonly HashSet<IHitTarget> hitTargets = new();
        private readonly List<IHitTargetEffect> toolHitTargetEffect;
        private readonly IHitTargetValidator targetValidator; 

        public event Action<IHitTarget> OnHitE;
        public int BaseHitPoints => baseHitPoints;

        protected ToolItemBase(ToolBaseFactorySO initialData) : base(initialData)
        {
            toolHitTargetEffect = new();
            targetValidator = initialData.ValidatorFactory.CreateValidator();
            baseHitPoints = initialData.HitPoint;
            useAnimation = initialData.UseAnimation;

            turnOnCanMoveEvent = initialData.TurnOnCanMoveEvent;
            turnOffCanMoveEvent = initialData.TurnOffCanMoveEvent;

            foreach (IHitTargetEffectFactory toolTargetEffectFactory in initialData.ToolTargetEffectFactory)
            {
                toolHitTargetEffect.Add(toolTargetEffectFactory.CreateEffect());
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void OnHit(HitObjectControllerBase source, IHitTarget target)
        {
            if (hitTargets.Contains(target))
                return;

            hitTargets.Add(target);
            OnHitInternal(source, target);
        }

        public void ResetTool()
        {
            hitTargets.Clear();
            inUse = false;
            turnOnCanMoveEvent.Reise();
        }

        public override bool Use(UnitAnimationControllerBase unitAnimation)
        {
            if (!CanUse()) return false;

            inUse = true;

            turnOffCanMoveEvent.Reise();

            return UseInternal(unitAnimation);
        }

        protected override bool CanUse()
        {
            if (inUse) return false;

            return true;
        }

        protected virtual void OnHitInternal(HitObjectControllerBase source, IHitTarget target)
        {
            ValidateResult validateResult = targetValidator.TryValidate(target, this);

            if (!validateResult.Result) return;

            var targetResult = target.OnGetHit(source, this, validateResult.HitPoints);

            if (!targetResult) return;

            OnHitE?.Invoke(target);

            foreach (IHitTargetEffect toolTargetHitEffect in toolHitTargetEffect)
            {
                toolTargetHitEffect.Execute(source, target);
            }
        }

        protected virtual bool UseInternal(UnitAnimationControllerBase unitAnimation)
        {
            unitAnimation.ToolAnimationByName(useAnimation);

            CorutineSystem.StartSequnce(WaitForAndAnimation(unitAnimation));

            return true;
        }

        private IEnumerator WaitForAndAnimation(UnitAnimationControllerBase unitAnimation)
        {
            var toolInUse = true;

            while (toolInUse)
            {
                yield return null;

                toolInUse = unitAnimation.GetBoolParameter("ToolInUse");
            }

            ResetTool();
        }
    }
}