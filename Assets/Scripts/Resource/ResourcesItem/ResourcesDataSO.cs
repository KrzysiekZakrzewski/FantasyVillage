using UnityEngine;

namespace BlueRacconGames.Resources.Data
{
    [CreateAssetMenu(fileName = nameof(ResourcesDataSO), menuName = nameof(Resources) + "/" + nameof(Data) + "/" + nameof(ResourcesDataSO))]
    public class ResourcesDataSO : ScriptableObject
    {
        [field: SerializeField]
        public int Id { get; private set; }
        [field: SerializeField]
        public ResourceSourceBase Prefab { get; private set; }
        [field: SerializeField, Range(0f, 1f)]
        public float ChanceToSpawnPercent { get; private set; } = 0f;
    }
}
