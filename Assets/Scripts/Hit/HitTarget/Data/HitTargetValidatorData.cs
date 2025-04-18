using UnityEngine;

namespace BlueRacconGames.Hit.Data
{
    //[CreateAssetMenu(fileName = nameof(HitTargetValidatorData), menuName = nameof(Data) + "/" + nameof(HitTargetValidatorData))]
    [System.Serializable]
    public class HitTargetValidatorData 
    {
        [field: SerializeField] public HitTargetType ValidateTargetTypes { get; private set; }
        [field: SerializeField] public float TargetHitPointMultiplier { get; private set; }
    }
}