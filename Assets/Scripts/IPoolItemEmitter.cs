using UnityEngine;

namespace BlueRacconGames.Pool
{
    public interface IPoolItemEmitter
    {
        T EmitItem<T>(PoolItemBase item, Vector3 startPosition) where T : class;
        void Clear();
    }
}