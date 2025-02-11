using BlueRacconGames.View;
using Inputs;
using Interactable.UI;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Interactable
{
    public abstract class InteractorControllerBase : MonoBehaviour
    {
        [SerializeField]
        private Transform interactionPoint;
        [SerializeField]
        private float interactionPointRadius = 0.5f;
        [SerializeField]
        private LayerMask interactableLayerMask;

        private Collider2D[] interactableCollider;

        private IInteractable interactable;
        private IInteractable lastInteractable;
        private InteractionPromptUI promptUI;

        [NonSerialized]
        private Inputs.PlayerInput playerInput;

        /*
        [Inject]
        private void Inject(GameHUD gameHud)
        {
            promptUI = gameHud.InteractionPromptUI;
        }
        */
        private void Awake()
        {
            playerInput = InputManager.GetPlayer(0);
            playerInput.AddInputEventDelegate(Interact, InputActionEventType.ButtonPressed, InputUtilities.Interact);
        }

        private void OnDestroy()
        {
            playerInput.RemoveInputEventDelegate(Interact);
        }

        private void Update()
        {
            interactableCollider = Physics2D.OverlapCircleAll(interactionPoint.position, interactionPointRadius, interactableLayerMask);

            CheckInteractable();

            PromptLogic();

            RemoveInteractable();
        }

        public void ChangeDirection(Vector2 direction)
        {
            interactionPoint.localPosition = direction;
        }

        private void RemoveInteractable()
        {
            if (interactable == null && interactableCollider.Length == 0) return;

            if(interactable != null &&  interactableCollider.Length == 0)
            {
                interactable.LeaveInteract(this);
                interactable = null;
                lastInteractable = null;
            }

            if(interactable != null && interactableCollider.Length > 0)
            {
                if (interactable == lastInteractable)
                    return;
                
                lastInteractable?.LeaveInteract(this);

                lastInteractable = interactable;
            }
        }

        private void CheckInteractable()
        {
            if (interactableCollider.Length == 0) return;

            interactable = interactableCollider[0].GetComponent<IInteractable>();

            if (interactable == null || !interactable.AutoInteractable || !interactable.IsInteractable)
                return;

            interactable.Interact(this);
            //interactable = null;
        }

        private void PromptLogic()
        {
            if (promptUI == null)
                return;

            if (CanShowPrompt())
            {
                if (promptUI.IsDisplayed)
                    return;

                promptUI.SetUp(interactable.InteractionPrompt, interactable.PromptPosition);
                return;
            }

            if (promptUI == null || !promptUI.IsDisplayed)
                return;

            interactable = null;
            promptUI.Close();
        }

        private bool CanShowPrompt() => interactableCollider.Length > 0 && interactable != null;

        private void Interact(InputAction.CallbackContext callbackContext)
        {
            if (interactable == null) return;

            interactable.Interact(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
        }
    }
}