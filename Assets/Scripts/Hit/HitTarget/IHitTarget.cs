using BlueRacconGames.Hit.Data;
using BlueRacconGames.HitEffect.Data;
using Game.Item;
using System;
using Zenject;

namespace BlueRacconGames.Hit
{
    public interface IHitTarget : IGameObject
    {
        HitTargetType TargetType { get; }
        event Action OnTakeHitE;

        void Initialize(DiContainer container, GetHitTargetDataSO initialData);
        bool OnGetHit(HitObjectControllerBase source, ToolItemBase tool, int hitPoints);
        bool CanGetHit();
    }
}