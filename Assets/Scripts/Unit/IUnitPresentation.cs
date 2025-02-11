using BlueRacconGames;
using System;

namespace Units
{
    public interface IUnitPresentation : IGameObject
    {
        event Action<IUnitPresentation> OnPresentationEndE;
        void Initialize(IUnit projectile);
        void OnLaunch();
        void OnExpire();
    }
}
