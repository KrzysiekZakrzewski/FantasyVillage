using BlueRacconGames.Events;
using BlueRacconGames.InventorySystem;
using BlueRacconGames.Pool;
using Game.Item;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Hit
{
    public abstract class HitObjectControllerBase : MonoBehaviour
    {
        [SerializeField] private Transform hitPoint;
        [SerializeField] private LayerMask hitLayer;
        [SerializeField] private float hitRange = 50f;
        [SerializeField] private DefaultPooledEmitter pooledEmitter;

        [field: SerializeReference, ReferencePicker] private IEventListenerFactory<EventListenerBase> eventListenerFactory;

        protected List<GameObject> targetsGM = new();

        private EventListenerBase eventListener;
        private Vector2 lastHitPoint;
        private ToolbarManager toolbarManager;
        private ToolItemBase currentTool;

        public Vector2 LastHitPoint => lastHitPoint;

        [Inject]
        private void Inject(ToolbarManager toolbarManager)
        {
            this.toolbarManager = toolbarManager;
        }

        protected virtual void Awake()
        {
            eventListener = eventListenerFactory.Create();
            eventListener.ResponseE += CheckHitPoint;
            toolbarManager.OnItemRefreshedE += GetUsableItem;
        }

        private void OnDestroy()
        {
            toolbarManager.OnItemRefreshedE -= GetUsableItem;
            eventListener.ResponseE -= CheckHitPoint;
            eventListener.DeRegister();
        }

        public virtual void CheckHitPoint()
        {
            Collider2D[] hitedObjects = Physics2D.OverlapCircleAll(hitPoint.position, hitRange, hitLayer);

            if (hitedObjects.Length == 0)
                return;

            foreach (Collider2D hit in hitedObjects)
            {
                if (targetsGM.Contains(hit.gameObject))
                    continue;

                targetsGM.Add(hit.gameObject);

                if (!hit.TryGetComponent<IHitTarget>(out var target))
                    continue;

                currentTool.OnHit(this, target);
                lastHitPoint = hit.transform.position;
            }

            targetsGM.Clear();
        }

        protected abstract bool CanHit();

        private void GetUsableItem(IItemRuntimeLogic item)
        {
            currentTool = null;

            if (item is not ToolItemBase) return;

            currentTool = item as ToolItemBase;
        }

        private void OnDrawGizmos()
        {
            if (hitPoint == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitPoint.position, hitRange);
        }
    }
}