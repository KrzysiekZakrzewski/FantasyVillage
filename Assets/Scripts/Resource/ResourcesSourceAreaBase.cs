using BlueRacconGames.Pool;
using BlueRacconGames.Resources.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlueRacconGames.Resources
{
    public abstract class ResourcesSourceAreaBase : MonoBehaviour, IResourcesSourceArea
    {
        [SerializeField] protected DefaultPooledEmitter emitter;
        [SerializeField] protected int maxResourcesSourceAmount;
        [SerializeField] protected ResourceSourceSpawnDataSO[] resourceSourcesDatas;

        protected List<Vector2Int> avaiablePositions;
        protected Dictionary<Vector2Int, ResourceSourceSpawnDataSO> resourceSourcesLUT = new();

        protected virtual void Awake()
        {
            SortBySpawnChance();
        }

        public bool CanSpawn()
        {
            return resourceSourcesLUT.Count < maxResourcesSourceAmount;
        }

        public void RemoveResourceSource(Vector2Int position)
        {
            if (!resourceSourcesLUT.ContainsKey(position)) return;

            resourceSourcesLUT.Remove(position);
        }

        public void SpawnResouceSource()
        {
            if (!CanSpawn()) return;

            var cellPosition = GetSpawnPosition();

            var position = new Vector3(cellPosition.x, cellPosition.y);

            var resourceSourceData = GetRandomResourceSource();

            if (resourceSourceData == null) return;

            var spawnedSource = emitter.EmitItem<DestructableResourceSource>(resourceSourceData.Prefab, position);

            spawnedSource.SetSortingOrder();

            resourceSourcesLUT.Add(cellPosition, resourceSourceData);

            UpdateAvaiablePositions(cellPosition);
        }

        protected abstract void UpdateAvaiablePositions(Vector2Int position);
        protected abstract Vector2Int GetSpawnPosition();
        protected abstract ResourceSourceSpawnDataSO GetRandomResourceSource();

        private void SortBySpawnChance()
        {
            var sorted = resourceSourcesDatas.OrderBy(x => x.ChanceToSpawnPercent).Reverse();

            resourceSourcesDatas = sorted.ToArray();
        }
    }
}
