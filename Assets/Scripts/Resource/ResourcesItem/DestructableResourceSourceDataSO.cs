using BlueRacconGames.HitEffect.Data;
using UnityEngine;

namespace BlueRacconGames.Resources.Data
{
    [CreateAssetMenu(fileName = nameof(DestructableResourceSourceDataSO), menuName = nameof(Resources) + "/" + nameof(Data) + "/" + nameof(DestructableResourceSourceDataSO))]
    public class DestructableResourceSourceDataSO : ResourceSourceDataSO
    {
        [field: SerializeField] public DestructableGetHitTargetDataSO DestructableGetHitTargetData { get; private set; }
    }
}
