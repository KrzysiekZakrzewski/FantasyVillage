using System;
using UnityEngine;
using BlueRacconGames;

namespace Units 
{
    public interface IUnit : IGameObject
    {
        event Action<IUnit> OnLaunchE;
        event Action<IUnit> OnExpireE;
        void Launch(Vector3 position);
    }
}