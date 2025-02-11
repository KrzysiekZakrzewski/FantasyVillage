using Game;
using System;
using UnityEngine;

namespace BlueRacconGames.Pool
{
    public interface IPoolItem : IGameObject
    {
        event Action<IPoolItem> OnLaunchE;
        event Action<IPoolItem> OnExpireE;
        void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition);
    }
}