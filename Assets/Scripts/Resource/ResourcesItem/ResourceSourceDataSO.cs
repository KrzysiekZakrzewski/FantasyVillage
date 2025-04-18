using System.Linq;
using UnityEngine;

namespace BlueRacconGames.Resources.Data
{
    public abstract class ResourceSourceDataSO : ScriptableObject
    {
        [field: SerializeField] public ResourceSourceItemDataSO[] ResourceItemDataSO { get; private set; }

        private void OnValidate()
        {
            SortBySpawnChance();
        }

        private void SortBySpawnChance()
        {
            var sorted = ResourceItemDataSO.OrderBy(x => x.ChanceToSpawnPercent).Reverse();

            ResourceItemDataSO = sorted.ToArray();
        }
    }
}