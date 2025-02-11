using UnityEngine;

namespace Interactable
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        [SerializeField]
        protected string interactionPrompt;
        [SerializeField]
        protected bool autoInteractable;
        [SerializeField]
        private Transform promptPosition;

        private bool isInteractable = true;

        public bool IsInteractable => isInteractable;
        public string InteractionPrompt => interactionPrompt;
        public bool AutoInteractable => autoInteractable;
        public Vector2 PromptPosition => promptPosition.position;

        public abstract bool Interact(InteractorControllerBase interactor);

        public abstract void LeaveInteract(InteractorControllerBase interactor);

        public void SwitchInteractable(bool state)
        {
            isInteractable = state;
        }
    }
}