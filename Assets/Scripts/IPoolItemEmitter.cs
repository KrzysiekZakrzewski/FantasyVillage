using UnityEngine;

namespace BlueRacconGames.Pool
{
    public interface IPoolItemEmitter
    {
        void EmitItem(IPoolItem projectile, Vector3 startPosition);
        void Clear();
    }
}