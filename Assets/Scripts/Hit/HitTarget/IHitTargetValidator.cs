using BlueRacconGames.Hit.Data;
using Game.Item;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.Hit.Validator
{
    public interface IHitTargetValidator 
    {
        event Action<IHitTarget> OnValidate;
        event Action<IHitTarget> OnReject;

        public ValidateResult TryValidate(IHitTarget target, ToolItemBase tool);
    }

    public abstract class HitTargetValidatorBase : IHitTargetValidator
    {
        protected Dictionary<HitTargetType, HitTargetValidatorData> validatorDataLUT;

        public event Action<IHitTarget> OnValidate;
        public event Action<IHitTarget> OnReject;

        public HitTargetValidatorBase(ToolHitTargetValidatorFactorySO dataContainer)
        {
            validatorDataLUT = new();

            foreach (HitTargetValidatorData data in dataContainer.ValidatorContainer)
            {
                validatorDataLUT.Add(data.ValidateTargetTypes, data);
            }
        }

        public ValidateResult TryValidate(IHitTarget target, ToolItemBase tool)
        {
            ValidateResult validateResult = Validate(target, tool);

            ResultCall(validateResult, target);

            return validateResult;
        }

        protected abstract ValidateResult Validate(IHitTarget target, ToolItemBase tool);

        protected void ResultCall(ValidateResult result, IHitTarget target)
        {
            Action<IHitTarget> resultAction = result.Result ? OnValidate : OnReject;

            resultAction?.Invoke(target);
        }
    }

    public class ToolHitTargetValidator : HitTargetValidatorBase
    {
        public ToolHitTargetValidator(ToolHitTargetValidatorFactorySO dataContainer) : base(dataContainer)
        {
        }

        protected override ValidateResult Validate(IHitTarget target, ToolItemBase tool)
        {
            bool isValidated = validatorDataLUT.TryGetValue(target.TargetType, out var data);

            if (!isValidated)
                return new ValidateResult(isValidated, 0);

            int hitPoints = Mathf.RoundToInt(data.TargetHitPointMultiplier * tool.BaseHitPoints);

            return new ValidateResult(isValidated, hitPoints);
        }
    }

    public struct ValidateResult
    {
        public bool Result { get; private set; }
        public int HitPoints { get; private set; }

        public ValidateResult(bool result, int hitPoints)
        {
            Result = result;
            HitPoints = hitPoints;
        }
    }
}