using Damageable;
using Damageable.Implementation;
using UnityEngine;

namespace BlueRacconGames.Resources
{
    public class DestructableResourceSource : ResourceSourceBase
    {
        private SourceResourceDamagable resourceDamagable;

        protected override void Awake()
        {
            base.Awake();

            resourceDamagable = GetComponent<SourceResourceDamagable>();

            resourceDamagable.OnExpireE += ExpireEvent;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            resourceDamagable.OnExpireE -= ExpireEvent;
        }

        private void ExpireEvent(IDamageable damageable)
        {
            SpawnItems();
        }
    }
}