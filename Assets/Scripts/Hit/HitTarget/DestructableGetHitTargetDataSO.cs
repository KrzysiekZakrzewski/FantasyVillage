using UnityEngine;

namespace BlueRacconGames.HitEffect.Data
{
    [CreateAssetMenu(fileName = nameof(DestructableGetHitTargetDataSO), menuName = nameof(HitEffect.Data) + "/" + nameof(DestructableGetHitTargetDataSO))]

    public class DestructableGetHitTargetDataSO : GetHitTargetDataSO
    {
        [field: SerializeField] public int HitPointsToDestroy { get; private set; }
    }
}
