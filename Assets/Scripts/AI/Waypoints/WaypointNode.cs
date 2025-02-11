using UnityEngine;

namespace BlueRacconGames.AI.Waypoints
{
    public class WaypointNode : MonoBehaviour
    {
        [SerializeField]
        private float minDistanceToReachWaypoints = 5f;

        [SerializeField]
        private WaypointNode[] nextWaypointsNode;

        [SerializeField]
        private float maxSpeed;

        public float MinDistanceToReachWaypoints => minDistanceToReachWaypoints;
        public WaypointNode[] NextWaypointNode => nextWaypointsNode;
        public float MaxSpeed => maxSpeed;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1f);
        }
    }
}