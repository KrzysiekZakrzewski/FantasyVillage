using UnityEngine;

namespace Interactable
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private float maxDistance = 10f;
        [SerializeField] private float maxStrength = 5f;
        [SerializeField] private LayerMask magnetLayer;

        private void FixedUpdate()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, maxDistance, magnetLayer);

            if (hitColliders == null || hitColliders.Length == 0) return;

            foreach (var hitCollider in hitColliders)
            {
                MagnetInteractor magnetInteractor = hitCollider.GetComponent<MagnetInteractor>();

                if (magnetInteractor == null) continue;

                float distance = magnetInteractor.CalculateDistance();

                if (distance > maxDistance) return;

                float strength = Mathf.Lerp(0f, maxStrength, Mathf.InverseLerp(maxDistance, 0f, distance));

                Vector2 direction = (transform.position - magnetInteractor.transform.position).normalized;

                magnetInteractor.UseMagnet(direction, strength);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawWireSphere(transform.position, maxDistance);
        }
    }
}