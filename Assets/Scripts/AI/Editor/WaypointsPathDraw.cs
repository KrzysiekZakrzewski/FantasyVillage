using BlueRacconGames.AI.Waypoints;
using UnityEngine;

namespace CarGame.AI
{
    public class WaypointsPathDraw : MonoBehaviour
    {
        [SerializeField]
        private WaypointsContainer container;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
        }
    }
}