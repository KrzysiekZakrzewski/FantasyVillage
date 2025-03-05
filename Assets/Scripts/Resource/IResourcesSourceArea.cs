using UnityEngine;

namespace BlueRacconGames.Resources
{
    public interface IResourcesSourceArea
    {
        bool CanSpawn();
        void SpawnResouceSource();
        void RemoveResourceSource(Vector2Int position);
    }
}