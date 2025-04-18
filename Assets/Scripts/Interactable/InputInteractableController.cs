using Inputs;
using Interactable.UI;
using Pathfinding;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactable
{
    public class InputInteractableController : InteractorControllerBase
    {
        [NonSerialized] private Inputs.PlayerInput playerInput;
        [SerializeField] private InteractionPromptUI promptUI;

        private void Awake()
        {
            playerInput = InputManager.GetPlayer(0);
            playerInput.AddInputEventDelegate(InteractInput, InputActionEventType.ButtonPressed, InputUtilities.Interact);
        }

        private void OnDestroy()
        {
            playerInput.RemoveInputEventDelegate(InteractInput);
        }
        protected override void Update()
        {
            base.Update();

            CheckInteractable();

            PromptLogic();

            RemoveInteractable();
        }

        public void ChangeDirection(Vector2 direction)
        {
            interactionPoint.localPosition = direction;
        }

        private void InteractInput(InputAction.CallbackContext callbackContext)
        {
            Interact();
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

            promptUI.Close();
        }

        private bool CanShowPrompt() => interactableCollider.Length > 0 && interactable != null;
    }
}