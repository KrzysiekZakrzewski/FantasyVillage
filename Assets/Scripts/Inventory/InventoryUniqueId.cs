using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace BlueRacconGames.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(InventoryUniqueId), menuName = nameof(BlueRacconGames) + "/" + nameof(InventorySystem) + "/" + nameof(InventoryUniqueId))]
    public class InventoryUniqueId : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [ReadOnly]
        [field: SerializeField] private bool _wasIDSet;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!_wasIDSet)
            {
                Id = GUID.Generate().ToString();
                _wasIDSet = true;
                EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}
