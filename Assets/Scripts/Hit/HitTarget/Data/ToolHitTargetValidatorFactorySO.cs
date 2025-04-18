using BlueRacconGames.Hit.Validator;
using UnityEngine;

namespace BlueRacconGames.Hit.Data
{
    public interface IHitTargetValidatorFactory
    {
        IHitTargetValidator CreateValidator();
    }

    [CreateAssetMenu(fileName = nameof(ToolHitTargetValidatorFactorySO), menuName = nameof(Data) + "/" + nameof(ToolHitTargetValidatorFactorySO))]
    public class ToolHitTargetValidatorFactorySO : ScriptableObject, IHitTargetValidatorFactory
    {
        [field: SerializeField] public HitTargetValidatorData[] ValidatorContainer { get; private set; }

        public IHitTargetValidator CreateValidator()
        {
            return new ToolHitTargetValidator(this);
        }
    }
}