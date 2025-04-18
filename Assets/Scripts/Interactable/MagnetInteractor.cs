using UnityEngine;

namespace Interactable
{
    public abstract class MagnetInteractor : MonoBehaviour
    {
        protected Rigidbody2D rb;

        public bool CanUse { get; protected set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void UseMagnet(Vector2 direction, float strength)
        {
            if (!CanUse) return;

            UseMagnetInternal(direction, strength);
        }

        public void StopUse()
        {
            rb.linearVelocity = Vector2.zero;
        }

        public float CalculateDistance()
        {
            return Vector2.Distance(rb.transform.position, transform.position);
        }

        public void SwitchCanUse(bool value) => CanUse = value;

        protected abstract void UseMagnetInternal(Vector2 direction, float strength);
    }
}
