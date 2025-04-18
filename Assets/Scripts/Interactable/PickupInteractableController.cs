namespace Interactable
{
    public class PickupInteractableController : InteractorControllerBase
    {
        protected override void Update()
        {
            base.Update();

            CheckInteractable();

            RemoveInteractable();
        }

        protected override void CheckInteractable()
        {
            base.CheckInteractable();

            Interact();
        }
    }
}