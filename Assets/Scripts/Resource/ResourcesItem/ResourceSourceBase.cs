using BlueRacconGames.Pool;
using Damageable;
using UnityEngine;

namespace BlueRacconGames.Resources
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class ResourceSourceBase : PoolItemBase, IResourceSource
    {
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

        }
    }
}
