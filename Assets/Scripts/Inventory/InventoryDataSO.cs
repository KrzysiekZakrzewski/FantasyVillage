using UnityEngine;

namespace BlueRacconGames.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(InventoryDataSO), menuName = nameof(BlueRacconGames) + "/" + nameof(InventorySystem) + "/" + nameof(InventoryDataSO))]
    public class InventoryDataSO : ScriptableObject
    {
        [SerializeField] private InventoryUniqueId inventoryUniqueId;
        [field: SerializeField] public int InventoryCount { get; private set; }

        public string InventoryName => inventoryUniqueId.Id;
    }
}
