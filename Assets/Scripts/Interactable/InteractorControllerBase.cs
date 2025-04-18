using UnityEngine;

namespace Interactable
{
    public abstract class InteractorControllerBase : MonoBehaviour
    {
        [SerializeField] protected Transform interactionPoint;
        [SerializeField] protected float interactionPointRadius = 0.5f;
        [SerializeField] protected LayerMask interactableLayerMask;

        private Collider2D cacheCollider;

        protected Collider2D[] interactableCollider;

        protected IInteractable interactable;
        protected IInteractable lastInteractable;

        protected virtual void Update()
        {
            interactableCollider = Physics2D.OverlapCircleAll(interactionPoint.position, interactionPointRadius, interactableLayerMask);
        }

        protected virtual void CheckInteractable()
        {
            if (interactableCollider.Length == 0) return;

            if (cacheCollider == interactableCollider[0]) return;

            cacheCollider = interactableCollider[0];

            interactable = interactableCollider[0].GetComponent<IInteractable>();
        }

        protected virtual void Interact()
        {
            if (interactable == null || !interactable.IsInteractable)
                return;

            interactable.Interact(this);
        }

        protected virtual void RemoveInteractable()
        {
            if (interactable == null && interactableCollider.Length == 0) return;

            if (interactable != null && interactableCollider.Length == 0)
            {
                interactable.LeaveInteract(this);
                interactable = null;
                lastInteractable = null;
                cacheCollider = null;
            }

            if (interactable != null && interactableCollider.Length > 0)
            {
                if (interactable == lastInteractable)
                    return;

                lastInteractable?.LeaveInteract(this);

                lastInteractable = interactable;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
        }
    }
}