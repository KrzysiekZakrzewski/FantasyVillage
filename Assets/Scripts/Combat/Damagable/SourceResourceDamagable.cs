using UnityEngine;

namespace Damageable.Implementation
{
    public class SourceResourceDamagable : DamageableBase
    {
        public override bool ExpireOnDead => true;

        protected override void Expire()
        {

        }
    }
}