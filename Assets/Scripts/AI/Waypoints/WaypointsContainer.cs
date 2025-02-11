using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.AI.Waypoints
{
    public class WaypointsContainer : MonoBehaviour
    {
        [SerializeField]
        private WaypointNode[] waypoints;
        public WaypointNode[] Waypoints => waypoints;

        private void Awake()
        {
            if (waypoints != null && waypoints.Length != 0) return;

            GetAllWaypoints();
        }

        public void GetAllWaypoints()
        {
            List<WaypointNode> waypointNodes = new List<WaypointNode>();

            for(int i = 0; i < transform.childCount; i++)
            {
                var waypoint = transform.GetChild(i).GetComponent<WaypointNode>();
                waypointNodes.Add(waypoint);
            }

            waypoints = waypointNodes.ToArray();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            if (waypoints == null || waypoints.Length == 0) return;

            for (int i = 0; i < waypoints.Length; i++)
            {
                var nextWaypoints = waypoints[i].NextWaypointNode;

                if(nextWaypoints == null || nextWaypoints.Length == 0) break;

                for (int j = 0; j < nextWaypoints.Length; j++)
                {
                    Gizmos.DrawLine(waypoints[i].transform.position, nextWaypoints[j].transform.position);
                    //Gizmos.DrawLine(waypoints[i - 1].transform.position, waypoints[i].transform.position);
                }
            }

            //Gizmos.DrawLine(waypoints[0].transform.position, waypoints[waypoints.Length - 1].transform.position);

        }
    }
}