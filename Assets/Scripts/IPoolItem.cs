using Game;
using System;
using UnityEngine;

namespace BlueRacconGames.Pool
{
    public interface IPoolItem : IGameObject
    {
        event Action<PoolItemBase> OnLaunchE;
        event Action<PoolItemBase> OnExpireE;
        void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition);
    }
}