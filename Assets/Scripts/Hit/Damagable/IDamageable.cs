using BlueRacconGames;
using System;

namespace Damageable
{
    public interface IDamageable : IGameObject
    {
        int MaxHealth { get; }
        int CurrentHealth { get; }
        bool Dead { get; }
        bool IsImmune { get; }
        bool ExpireOnDead { get; }

        event Action OnTakeDamageE;
        event Action OnHealE;
        event Action OnDeadE;
        event Action<IDamageable> OnExpireE;

        void Launch(IDamagableDataSO damagableDataSO);
        void TakeDamage(int damageValue);
        void Heal(int healValue);
        void IncreaseHealt(int increaseValue);
        void DecreaseHealt(int decreaseValue);
        void OnDead();
    }
}