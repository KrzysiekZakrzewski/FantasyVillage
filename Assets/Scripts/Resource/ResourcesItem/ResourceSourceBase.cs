using BlueRacconGames.Pool;
using BlueRacconGames.Resources.Data;
using UnityEngine;

namespace BlueRacconGames.Resources
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class ResourceSourceBase : PoolItemBase, IResourceSource
    {
        [SerializeField] private Transform resourceSpawnPosition;
        protected IPoolItemEmitter emitter;
        protected ResourceSourceItemDataSO[] resourceSourcesDatas;
        private SpriteRenderer[] spriteRenderers;

        protected virtual void Awake()
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        }

        protected virtual void OnDestroy()
        {

        }

        public override void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition)
        {
            base.Launch(sourceEmitter, startPosition);

            emitter = sourceEmitter;

            SetSortingOrder();
        }

        public void SetSortingOrder()
        {
            for(int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].sortingOrder -= (int)transform.position.y;
            }
        }

        public void SpawnItems()
        {
            var resourceSourceItemData = GetRandomResourceSource(resourceSourcesDatas);

            if (resourceSourceItemData == null) return;

            int spawnedSourceAmount = Random.Range(resourceSourceItemData.MinSpawnAmount, resourceSourceItemData.MaxSpawnAmount);

            for(int i = 0; i <= spawnedSourceAmount; i++)
            {
                var randomOffset = Random.insideUnitCircle;

                var spawnPosition = new Vector2(resourceSpawnPosition.position.x, resourceSpawnPosition.position.y) + randomOffset;

                emitter.EmitItem<ResourceInteractable>(resourceSourceItemData.Prefab, spawnPosition);
            }
        }

        protected ResourceSourceItemDataSO GetRandomResourceSource(ResourceSourceItemDataSO[] resourceSourcesDatas)
        {
            var randomizeValue = Random.value;

            ResourceSourceItemDataSO resourcesData = null;

            for (int i = 0; i < resourceSourcesDatas.Length; i++)
            {
                var tempData = resourceSourcesDatas[i];

                if (tempData.ChanceToSpawnPercent < randomizeValue) break;

                resourcesData = tempData;
            }

            return resourcesData;
        }
    }
}
