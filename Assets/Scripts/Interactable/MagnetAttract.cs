using UnityEngine;

namespace Interactable
{
    public class MagnetAttract : MagnetInteractor
    {
        protected override void UseMagnetInternal(Vector2 direction, float strength)
        {
            rb.AddForce(direction * strength, ForceMode2D.Force);
        }
    }
}
