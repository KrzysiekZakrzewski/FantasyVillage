using UnityEngine;

namespace Interactable
{
    public interface IInteractable
    {
        bool IsInteractable { get; }
        bool AutoInteractable { get; }
        string InteractionPrompt { get; }
        Vector2 PromptPosition { get; }
        void SwitchInteractable(bool state);
        bool Interact(InteractorControllerBase interactor);
        void LeaveInteract(InteractorControllerBase interactor);
    }
}